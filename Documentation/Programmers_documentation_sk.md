# Programátorská dokumentácia - Work tracer

## Krátky popis

- Aplikácia s grafickým uživatelským rozhraním slúžiaca na zaznamenávanie času strávenom prácou na danom projekte.
- Ďalej obsahuje:
  - interface pre prácu s GIT repozitárom - k tomuto účelu využíva externú aplikáciu **TortoiseGit**
  - grafické znázornenie práce na projekte
  - možnosť zmeny lokalizácie (jazyku)
  - možnosť prechodu medzi repozitárovým a nerepozitárovým (lokálnym) módom

## architektúra aplikácie

aplikácia sa skladá:

- z formulárov, zaisťujúcich uživatelské rozhranie a komunikáciu s uživatelom:
  - Main_form
  - Configure_form
  - Recording_form
  - Commit_form
  - Progress_form
  - YesNoDialog_form

- z tried spolupracujúcich na behu aplikácie, každá z nich spracováva pre ňu špecifické procesy:
  - Program
  - Initializer
  - AppExitMan
  - LocalizationMan
  - ModesMan
  - TortoiseGitMan
  - ProjectMan
  - RecordingMan
  - CommitMan
  - ProgressMan
- (Man v skratke pre *manager*)

## Rozbor jednotlivých tried 

### internal static class **Program**

- obsahuje vstupnú metódu Main() a inštanicie jednotlivých formulárov

#### polia a vlastnosti

```cs
public static Main_form main_form;
public static Configure_form configure_form;
public static Recording_form recording_form;
public static Progress_form progress_form;
public static Commit_form commit_form;
```

- inštancie jednotlivých formulárov, ktoré budeme v projekte využívať
- výnimkou je YesNoDialog_form, ktorý vždy vytvoríme nový a hneď ho aj po jeho využití zavrieme

#### Metódy

```cs
static void Main();
```

- nielenže inicializuje aspúšťa winforms aplikáciu ale aj volá metódu `Initializer.Execute()` ktorá inicializuje všetky triedy aplikácie, ktoré to potrebujú

```cs
static public void CheckAfterActivatingApp(Form form);
```

- metóda volaná z tried formulárov, vždy keď sa detekuje, že sa vstúpilo späť do aplikácie
- táto metóda volá metódy manager-ov, ktorý sa tým môžu prispôsobiť zmenám spraveným počas neaktívnosti aplikácie
- ak sa zistí, že nieje možný prístup k csv súboru záznamov, vypíše sa chybová hláška uživatelovi
- pole `messageAlreadyShown` slúži na to, aby sa po vstúpení do aplikácie neukazovala správa v message.box-e dvakrát, nakoľko sa po ukázaní správy aplikácia znova deaktivovala a ukazovala túto správu dookola a tým sa znemožnilo do aplikácie vstúpiť

### internal static class **Initializer**

- zodpovedá za volanie inicializačných metód všetkých hlavných tried
- pracuje s inicializačným súborom *init_params.txt*, z ktorého na začiatku vytiahne inicializačné dáta uložené pri predošlom uzatváraní aplikácie a pošle ich ďalej do inicializácií jednotlivých tried
- poradie volania inicializácií je dôležité
  - inicializácie niektorých tried závisia na už inicializovanosti iných

#### metódy

```cs
static public void Execute();
```

- hlavná metóda triedy, volaná z vonka
- najprv načíta inicializačné parametre a následne volá všetky inicializácie

```cs
static private Dictionary<string, string> InitParamsParser()
```

- otvorí súbor inicializačnými parametrami, načíta ich a vráti
- ak sa nepodarí taký súbor nájsť, či otvoriť, vráti defaultné parametre

```cs
static private void Initialize (Dictionary<string,string> init_params)
```

- volá inicializačné metódy všetkých manager-ov
- poradie volania inicializácií je dôležité

### public static class **AppExitMan**

- zodpovedá za zpracovanie ukončenia aplikácie uživatelom
- ukladá parametre do incializačného súboru
- pokiaľ nieje aktuálny stav nahrávania *zastavený*, otvára uživatelovi potvrdzujúce dialógové okno, poprípade ruší akciu uzatvárania aplikácie

#### metódy

```cs
public static void ExitApp (FormClosingEventArgs e)
```

- podmienka `e.CloseReason is CloseReason.UserClosing` je čisto kôli tomu že sa následne začnú zatvárať aj ostatné formuláre a tým pádom by sa tento kód previedol pre každý jeden formulár znova
- pokiaľ je nezastavené nahrávanie, otvára sa dialógové okno

```cs
private static void SaveParameters
```

- otvorí sa inicializačný súbor a zapíšu sa doňho potrebné inicializačné parametre


### internal static class **LocalizationMan**

- spravuje lokalizáciu aplikácie
- pokaždé keď sa zmení lokalizácia, prepíše všetky nápisy v aplikácii
  - tým že zavolá metódu Relable() v každom formuláre

#### polia a vlastnosti

```cs
static public Langs Lang; 
```

- udržuje aktuálny jazyk aplikácie

#### metódy

```cs
static public void Initialize(string init_lang)
```

- inicializácia triedy volaná pri spustení programu
- nastaví jazyk podľa parametru z inicializačného súboru
- následne zavolá metódu, ktorá zapríčiní nastavenie daného jazyka

```cs
static public void ChangeLocalization(Langs new_lang)
```

- mení lokalizáciu na danú argumentom
- najprv upraví CurrentUICulture a následne zavolá metódu `Relable()`, ktorá prepíše všetky nápisy v aplikácii

```cs
static public void Relable()
```

- volá `Relable()` metódy všetkých formulárov a taktiež metódu CommitMan triedy, ktorá zabezpečí prepísanie hlášok v richTextBoxoch

### public static class **ModesMan**

- spracováva zmenu módov
- obsahuje triedy reprezentujúce módy aplikácie
- triedy derivované od interface-u IVisitMode slúžia ostatným manager-om ako zprostredkovatelia aktuálneho módu
  - upravený princíp *visotor*-a
  - zabezpečuje rozširitelnosť o ďalšie módy - ak pridáme ďalší mód, tieto triedy popisujú, ktoré fukcie závisia od aktuálneho módu prostredia a potrebujú doimplementovať

#### polia a vlastnosti

```cs
// slúži pre `LocalizationMan` ako zdroj nápisov označenia módov
static public string[] Localizations 
{ get => new string[2] { 
    Localization.Mode_local_text, 
    Localization.Mode_repo_text }; 
}
// inštancie triedy reprezentujúcej mody aplikácie
static private readonly Mode[] modes = 
{ 
    new LocalMode(),
    new ReposMode() 
};
// aktuálny mód aplikácie
static private Mode mode;
// inštancie triedy potomkov interface-u IVisitMode
static public readonly IVisitMode[] visitModes =
{
    new VisitLocalMode(),
    new VisitReposMode(),
};
// aktuálny reprezentant módu pre ostatných manager-ov
static public IVisitMode VisitMode;
```

#### vnútorné triedy a interface-y

```cs
private abstract class Mode {}
private class LocalMode : Mode {}
private class ReposMode : Mode {}
```

- triedy vyjadrujúce mód aplikácie

##### metódy

```cs
public abstract void SetMode();
```

- nastavuje vzhľad prostredia v závislosti na potomkovi triedy Mode, ktorý vyjadruje nejaký mód aplikácie
- zmení nápis v hlavnom menu a zmení farbu objektov, viažucich sa ku výberu Tortoise Git priečinku, v nastaveniach

---

```cs
public interface IVisitMode {}
public class VisitLocalMode : IVisitMode {}
public class VisitReposMode : IVisitMode {}
```

- triedy implementujúce interface IVisitMode slúžia ostatným manager-om ako zprostredkovatelia aktuálneho módu
  - upravený princíp *visotor*-a
  - zabezpečuje rozširitelnosť o ďalšie módy - ak pridáme ďalší mód, tieto triedy popisujú, ktoré fukcie závisia od aktuálneho módu prostredia a potrebujú doimplementovať

##### metódy

```cs
public bool VisitForIsProjectValid();
public bool VisitForIsTGitValid();
public void VisitForSetRightTGit_dir();
public void VisitForSetFalseTGit_dir();
public void VisitForDoYouWnatToCreateRepoQuestion();
public RecordingMan.Record VisitForCreateRecord();
public void VisitForStop_roundButton_Click(object sender, EventArgs e);
public void VisitForCheckAndSetCommitInProgress(int? commitIndex);
public void VisitForCheckAndSetCommitInMain();
```

---

#### metódy

```cs
static public void Initialize(string init_mode)
```

- má za úlohu inicializovať triedu
- z inicialziačného súboru dostane posledný uložený mód, ktorý nastavý ako aktuálne aktívny
- nastaví mód aj reprepentanta módu pre ostatné triedy a zavolá metódu aktuálneho módu `SetMode()`

```cs
static public void ChangeMode(ModesI new_mode)
```

- zmení mód aplikácie na daný argumentom
- nastavi mód aj reprezentanta módu pre ostatné triedy a zavolá metódu aktuálneho módu `SetMode()`
- následne volá metódy všetkých manager-ov, ktorých by sa zmena módu mohla týkať, nech sa na ňu adaptujú

---

### public static class **TortoiseGitMan**

- trieda spravujúca priečinok s Tortoise Git-om (TGit-om)
- mení a kontroluje priečinok
- nastavuje uživatelské rozhranie vzhľadom na validitu priečinku

#### polia a vlastnosti

```cs
// vracia priečinok s TGit-om
static public string TGit_dir { get; }
// uchováva aktuálny priečinok s TGit-om
static private string tGit_dir;
// je v nej uložená platnosť po poslednej kontrole priečinka s TGit-om
// nastavovaná pomocou IsTGitValid() metódy
static public bool LastTGitValidity { get; private set; }
```

#### metódy

```cs
static public void Initialize(string init_tGit_dir);
```

- inicializuje triedu, z inicializačného súboru dostáva poslednú cestu k TGit-u  a ukladá ju do premennej, následne nastavuje uživatelské rozhranie

```cs
static public void ChooseTGitFromDialog();
```

- za pomoci triedy `FolderBrowserDialog()` otvára dialógové okno, v ktorom si môže uživatel vybrať priečinok s Tortoise-Git-om
- ak nieje priečinok platný, vypíše sa príslušná správa
- ak výber niečo preruší, taktiež sa vypíše príslušná hláška
- po vybraní priečinka sa prispôsobý UI a zavolá sa metóda triedy `RecordingMan` aby tiež prispôsobila UI v závislosti na výbere nového TGit priečinku

```cs
static public bool ExistsTG()
```

- metóda na testovanie toho, či sa vo vybranom priečinku naozaj nachádza Tortoise Git
- jednoducho sa testuje existencia spustitelného súboru TGit-u

```cs
        static public bool IsTGitValid();
        static public bool IsTGitValid(ModesMan.VisitLocalMode mode);
        static public bool IsTGitValid(ModesMan.VisitReposMode mode)
```

- vracia platnosť priečinka s TGit-om v závislosti na aktuálnom aktívnom móde
- pokiaľ je aktívny lokálny mód, vracia sa vždy hodnota true, nakoľkov lokálnom móde nieje potrebný TGit
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)

```cs
static public void CheckAndSetTGit_dir()
```

- volá príslušnú metódu na základe platnosti priečinku s TGit

```cs
static private void SetRightTGit_dir();
static public void SetRightTGit_dir(ModesMan.VisitLocalMode _);
static public void SetRightTGit_dir(ModesMan.VisitReposMode _);
```

- nastavuje UI pri platnom nastavení TGit-u v závislosti od aktuálneho aktívneho módu
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)

```cs
static private void SetFalseTGit_dir();
static public void SetFalseTGit_dir(ModesMan.VisitLocalMode _);
static public void SetFalseTGit_dir(ModesMan.VisitReposMode _);
```

- nastavuje UI pri neplatnom nastavení TGit-u v závislosti od aktuálneho aktívneho módu
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)

```cs
static public void WriteTGit_dirTo(StreamWriter file)
```

- metóda (volaná z `AppExitMan`) pre zapísanie aktuálneho priečinku s TGit-om do súboru `file`

### internal static class **ProjectMan**

- trieda pracujúca s projektom a jeho priečinkom
- mení a kontroluje priečinok
- nastavuje uživatelské rozhranie vzhľadom na validitu priečinku
- zároveň počas aktívneho repozitároveho módu na pokyn uživatela zaobstaráva vytváranie repozitára v danom priečinku projektu
- pri výbere projektu sa zatiaľ nevytvára súbor so záznamami, ten sa vytvorí až pri prvom vytvorenom zázname

#### polia a vlastnosti

```cs
//meno CSV súboru, do ktorého sa ukladajú záznamy nahrávania
private const string csvRecordFileName;
// vracia priečinok s projektom
static public string Proj_dir { get; }
// uchováva priečinok aktuálneho projektu
static private string proj_dir;
// je v nej uložená platnosť po poslednej kontrole priečinka s projektom
// nastavovaná pomocou IsProjectValid() metódy
static public bool LastProjValidity { get; private set; }
// vracia cestu ku CSV súboru so záznamami
// kombinácia premennej proj_dir s konštatnou csvRecordFileName 
static public string PathToCSVRecordFile { get; }
```

#### metódy

```cs
static public void Initialize(string init_proj_dir)
```

- inicializuje triedu, z inicializačného súboru dostáva poslednú cestu k poslednému aktívnemu projektu a ukladá ju do premennej, následne nastavuje uživatelské rozhranie

```cs
static public bool ExistsRecordCSV()
```

- testuje, či sa v priečinku aktuálneho projektu nachádza CSV súbor so záznamami

```cs
static public void ChooseProjectFromDialog()
```

- za pomoci triedy `FolderBrowserDialog()` otvára dialógové okno, v ktorom si môže uživatel vybrať priečinok s projektom k nahrávaniu
- ak nieje priečinok platný, vypíše sa príslušná správa
- ak výber niečo preruší, taktiež sa vypíše príslušná hláška
- po vybraní priečinka sa zavolá matóda `DoYouWnatToCreateRepoQuestion()`, ktorá umožňuje uživtelovi inicializovať git-ový repozitár v priečinku projektu
- následne sa prispôsobý UI a zavolajú sa metódy všetkých ostatných manager-ov, ktorých by mohol výber projktu nejakým spôsobom ovlivinť
- pokiaľ nebude CSV súbor so záznamami prístupný, vypíše sa príslušná hláška uživatelovi

```cs
static private void DoYouWnatToCreateRepoQuestion();
static public void DoYouWnatToCreateRepoQuestion(ModesMan.VisitLocalMode mode);
static public void DoYouWnatToCreateRepoQuestion(ModesMan.VisitReposMode mode);
```

- metóda, ktorá ak je aktívny repozitárový mód a existuje cesta ku projektu ale projekt sa nenachádza v žiadnom git-ovom repozitáry, sa opýta uživatela, či náhodou nechce v danom pričinku inicializovať git-ový repozitár
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)

```cs
public static bool ExistsProjDir()
```

- testuje existenciu cesty k priečinku projektu

```cs
public static bool IsThereRepo()
```

- volá externý proces, ktorý vráti true, pokiaľ sa priečinok s projektom nachádza v nejakom git-ovom repozitáry

```cs
static public bool IsProjValid();
static public bool IsProjValid(ModesMan.VisitLocalMode mode);
static public bool IsProjValid(ModesMan.VisitReposMode mode);
```

- na základe aktívneho módu zisťuje, či je daný priečinok s projektom platný
- pri lokálnom mód stačí otestovať existenciu priečinku, pri repozitárovom sa naviac tesuje náležitosť priečinku do git-ového repozitára
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)

```cs
static public void CheckAndSetProj_dir();
```

- volá príslušnú metódu na základe platnosti priečinku s projektom

```cs
static private void SetRightProj_dir();
```

- nastavuje UI pri platnom priečinku s projektom

```cs
static private void SetRightProj_dir();
```

- nastavuje UI pri neplatnom priečinku s projektom

```cs
private static void CreateRepo();
```

- volá externý proces, ktorý inicializuje v aktuálnom vybranom priečinku s projektom git-ový repozitár

```cs
public static void WriteProj_dirTo(StreamWriter file);
```

- metóda (volaná z `AppExitMan`) pre zapísanie aktuálneho priečinku s projektom do súboru `file`

### static public class **RecordingMan**

- trieda zodpovedajuca za nahrávanie práce uživatela
- mení a nastavuje aktuálny stav nahrávania
- mení a nastavuje aktuálnu fázu práce
- zapisuje záznamy nahrávania do CSV súboru, využíva k tomu triedu `CSVHelper`
- pri vytvaraní prvého záznamu sa aj vytvára CSV súbor
  - to že sa v projekte nevyskytuje takýto súbor, znači teda, že jednoducho ešte nebol spravený žiadny zázanam
  - v takom prípade sa stav nahrávania nastaví na počiatočný a to *zastavený*
- obsahuje triedy reprezentujúce stavy nahrávania a fázy práce na projekte
  - triedy reprezentujúce fázy práce na projekte sú nateraz **nevyužité**
- podobne ako `ModesMan` obsahuje triedy derivované od interface-u `IVisitRecState` a od `IVisitWorkPhase` slúžia ostatným manager-om ako zprostredkovatelia aktuálneho stavu nahrávania a fáze práce na projekte
  - v tejto chvíli **nevyžívané** nijakým spôsobom
  - využívanie upraveného princíp *visotor*-a
  - zabezpečuje rozširitelnosť o ďalšie módy - ak pridáme ďalší mód, tieto triedy popisujú, ktoré fukcie závisia od aktuálneho módu prostredia a potrebujú doimplementovať
- obshyje triedy `Record`, ktorá slúži ako vzor pre záznamy a taktiež ako trieda, ktorú využíva knižnica CSVHelper pre zápis záznamov do CSV súboru

#### polia a vlastnosti

```cs
// indikátor toho, ktorý stav nahrávania je aktívna
static public RecStatesI recState { get; private set; }
// indikátor toho, ktorá fáza nahrávania je aktívna
static public WorkPhasesI workPhase { get; private set; }
// slúži pre `LocalizationMan` ako zdroj nápisov označenia stavov
static public string[] StatesLocalizations
{
    get => new string[4]{
    Localization.CurrTrackState_label_none_text,
    Localization.CurrTrackState_label_start_text,
    Localization.CurrTrackState_label_pause_text,
    Localization.CurrTrackState_label_stop_text };
}
// inštancie triedy reprezentujúcej stavy nahrávania aplikácie
static private RecState[] recStates;
// drží v sebe posledný záznam projektu
// pokiaľ projekt neobsahuje ešte ani jeden záznam, je nastavená na null
static public Record? LastRecord { get; private set; }
// konfiguráčné polia pre CSVReader a CSVWriter triedy 
static private readonly CsvConfiguration basicConfig;
static private readonly CsvConfiguration withoutHeaderConfig;   
```

#### vnútorné triedy a interface-y

```cs
public class Record {}
```

- trieda reprezentujúca nahrávku
- využívaná ako vzor pre zápis a čítanie z CSV súboru pomocou knižncie CSVHelper
- každá vlastnosť reprezentuje jeden stĺpec v tomto súbore
- položka *Git* obsahuje kód commit-u vytvoreného pri danom zastavení nahrávania
  - v tomto momente sa nijak **nevyužíva**

```cs
private abstract class RecState {}
private class StartedRecState : RecState {}
private class PausedRecState : RecState {}
private class UnknownRecState : RecState {}
```

- triedy vyjadrujúce stavy nahrávania

##### metódy

```cs
public abstract void SetState();
```

- nastavuje vzhľad prostredia v závislosti na potomkovi triedy RecSTate, ktorý vyjadruje nejaký stav nahrávania

```cs
public abstract Record CreateRecord();
```

- vracia záznam triedy `Record` namieru vytvorený inštancii potomka triedy RecState
- vprípade `StopedRecState` záleží na tom či sme v lokálnom alebo repozitárovom móde
  - keďže je trieda `private`, princíp *visitor*-a zabezpečujú metódy `CreateStopRecord(ModesMan.VisitReposMode mode)` a `CreateStopRecord(ModesMan.VisitLocalMode mode)` (vid ďalej v metódach triedy `RecordingMan`)

---

```cs
public interface IVisitRecState{}
public class VisitStartedRecState : IVisitRecState {}
public class VisitPausedRecState : IVisitRecState {}
public class VisitStopedRecState : IVisitRecState {}
public class VisitUnknownRecState : IVisitRecState {}
```

- triedy implementujúce interface IVisitRecState slúžia ostatným manager-om ako zprostredkovatelia aktuálneho stavu nahrávania
- v tejto chvíli niesú nijak využité
- upravený princíp *visotor*-a
- zabezpečuje rozširitelnosť o ďalšie stavy - ak pridáme ďalšie stavy nahrávania, tieto triedy popisujú, ktoré fukcie závisia od aktuálneho stavu a potrebujú doimplementovať
  
---

```cs
public abstract class WorkPhase {}
public class CreatingWorkPhase : WorkPhase {}
public class ProgramingWorkPhase : WorkPhase {}
public class DebugingWorkPhase : WorkPhase {}
```

- triedy vyjadrujúce fázy práce
- aktuálne niesú nijak využité

---

```cs
public interface IVisitWorkPhase{}
public class VisitCreatingWorkPhase : IVisitWorkPhase {}
public class VisitProgramingWorkPhase : IVisitWorkPhase {}
public class VisitDebugingWorkPhase : IVisitWorkPhase {}
```

- triedy implementujúce interface IVisitWorkPhase slúžia ostatným manager-om ako zprostredkovatelia aktuálnej fázy práce na projekte
- v tejto chvíli niesú nijak využité
- upravený princíp *visotor*-a
- zabezpečuje rozširitelnosť o ďalšie fázy - ak pridáme ďalšie fázy, tieto triedy popisujú, ktoré fukcie závisia od daých fáz práce na projekte a potrebujú doimplementovať

#### metódy

```cs
static public void Initialize()
```

- inicializácia triedy
- je dôležité v ktorom poradí bude zavolaná, nakoľko závisí od inicializovanosti tried

```cs
static public void ProcessNewRecord(RecStatesI new_recState)
```

- zpracováva a ukladá nový stav nahrávania
- vytvorý záznam a následne sa ho pokúsi uložiť
- ak pri ukladaní nenastane žiadna chyba, nastaví sa UI podľa nového stavu a uloží sa nahrávka do `LastRecord` vlastnosti
- pokiaľ ukladanie nahrávky zlyhá, vypíše sa uživatelovi chybová hláška a zmena UI sa neprevedie, nakoľko je preskočená v try bloku

```cs
static public bool IsCSVNotLocked()
```

- testuje dostupnosť súboru CSV

```cs
static public void ChangeWorkPhase(WorkPhasesI new_workPhase)
```

- nastaví premennú `workPhase` na novú hodnotu

```cs
static private void ChangeAndSetRecState(RecStatesI new_recState)
```

- nastaví premennú `recState` na novú hodnotu
- následne prispôsobý UI novej hodnote

```cs
static public void AdaptToEnviromentWithNewProj(out bool ableToAccessCSV)
```

- prispôsobuje UI a stav nahrávania zmene v prostredí aplikácie, pričom počíta s novo nastaveným priečinkom projektu
- volaná väčšinou pri zmene v prostredí, pri ktorej sa zároveň nejakým spôsobom menil projekt
- v závislosti na platnosti zdrojov nastavuje UI požadované vlastnosti
- pokiaľ sa nenájde CSV súbor s nahrávkami, čiže nebola ešte spravená žiadna nahrávka, nastaví sa stav nahrávania na zastavený

```cs
static public void AdaptToEnviromentWithOldProj()
```

- podobné vlastnosti ako `AdaptToEnviromentWithNewProj`
- predpokladá sa však, že priečinok s projektom nebol nijak zmenený

```cs
static private void SaveRecord(Record record)
```

- ukladanie záznamu do CSV súboru so záznamami
- pokiaľ sa nenájde CSV súbor s nahrávkami, čiže nebola ešte spravená žiadna nahrávka, vytvorí s nový súbor a vpíše sa do neho aj *header*

```cs
static private Record? GetLastRecordFromCSV()
```

- vracia posledný záznam z CSV súboru so záznamami
- pokiaľ ešte taký súbor neexistuje, vracia `null` pre indikovanie neexistencie žiadnej nahrávky

```cs
static private void WriteRecordToCsv(Record record)
```

- pomocou triedy CSVWriter, zapíše najprv Header a následne záznam triedy Record do CSV súboru záznamov

```cs
static private void AppendRecordToCsv(Record record)
```

- pomcou triedy CSVWriter pridá nahrávku do CSV súboru s nahrávkami

```cs
static private Record? ReadLastRecordFromCsv()
```

- pomocou triedy CSVReader prečíta všetky záznamu v CSV súboru a vráti posledný z nich

```cs
static public Record CreateStopRecord(ModesMan.VisitLocalMode mode)
static public Record CreateStopRecord(ModesMan.VisitReposMode mode)
```

- pomocné metódy na vytvorenie záznamu zastavenia nahrávania
- sprístupňujú metódy `CreateRecord(ModesMan.VisitReposMode mode)` a `CreateRecord(ModesMan.VisitLocalMode mode)` *private*-nej triedy `StopedRecState`, aby sa mohli previesť v závislosti na aktuálnom aktívnom móde za pomoci princípu *visitor*

### public static class **CommitMan**

- trieda obsluhújuca commit-ovanie a zobrazovanie commit-ov v aplikácii
- pre účel zobrazovania obsahuje vnútornú triedu `CommitPresenter`, ktorá spravuje túto funkcionalitu
- volá Tortoise Git commit-ovaci proces, spravuje výsledky jeho volania, upravuje UI v závislosti na výsledku commit-ovania

#### polia a vlastnosti

```cs
// kód posledného spraveného commit-u v repozitáry projektu
static public string? lastCommitCode;
// indikátor toho, či vznikol nový commit pri poslednom pokuse o commit-nutie
// pokus o commit-nutie nastáva, keď sa uživatel rozhodne v *commit_form*-e zmačknúť tlačidlo Yes, alebo keď sa rozhodne necommit-núť a stlačí tlačidlo Yes v dialógovom okne, ktoré sa ho spytuje, či vážne nechce commit-núť 
static public bool hasBeenCommitted;
```

#### triedy a interface-y

```cs
static private class CommitPresenter
```

- slúži na zobrazovnie commit-ov v aplikácii
- vypisuje poslený commit v hlavnom menu a jeden z commit-ov v časovom úsedku danom uživatelom v prezeraní výsledkov
- volá triedu `TextJustification` pre zarovanie textu commitu

##### polia a vlastnosti

```cs
// konštanty popisujúce rozmery v rich text boxoch, využívané pre správne zarovnanie textu
const int richTextBoxOneCharSize = 8;
const int richTextBoxBias = 6;
// uložené commit-y pre zobrazovanie vo Progress_form formulári
static private string[] commitsFromRangeInProgress = { };
```

##### metódy

```cs
static public void GetCommitsAndShowCurrentOneInProgress()
```

- pokúsi sa získať commit-y z rozsahu danom v progress formuláru a nastaví podľa úspešnosti UI
- ak sa podarí commit-y získať, nastaví sa (ak je to možné) ako aktuálny commit ten s indexom rovným hodnote `Commit_vScrollBar`-u v `Progres_form`
- ak sa nepodarí nájsť žiadny commit v danom rozmedzí, vypíše sa do `Commit_richTextBox` v prezeraní výsledkov príslušná hláška

```cs
static public bool TryGetAndSetCommitsFromRangeInProgress()
```

- získa časové rozmedzie z `progress_form` a získa commity v tomto rozmedzí a uloží ich do premennej `commitsFromRangeInProgress` a vráti indikátor nalezenia aspoň jednoho commit-u

```cs
static public void ShowCommitInProgress(int index)
```

- zobrazenie zarovnaného textu commit-u s daným indexom `index` v `progress_form`-e
- pokiaľ je index väčší ako množstvo uložených commit-ov v `commitsFromRangeInProgress`, vypíše sa posledný z uložených commitov

```cs
static public void GetAndShowLastCommitInMain()
```

- pokúsi sa získať posledný commit repozitára a naśledne ho zobrazí v hlavnom menu v `Commit_richTextBox`-e
- pokiaľ sa žiadny commit nenájde, vypíše na dané miesto príslušnú hlášku

```cs
static private void JustifyAndShowCommitIn(Main_form main_form, string commit);
static private void JustifyAndShowCommitIn(Progress_form progress_form, string commit)
```

- zarovanie a zobrazenie textu `commit`-u v `main_form`/`progress_form`

```cs
static private bool TryGetLastCommitText(out string commitText);
```

- za pomoci externého procesu získa stringovú reporezentáciu posledného commmit-u v repozitáry daného projektu
- vracia túto reprezentáciu a indikátor, že sa podarilo nejaký takýto commit nájsť

```cs
 static private bool TryGetCommitTextsFromRange(DateTime since, DateTime until, out string[] commitTexts)
```

- za pomoci volania externého procesu získa stringové reprezentácie commit-ov z rozpätia `since`-`until`
- ku jednoduchšiemu parse-ovaniu outputu git-ového procesu využíva non-printable znak u0003
- ku commitu pridáva taktiež čas označujúci dobu od spravenia daného commit-u
- vracia list commit-ov v časovo zostupnom poradí a indikátor, či sa podarilo vôbec nejaký commit v danom rozahu násjť

---

#### metódy

```cs
static public void Initialize()
```

- inicializácia triedy - vypísanie patričných nápisov do `Commit_richTextBox`-ov v `progres_form` a `main_form`

```cs
static public bool TryCallTGitAndMakeCommit()
```

- zavolanie externej aplikácie Tortoise Git a následné spracovanie jej výsledku
- vracia false, pokiaľ má uživatel dostať ďalšiu možnosť commit-núť
- pokiaľ sa nepodarí spustiť externá aplikácia vypíše sa príslušná hláška
- vytvorenie nového commit-u testujeme tak, že si pred spravením commit-u uložíme kód posledného a následne po spravení commitu porovnáme nový kód posledného s tým uloženým a ak sa nezhodujú, vieme že bol v čase spustenia aplikácie TGit spravený minimálne jeden nový commit
  - je dôležité si uvedomiť, že tento commit nemusí nutne prísť zo spustenej externej aplikácie, to je ale už problém uživatela, že commituje mimo tejto aplikácie
  - čiže ak sa spraví commit aj mimo aplikácie, aplikácia to detekuje a nastaví daný *externý* commit ako ten posledný spravený a vyhodnotí, že sa spravenie commitu úspešne podarilo

```cs
static public void ChangeCommitInProgressRichTextBox(int index)
```

- jednoduchá metóda sprístupňujúca vypisovanie commitu v `progress_form` 

```cs
static public void GetCheckAndSetCommit_richTextBoxes()
```

- metóda spojujúca získanie a nastavenie správneho commit-u jak v `main_form`, tak aj v `progress_form`
- index commit-u v `progress_form` je zachovaný

```cs
static public void GetCheckAndSetCommit_richTextBoxes(int commitIndexInProgress)
```

- metóda spojujúca získanie a nastavenie správneho commit-u jak v `main_form`, tak aj v `progress_form`
- index commit-u v `progress_form` je zmenený na `commitIndexInProgress`

```cs
static public void GetCheckAndSetCommitInProgress();
static public void GetCheckAndSetCommitInProgress(int commitIndex);
static public void GetCheckAndSetCommitInProgress(ModesMan.VisitReposMode mode, int? commitIndex);
static public void GetCheckAndSetCommitInProgress(ModesMan.VisitLocalMode mode, int? commitIndex);
```

- ziskánie a vypísanie textu commitu v `progress_form` v závislosti na aktuálnom móde aplikácie
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)
- metóda sa volá pomocou dvoch overload-ov
  - bezparametrový zachováva index zobrazeného commit-u
  - jednoparametrový nastavuje index zobrazeného commitu na `commitIndex`
- v prípade lokálneho módu sa vždy vypíše príslušná hláška
- pokiaľ sa v repozitárovom móde nenájde žiadny vypísatelný commit, vypíše sa príslušná hláška

```cs
static public void GetCheckAndSetCommitInMain();
static public void GetCheckAndSetCommitInMain(ModesMan.VisitReposMode mode);
// when local mod is active, respective message is shown in commit rich text box
static public void GetCheckAndSetCommitInMain(ModesMan.VisitLocalMode mode);
```

- ziskánie a vypísanie textu posledného commitu v `main_form` v závislosti na aktuálnom móde aplikácie
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)
- v prípade lokálneho módu sa vždy vypíše príslušná hláška
- pokiaľ sa v repozitárovom móde nenájde žiadny vypísatelný commit, vypíše sa príslušná hláška

```cs
static private bool TryGetLastCommitCode(out string commitCode)
```

- volanie externého procesu na zistenie kódu posledného commitu v repozitáru aktuálneho projektu
- pokiaľ žiadny taký commit neexistuje, vráti sa hodnota `false`

```cs
static public void RunTortoiseGitCommitCommand()
```

- volanie commit-ovacieho externého procesu Tortoise Git-u


### static public class **ProgressMan**

- trieda zodpovedná za zobrazovanie výsledkov nahrávania
- zodpovedá taktiež za úpravu výpočtu výsledkov a zobrazenia commitov v `progress_form`-e pri zmenení časového rozpätia pomocou *dateTimePicker*-ov
- počíta a ukladá si vypočítané hodnoty a zobrazuje ich na príslušných miestach
- ku výpočtu využíva triedu `ComputedValue` a je potomky
  - každý potomok reprezentuje jednu počítanú hodnotu
  - následne sa jednotlivé záznami spracovávajú podľa toho, ktorá hodnota sa sa počíta
  - spôsob akým sa jednotlivé hodnoty počítajú je zachytený v *tabuľke* `whatToDo` triedy `ComputedValue`

#### polia a vlastnosti

```cs
// pole udržujúce si inštancie počítaných hodnôt
// inicializované v Initialize metóde
static private ComputedValue[] computedValues;
// konfiguráčné pole pre CSVReader triedu 
static private CsvConfiguration basicConfig;
```

#### triedy a interface-y

```cs
        private abstract class ComputedValue {}
        private class CompleteComputedValue : ComputedValue {}
        private class CompleteWithPauseComputedValue : ComputedValue {}
        private class CreatingComputedValue : ComputedValue {}
        private class CreatingWithStopsComputedValue : ComputedValue {}
        private class ProgramingComputedValue : ComputedValue {}
        private class ProgramingWithStopsComputedValue : ComputedValue {}
        private class DebugingComputedValue : ComputedValue {}
        private class DebugingWithStopsComputedValue : ComputedValue {}
```

- triedy reprezentujúce jednotlivé počítané hodnoty
- každý potomok *override*-uje  metódu `ProcessRecord` tak, že indexuje do *tabuľky* `whatToDo`, v ktorej sa ukrýva indikátor akcie, ktorý sa má v závislosti na počítanej hodnote vykonať
  - daný získaný indikátor potom použije ako argument metódy `Process`, ktorá na základe neho vykoná určitú akciu
  - (poznámka - tento spôsob nieje najelegantnejší ale dovoluje vcelku jednoducho robiť prípadne zmený v počítaní hodnôt a taktiež pridávanie ďalších počítaných hodnôt či akcií)

> !! je potrebné dávať pozor, že sa na túto techniku výpočtu nevzťahuje žiadny princíp *visitor*-a, nakoľko výpočet závisí na stavu nahrávania a pracovnej fáze danej spracovávanej nahrávky
> je potrebné preto dávať pozor, pokiaľ sa bude pridávať nový stav nahrávania alebo pracovnej fáze, aby sa doimplementovalo na nich závislé počítanie výsledkov - teda aby sa rozšírila *tabuľka* `whatToDo` o ďalšie hodnoty

##### polia a vlastnosti

```cs
// pole, v ktorom sa počíta výsledná hodnota dĺžky nahrávania inštancie
public TimeSpan CompleteTime { get; protected set; }
// pomocné pole, do ktorého si pri konkrétnych typoch záznamov uložím počiatočný čas meraného úseku a následne sa pomocou neho a prislúchajúceho konca meraného úseku vypočíta čaový prírastok ku CompleteTime hodnote
public DateTime? InitialClosure { get; protected set; }
// pole vyjadrujúce jednotlivé akcie v trojdimenzionálnej mriežke
// dimenzie tvoria stav nahrávania, pracovná fáza a typ počítanej hodnoty
// pre každý jeden prípad je tu vypísaná akcia, ktorá sa ma vykonať
protected Processes[,,] whatTodo;
```

##### metódy

```cs
public ComputedValue()
```

- pri volaní konštruktoru sa zavolá metóda `Reset`, ktorá nastaví vlastnosti na počiatočné

```cs
public virtual void Reset()
```

- nastavuje vlastnosti na počiatočné hodnoty

```cs
public abstract void ProcessRecord(RecordingMan.RecStatesI recStateI, RecordingMan.WorkPhasesI workPhaseI, DateTime datetime);
```

- spracovanie záznamu
- dostane časový údaj, stav nahrávania a pracovnej fáze
- spôsob spracovania sa riadi pomocou pola `whatTodo`
- jednotlivý potomkovia si ju override-uju tak, aby správne indexovali do tohto pola

```cs
protected void Process(Processes process, DateTime datetime)
```

- metóda volaná z `ProcessRecord` metódy
- na základe argumentu `process` prevedie danú akciu

---

#### metódy

```cs
static public void Initialize()
```

- inicializácia triedy, inicializujú sa polia počítaných hodnôt
- zároveň sa zavolá nastavenie *dateTimePicker*-ov s príznakom na reset hodnôt nastaveným na *true*

```cs
static public void CheckAndSetDateTimePickersInProgress(bool ResetValues, out bool ableToAccessCSV)
```

- metóda pre nastavenie `Since_dateTimePicker` a `Until_dateTimePicker` objekty v `progress_form` formuláry
- obsahuje argument `ResetValues` ktorý pokial je *true*, tak nastaví dané *picker*-y na časové hodnoty prvého a posledného záznamu v CSV súbore aktuálneho projektu
- pokiaľ je tento argument nastavený na *false*, hodnoty sa zachovávajú ak sú v rozmedzí danom prvým a posledným záznamom projektu
  - ak je hodnota niektorého z picker-ov mimo daný interval, nastaví sa na časovú hodnotu prvého, resp. posledného záznamu  
- pokiaľ sa v projekte zatial nenachádza žiadny záznam (teda výsledný prvý a posledný záznam je `null`), žiadna zmena sa nekoná
- minimálne a maximálne hodnoty *dateTimePicker*-ov sa udržujú také, aby sa nemohli *dateTimePicker*-y navzájom prekrývať
- pokiaľ sa nepodarí čítať z CSV súboru so záznammi, vráti sa *false* hodnota v argumente `ableToAccessCSV`

```cs
static public void SetAndShowProgression(out bool ableToAccessCSV)
```

- získa časové rozpätie a spočíta z neho výsledné hodnoty roboty a následne ich vypíše v `progres_form`-e

```cs
static public void ShowComputedValuesInProgress()
```

- volá metódy sprístupňujúce *label*-y z `progress_form` formuláru
- prepisuje výsledné hodnoty v tomto formuláru

```cs
static public void ComputeProgressFromCsvInRange(DateTime since, DateTime until, out bool ableToAccessCSV)
```

- otvorí CSV súbor so záznamami a jedne po druhom ich spracováva a necháva nech si všetky inštancie pola `computedValues` spravia svoje akcie v závislosti na daných záznamoch
- na začiatku sa všetky hodnoty resetujú/vynulujú
- následne sa pomocou triedy `CSVReader` prechádzajú všetky záznamy v CSV súboru
- pokiaľ sa nedá otvoriť CSV súbor, vráti sa hodnota *false* vo výstupnom argumente `ableToAccessCSV`


```cs
private static void ResetComputedValues();
```

- resetuje všetky inštancie pola `computedValues`

```cs
private static bool TryProcessNextRecordInRangeFrom(CsvReader csv, DateTime since, DateTime until);
```

- pokúsi sa prečítať a spracovať ďalší záznam z CSV súboru
- spracováva len záznamy v intervale `since`-`until`
- pokial je spracovávanie z akéhokolvek dôvody ukonca, vracia *false*
- pre každú počítanú hodnotu volá pre daný spracovávaný záznam metódu `ProcessRecord` s parametrami záznamu

```cs
 private static bool TryReadRecord(CsvReader csv, out RecordingMan.Record? record);
```

- pokúša sa prečítať ďalší záznam z CSV súboru a vrátiť ho vo výstupnom argumente `record`
- ak sa to z akéholkovek dôvodu nepodarí alebo už nieje žiadny ďalší záznam ktorý by sa dal čítať vracia hodnotu *false*

```cs
 static private bool TryReadFirstAndLastRecordFromCsv(RecordingMan.Record? firstRecord, RecordingMan.Record? lastRecord);
```

- prečíta CSV súbor so záznamami a vráti prvý a posledný záznam vo výstupných argumentoch
- pokiaľ by CSV súbor nebol prístupný, vráti hodnotu *false*

## Rozbor implementácií jednotlivých formulárov

### public partial class **Main_form** : Form

- hlavné okno
- otvára sa ako prvé pri spustení aplikácie

#### niektoré metódy

```cs
private void RecordingFormOpening_Button_Click(object sender, EventArgs e)
```

- pred otvorením formulára `recording_form` vypíše príslušné upozornenia v závislosti na neplatnosti zdrojov

```cs
private void ProgressFormOpening_button_Click(object sender, EventArgs e)
```

- pred vstupom do `progress_form` formulára nechá spočítať a zobraziť výsledky nahrávania
- ak sa nepodarilo čítať z CSV súboru, vypíše sa príslušné upozornenie

```cs
public void Relable()
```

- prepísanie nápisov vo formuláre z dôvodu lokalizácie aplikácie

```cs
        public void SetTortoiseFileNotSelected_labelVisible(bool indicator);
        public void SetProjNotSelected_labelVisible(bool indicator);
        public void SetMode_label();
        public void SetProgressFormOpening_buttonEnabled(bool indicator);
        public void WriteToCommit_richTextBox(string what);
        public void SetCurrTrackState_label();
        public int GetCommit_richTextBoxWidth();
```

- metódy sprístupňujúce objekty formulára z vonkajška

### public partial class **Configure_form** : Form

- formulár, v ktorom sa dajú nastaiť rôzne parametre aplikácie
- zmena jazyka, módu, projektu alebo priečinka obsahujúceho Tortoise Git

#### niektoré metódy

```cs
private void ChooseMode_trackBar_Scroll(object sender, EventArgs e)
```

- ak sa niektorý zo zdrojov stal neplatný, vypíše sa chybová hláška uživatelovi

```cs
public void Relable()
```

- prepísanie nápisov vo formuláre z dôvodu lokalizácie aplikácie

```cs
public void SetLocalization_trackBar(int value);
public void SetChooseMode_trackBar(int value);
public void SetProjDir_label(string dir);
public void SetProjDir_labelColor(System.Drawing.Color color);
public void SetTGitDir_label(string dir);
public void SetTGitDir_labelColor(System.Drawing.Color color);
public void SetForeColor_TGitLabelsButtons(System.Drawing.Color color);
```

- metódy sprístupňujúce objekty formulára z vonkajška

### public partial class **Recording_form** : Form

- formulár slúžiaci na nahrávanie práce uživatela

#### niektoré metódy

```cs
private void Stop_roundButton_Click(object sender, EventArgs e);
public void Stop_roundButton_Click(ModesMan.VisitLocalMode mode, object sender, EventArgs e);
public void Stop_roundButton_Click(ModesMan.VisitReposMode mode, object sender, EventArgs e);
```

- spracuje zastavenie nahrávania na základe aktuálneho módu
- kontrola módu je zprostredkovaná princípom *visitor* (vid. *ModesMan*)
- pri repozitárovom móde sa otvorí `commit_form`, aby mohol uživatel commit-núť svoju prácu 

```cs
private void Start_roundButton_EnabledChanged(object sender, EventArgs e)
private void Stop_roundButton_EnabledChanged(object sender, EventArgs e)
private void Pause_roundButton_EnabledChanged(object sender, EventArgs e)
```

- pri *disable*-nutí tlačidla sa odstráni obrázok, aby bol jasné že sa nedá stlačiť

```cs
public void Relable()
```

- prepísanie nápisov vo formuláre z dôvodu lokalizácie aplikácie


```cs
public void SetCurrTrackState_label();
public void SetStart_roundButtonEnabled(bool indicator);
public void SetStop_roundButtonEnabled(bool indicator);
public void SetPause_roundButtonEnabled(bool indicator);
public void SetPhase_trackBarEnabled(bool indicator);
public void SetPhase_trackBarValue(int value);
```

- metódy sprístupňujúce objekty formulára z vonkajška

### public partial class **Commit_form** : Form

- formulár slúžiaci spytujúci sa uživatela, či chce commit-núť zmeny pri zastavení nahrávania

#### niektoré metódy

```cs
private void NoCommit_button_Click(object sender, EventArgs e)
```

- ak sa uživatel zmačknutím tlačidla rozhodne necommit-núť, ukáže sa mu dialógové okno, spytujúce sa, či si je istý
- pokiaľ si je istý, ukáže `recording_form` a skryje `commit_form` a spracuje nový stav nahrávania


```cs
private void YesCommit_button_Click(object sender, EventArgs e)
```

- ak sa uživatel zmačknutím tlačidla rozhodnte commit-núť zmeny, zavolá sa commit-ovací proces Tortoise Git-u
- ak je commit úspešný, alebo sa uživatel počas behu externého procesu rozhodne, že commit-ovať nechce, ukáže `recording_form` a skryje `commit_form`
- pokiaľ sa uživatel počas behu externého procesu rozhodne necommit-ovať, dialógové okno sa ukáže, spytujúc sa, či si je istý že neche commit-ovať



```cs
private void BackFromCommitToRecordingForm()
```

- skrývanie `commit_form`-u a ukázanie `recording_form`-u
- zároveň sa nastavia `Commit_richTextBox`-y
- nakoniec sa spracuje novy stav nahrávania

```cs
private void Commit_form_FormClosing(object sender, FormClosingEventArgs e)
```

- pokiaľ je okno commit-formulára zavreté uživatelom, má táto akcia rovnaký výsledok, ako stlačenie tlačidla `NoCommit_button`

```cs
public void Relable()
```

- prepísanie nápisov vo formuláre z dôvodu lokalizácie aplikácie

### public partial class **Progress_form** : Form

- formulár ukazujúci postup práce uživatela

#### niektoré metódy

```cs
private void Since_dateTimePicker_CloseUp(object sender, EventArgs e)
private void Until_dateTimePicker_CloseUp(object sender, EventArgs e)
```

- keď uživatel zmení či už počiatočný aleb koncový dátum, všetky výsledné odnoty sú prepočítané a commit-y v *richTextBox*-e prenastavené tak, aby boli v súlade s novým časovým rozpätím
- zároveň si *dateTimepicker*-y udržujú maximálne a minimálne hodnoty tak, aby sa v žiadnom momente nekrížili
- pokiaľ je zakliknutý `SameDate_checkBox`, dá sa stlačiť iba `Since_dateTimePicker` a  `Until_dateTimePicker` sa nastavuje na jeho hodnotu

```cs
private void SameDate_checkBox_CheckedChanged(object sender, EventArgs e)
```

- pokiaľ je `SameDate_checkBox` zakliknutý, `dateTimePicker`-y fungujú ako jeden
- nastavia sa na rovnaký dátum a `Until_dateTimePicker` sa zablokuje pre uživatela
- podobne ako pri *datetimePicker*-och sa znova prepočítajú výsledné hodnoty a prenastavia sa commit-y v `Commit_richTextBox`-e

```cs
public void Relable()
```

- prepísanie nápisov vo formuláre z dôvodu lokalizácie aplikácie

```cs
 public void SetCompDuration_labelText(string text);
public void SetCompDurationWithPause_labelText(string text);
public void SetCreatDuration_labelText(string text);
public void SetCreatDurationWithPause_labelText(string text);
public void SetProgrDuration_labelText(string text);
public void SetProgrDurationWithPause_labelText(string text);
public void SetDebugDuration_labelText(string text);
public void SetDebugDurationWithPause_labelText(string text);
public void WriteToCommit_richTextBox(string what);
public int GetCommit_richTextBoxWidth();
public void EnableCommit_vScrollBar(bool indicator);
public DateTime GetFullSince_dateTimePickerDate();
public DateTime GetFullUntil_dateTimePickerDate();
public void SetUntil_dateTimePickerMinDate(DateTime minDate);
public void SetSince_dateTimePickerMaxDate(DateTime maxDate);
public void SetSince_dateTimePickerValue(DateTime value);
public void SetUntil_dateTimePickerValue(DateTime value);
public void SetRecordSinceDate_labelText(string date);
public void SetRecordUntilDate_labelText(string date);
public void SetCommit_vScrollBarMaximum(int maximum);
public int Commit_vScrollValue { get; set; }
```

- metódy sprístupňujúce objekty formulára z vonkajška

### public partial class **YesNoDialog_form** : Form

- formulár, ktorý funguje ako dialógové okno
- argumetnmi pri volaní jeho konštrukotru sa mu nastavia nápisy
