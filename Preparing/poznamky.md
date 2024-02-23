# Poznamky

## Arhitektura work tracker-u

- Program
- Main_form
- Recording_form
- Configure_form
- Commit_form
- YesNoDialog_form

## Co chcem

### v mainu

1. *RecordingFormOpening_button* - otvori sa mi okno na nahravanie
    - skonrolujem ci mam nastaveny tortoise git folder a subor projektu - ak nieje nieco nastavene, odnaviguje uzivatela na miesto kde to ma nastavit pomcou *MessageBox*
      - tortois kontrolujem iba ak som v repo mode
2. *ConfigFormOpening_button* - otvori configuracny formular
    - dva labely pod buttonnom sluzia na upozornenie, ze nieje vybrany source projekt a source tortoise-git-u
3. *ProgressFormOpening_button* - ak je vybrany validny projekt, otvori formular s nahliadnutim progressu v danom projekte, neda sa zmacknut, ak nieje vybrany projekt

- v richtext boxe je zobrazeny posledny commit projektu
  - v pripade nevybraneho projektu, prazdneho repozitara alebo lokalneho modu vypisem prislusnu hlasku
- pridam label, ktory mi bude vraviet v akom stave recordingu akurat som, nakolko pocas recordingu budem moct vyjst z record formulara
- zaroven este pridam Mode_label, ktory mi hovori v ktorom mode som - ci local alebo repo

### v recordingu

1. *Play_roundButton* - zapise do csv suboru ze sa zacalo trackovanie
    - zmeni sa vnutorny stav na play-stav
    - az teraz budem vytvarat csv-cko, ak sa nahodou v projekte nenachadza...teda asi sa vytvori proste samo, tym ze donho prvykrat budem chciet zapisat
2. *Pause_roundButton* - zapise sa do csv suboru zastavenie trackovania
    - zmeni sa vnutorny stav na zastavenie
3. *Stop_roundButton* - pri repo mode:
    - otvori sa okno s otazkou, ci chcem commitnut
      - ak nechem, zapise sa do csv suboru zastavenie trackovania s not commit indikatorom a vratim sa do **recordingu**
      - ak chcem commitnut, tak otvorim Tortoise git,
        - po jeho zavreti pozriem, ci sa vazne commitlo:
        - ak nie, tak znova otvorim okno s tym ci chcem commitnut
        - ak ano, zapisem zaznam do csv-cka a priradim mu id commitu, vratim sa do **recordingu**
    - pri nie repo mode - proste zapisem do csv suboru a zostavam v **recordingu**
    - zmenim sa vnutorny stav na stop-stav
4. *ReturnToMain_button* - vrati ma do main manu, moze ma vratit kedykolvek, teda za akehokolvek stavu trackovania
5. *ConfigFormOpening_button* - otvori configuracny formular

- nebudem zanechavat csv-cko v nejakom konzistentnom stave, proste co tam posledne uzivatel zada, to tam bude, ked tak sa ho len mozem opytat, ked bude chciet zavriet okno, ci nechce zastavit nahravanie
- vzdy ked zaktivnim dane okno...a.k.a. kliknem nanho, tak skontrolujem directory csv-cka, tortoise gitu a aj projektu, popripade upozornim uzivatela na to ze sa zmenili direktory, ze si to musi upravit..
  - taktiez pri commitovani kontrolujem, ci sa mi nezmenil udaj o poslednom commite
- dokym niesu vsetky adresare spravne nastavene, nenecham ho stlacit ziadne trackovacie tlacidlo
- zaznamenavat aj fazu programovania, faza sa moze menit iba pocas pauznutia/stopnutia - faza sa zaznamenava v dalsom stlpci v csv-cku

### v configuracii

1. *ChooseTGitDir_button* - otvori mi file manager, kde si mozem najst folder s tortoisom
    - kontrolujem tym, ze sa pozriem, ci sa v nom nachadza *TortoiseGitProc.exe* subor
2. *ProjectSelection_Button* - otvori mi file manager, v ktorom si vyberiem folder s projektom, bud:
    1. vyuzivam repo
        - to ze dany projekt vyuziva git repozitar zistim tak, ze zavolam funkciu *git rev-parse --is-inside-work-tree*
          - ak sa tam nenachadza, tak sa opytam uzivatela, ci z toho chce spravit repozitar - pytam sa uz vtedy, ked uzivatel vybera dany projekt - napisem aj radu, ze ak neche vyuzivat repo, tak nech si to ide prestavit do nastaveni, ak si ho tam nevytvori, stale bude napisane ze si nevybral repo
          - pytam sa otvorenim CreateRepo_form-u.cs
    2. nevyuzivam repo
        - nemusim kontrolovat git, cize sa ani nepytam ci chcem vytvorit adresar, proste ten subor ktory vyberie, tam potom ulozim to moje csv-cko
    - nastavim stav nahravania podla posledneho zanzmu v csv-cku, ak sa tam ziadne csv-cko nenachadza, nastavim ho na noTrackingTable stav(pre uzivatela vypisem ze je v zastavenom stave)
3. *checkBoxy sk,en* - na menenie jazyka, vzdy len jeden zakliknuty, lokalizaciu skusim aplikovat instantne po zakliknuti...
4. trackBar - menenie medzi repo a local modom
    - kotrolujem projekt, csv-cko a popripade tortoise git ulozisko
5. *BackToPrevForm_button* - po zmacknuti sa vratime z configu do predosleho otvoreneho okna, ci uz main alebo recording okna

- moznost zmenit umiestnenie tortoise-gitu
  - mozno nejakym sposobom upresnit, ktory folder presne ma uzivatel vybrat
- moznost zmenenia lokalizacie
- moznost zmeny trackovacieho modu
- vsetko co nieje platne vybrane sfarbim na cerveno
- vypisovat v labeloch iba meno projektu, nie celu path k nemu..., ak nevalidny projekt, tak vypisovat No valid project cervene napriklad
- vytvaram csv az pri prvom zapocati nahravania
- ak nieje vybrany subor validny(ci uz tortoisegit alebo projekt), vypisem ho pod button cervene

## v Progresse

1. *From_dateTimePicker/To_dateTimePicker* - zoberiem interval,medzi korym mam zobrazit dni/7dni/mesiace,
    - vzdy nastavim v *from* maximalny date z *to* a symetricky minimalny v *to* z *from*
    - minimalny v *from* je datum prveho zaznamu a maximalny v *to* je datum posledneho zaznamu v csv-cku
2. *Commit_dateTimePicker* - ukaze commit vybraneho datumu v richTextBox-e
    - budem si vazne vyberat len datum a ukaze mi to prvy commit tohto datumu
    - nasledne tam budem mat scrollbar, ktorym budem moct listovat medzi commitmi tohto datumu
    - ak sme v local mode, je v richTextBoxe napisana prislusna hlaska

- davame pozor, lebo projekt nemusi obsahovat to csv-cko, ak ho neobsahuje, vsade budu nuly a grafy nebudu nic ukazovat
- treba vyriesti problem s prechodnymi rokmi a tak..

- **vsetky nastavenia si ukladam tiez do config textaku**

## Myslienky

- zaznamy casov budem ukladat do .git file-u ak je repo mod zapnuty, inak priamo do suboru projektu, nemusim to robit zlozitejsie ako to je
- prvotne nastavenia spravim priamo v aplikacii, nebudem sa zatazovat ziadnymi config vecmi

- vytvorit triedu na kontrolovanie zdrojov, ktora bude mat nejake funkcie, ktore budu vracat error kody v zavislosti na tom co sa pokazilo, co uz neplati, alebo naopak ci je vsetko v pohode
- kontrolovat zdroje budem vzdy, ked je moznost, ze by uzivatel nieco pokazil, teda ked sa vratim spat do aplikacie po tom co sa z nej vykliklo

- formulare si inicializovat v programe a ulozit si ich a otvarat stale tie iste, nevytvarat nove stale

-uvolnovat tlacidla az po tom, co prebehnu vsetky ostatne procesy, taka pasivna ochrana voci zlomyselnikom

## co treba vzdy kontrolovat

- pri zapisani do csv-cka - kontrolovat, ci je csv stale tam kde ma byt
- pri praci s repozitarom:
  - ci sa mi nezmenilo miesto tortoise-git-u
  - ci stale ma cestu ku .git - file-u

- vzdy ked kliknem enternem akekolvek okno aplikacie, zkontrolujem zdroje a popripade nastavim vhodne indikatory
- command na testovanie toho, ci sa nachadzam v directory s repozitorom:

```bash
git rev-parse --is-inside-work-tree
```

## pri spusteni aplikacie

- nastavim vsetko podla config file-u
- kontrolujem, ci stale existuje cesta ku tortoise-git-u
- nastavim stav nahravania podla posledneho stavu nahravania
- zistim kod posledneho commit-u

1. mod repo:
    - kontrolujem ci stale existuje dany projekt a ci v nom je stale repozitar - ak niektora z tychto veci neplati, napisem, ze nieje vybrany cielovy projekt
2. mod nonrepo:
    - kontrolujem, ci existje cesta do projektu

## Zatvaranie aplikacie

- aplikácia sa vypína jednoduchým stlačením tlačidla pre uzatvorenie okna
- pokiaľ je náhodou nahrávanie v sputenom alebo pozastavenom stave, vyskočí na uživatela upozornenie, že stav nahrávania nieje zastavený a či naozaj chce odísť z aplikácie
- pri zatvarani aplikacie ulozit recordings

## Problemy na vyriesenie

- mozno dako riesit nedostupnost git-ovych procesov
- dako vyriesit, ked sa mi niekto bude hrabat v csv-cku a znevalidniho, osetrene to mam ale dako by to chcelo asi oznamovat uzivatelovi
- umiestnenie aplikacie, aby sa mi nezobrazovali okna stale len hore ale aby sa vsetky premiestnovali po premiestneni jednoho z nich, najlepsie aby sa centrovali vsetky do stredu

## Uzitocne veci

- ziskanie id posledneho commitu, textu posledneho commitu, textu commitov v intervale
git log --format="%H" -n 1
git log -1 --pretty=%B
git log --oneline --since="2022-04-22" --until="2022-04-24" --pretty=%B


## Toto si tu odlozim

### Z progress manageru, keby som chcel presa len to zmenit spat

//public virtual void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitUnknownRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitUnknownRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }
//public virtual void ProcessRecord(RecordingMan.VisitUnknownRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) { }








//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) 
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}

//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;

//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}





//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}

//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}





//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, CreatingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, ProgramingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}





//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitPausedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}




//public override void ProcessRecord(RecordingMan.VisitStartedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime) => InitialClosure = datetime;
//public override void ProcessRecord(RecordingMan.VisitStopedRecState visitRecState, DebugingWorkPhase workPhase, DateTime datetime)
//{
//    if (InitialClosure is not null) CompleteTime += datetime - (DateTime)InitialClosure;
//    InitialClosure = null;
//}
