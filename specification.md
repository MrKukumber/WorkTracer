# Špecifikácia - Work Tracer

## Názov

Work Tracer

## Krátky popis

- Aplikácia s uživatelským rozhraním slúžiaca na zaznamenávanie času strávenom na práci na danom projekte.
- Ďalej obsahuje:
  - interface pre prácu s GIT repozitárom - k tomuto účelu využíva externú aplikáciu **TortoiseGit**
  - grafické znázornenie práce na projekte
  - možnosť zmeny lokalizácie (jazyku)
  - možnosť prechodu medzi repozitárovým a nerepozitárovým módom

## Spôsob spustenia

- pomocou spustenia .exe súboru aplikácie alebo odkazu naňho
- pokiaľ chce užívateľ pracovať aj v repozitárovom móde, musí mať pred spustením nainštalovanú aplikáciu [TortoiseGit](https://tortoisegit.org/download/), ideálne verzie 2.14.0, pri novších a starších verziách nieje zaručená funkčnosť aplikácie

## Práca s aplikáciou

### Základný princíp

- slúži na zaznamenávanie práce programátora na konkrétnom projekte
  - programátor si pri práci spúšťa a zastavuje "časomieru" a aplikácia dané úseky zaznamenáva a ukladá do príslušného vytvoreného .csv súboru, ktorý sa uloží do vybraného priečinku projektu
- následne si môže uživatel pozrieť commit-y daného projektu, graf znázorňujúci jeho prácu a celkové množstvo odpracovaných hodín na projekte

### Používanie a funkcie

- po spustení aplikácie sa otvorý **hlavné menu**

#### Hlavné menu

- v hlavnom menu vidíme tri tlačidlá, ktoré nás presmerujú do okien jednotlivých funkcionalít
- následne sa tu nachádza kolonka, v ktorej (ak už máme vybraný projekt a máme zapnutý repozitárový mód) sa nám zobrazí posledný commit daného vybraného projektu
- ďalej sa tu náchádza informácia o tom, či máme zastavené, pustené alebo pozastavené nahrávanie, informácia o aktívnom móde(repozitárový/lokálny) a prípadne upozornenie, ak nemám vybraný valídny projekt alebo valídnu cestu k priečinku s TortoiseGit-om

#### Nastavenia

- z hlavného menu sa pomocou príslušného tlačidla dokážeme dostať do nastavení aplikácie
- vo výslednom okne môžeme meniť jazyk aplikácie medzi anglickým a slovenským a tiež meniť mód zaznamenávania práce z lokálneho (bez využitia GIT-ového repozitára) na repozitárový a naopak
- pokiaľ je zapnutý repozitárový mód, je potrebné vybrať súbor, v ktorom sa nachádza TortoiseGit
  - po stlačení patričného tlačidla sa otvorí okno, v ktorom bude môcť uživateľ vybrať spávny priečinok s touto aplikáciou
  - konkrétne je potrebné vybrať priečinok *cesta_ku_priecinku_s_TortoiseGit\Nazov_priecinka_vybrany_pri_instalacii\bin* (napr.: C:\Program Files\TortoiseGit\bin)
- následne je potrebné vybrať súbor s projektom, ktorého vývoj budeme zaznamenávat
  - ten vyberieme podobne druhým tlačidlom v danom okne
  - vo vybranom priečinku projektu (ak už náhodou nieje vytvorený) sa vytvorý .csv súbor do, ktorého sa budú zaznamenávať log-y z aplikácie, pokiaľ už sa tam súbor nachádza, aplikácia si ho načíta a pracuje s ním
  - ak nieje zapnutý *lokálny* mód, priečinok s projektom môže byť v podstate akýkoľvek priečinok, v ktorom je možné upravovať súbory
  - pokiaľ je zapnutý *repozitárový* mód, najprv sa otestuje, či daný priečionk je súčasťou repozitára, ak nebude, aplikácia sa opýta, či chce uživatel vytvoriť na danom mieste nový repozitár
    - ak odmietne, projekt zostane nevybraný
  - akonáhle zmením výber projektu, načítajú sa jeho dáta a nastaví sa teda aj aktuálny stav nahrávania na ten, ktorý bol ako posledný zapnutý v tomto projekte
- všetky nastavenia sa po vypnutí aplikácie uložia a pri novom spustení aplikácie sa znova načítajú
- pri prvom spustení je potrebné všetko prvotne nastaviť
- nakoniec sa príslušným tlačidlom dostaneme späť do okna, z ktorého sme do nastavení prišli

#### Zaznamenávanie

- ďalším tlačidlom v menu sa dostaneme do okna, ktoré slúži na samotné zaznamenávanie pracovného procesu
- vidíme tu tri tlačidlá pre zaznamenávanie, slider pre zmenu základných fáz vývoja a následne tlačidlo, ktorým sa znova dostaneme do nastavení a tlačidlo, ktorým sa dostaneme späť do hlavného menu
- tri tlačidlá pre zaznamenávanie sú:
  1. *Spusti* - spúšťanie nahrávania, toto tlačidlo sa dá zmačknuť iba v prípade, ak je nahrávanie pozastavené alebo zastavené
  2. *Pozastav* - slúži na pozastavenie nahrávania, na rozdiel od *zastavenia* neponúka uživatelovi commit-núť zmeny
      - uživatel si sám určí, čo preňho *pozastavenie* znamená
      - na rozdiele medzi zastavením a pozastavením záleží aj jedna z výsledných štatistík, viac v sekcii *Prezeranie výsledkov*
      - tlačidlo pozastavenia je možné stlačiť iba v prípade že je zapnuté nahrávnanie
  3. *Zastav* - slúži pre zastavenie nahrávania
      - pri zapnutom *repozitárovom* móde, po zastavení nahrávania vyskočí pre uživatela ponuka spravenia commit-u
      - pri *lokálnom* móde sa len jednoducho zastaví nahrávanie
      - zastaviť nahrávanie je možné aj v prípade pozastavenia aj počas behu nahrávania
- *track bar* pre nastavovanie fáze môžeme nastaviť do troch rôznych polôch
  - informácia o fáze programovania sa ukladá pre jednotlivé *spustenia* nahrávania
  - nedá sa preto meniť počas spusteného nahrávania, iba počas pozastavenia alebo zastavenia
  - (uvdím, či sa mi podarí informáciu o fáze programovania nejak zahrnúť do zobrazenia výsledkov)
- pomocou tlačidiel na spodu sa dokážem dostať späť do hlavného menu alebo do nastavení
- to môžem učiniť v akejkoľvek fáze nahrávania, či už pri spustení, pozastavení alebo zastavení nahrávania

#### Commit-ovanie

- po zastavení nahrávania pri repozitárovom móde sa otvorí okno, ktoré sa uživatela opýta, či nechce commit-núť svoje zmeny v programe
- pokiaľ uživatel neche, vyskočí ešte dialógové okno, ktorým uživatel potvrdí túto voľbu
  - ak sa uživatel napriek tomu rozhodne ne-commit-núť svoje zmeny, vráti ho aplikácia späť do okna nahrávania
- pokiaľ sa rozhodne commit-núť svoje zmeny, zavolá sa externá aplikácia **TortoiseGit**, pomocou ktorej bude môcť uživateľ tento úkon spraviť
- po spravení tohto úkonu sa externá aplikácia znova zavrie a uživatel bude vrátený do okna  nahrávania
- ak by sa úkon nezdaril alebo by sa uživatel rozhodol, že predsa len nechce commit-ovať, otvorí sa opäť dialógové okno podobne ako v predchádzajúcom prípade, ktoré sa dotazuje uživatela, či vážne nechce commit-nuť, ak by chcel, otvorí sa znova aplikácia TortoiseGit, ak nie, vráti sa uživatel do okna nahrávania

#### Prezeranie výsledkov

- z hlavného menu sa pomocou príslušného tlačidla dostaneme do okna prezerania výsledkov
- dané tlačidlo sa dá stlačiť iba v prípade, že je uživateľom vybraný valídny projekt
- v danom okne môžeme vidieť kolonku pre zobrazovanie commit-ov a grafy a rôzne číselné údaje popisujúce samotný postup v programovaní
- v kolonke pre zobrazovanie commit-ov (ak už máme vybraný projekt a máme zapnutý repozitárový mód) si môžeme nastaviť deň, z ktorého si chceme zobraziť commit a náslende pomocou *scroll-bar-u* môžeme listovať commit-y v danom dni
- prvý graf ukazuje prácu na projekte
  - pomocou track-bar-u možeme meniť medzi tromi rôzymi mierkami, teda či chceme vykresliť graf po dňoch, 7-mich dňoch alebo mesiacoch
- druhý graf nám vykresľuje priemerné množstvo práce, ktoré sme spravili v jednotlivých hodinách dňa
- čísla ukazujú celkový počet hodín, strávených na práci na projekte, jedno hovorí o čistom čase práce a druhé započítava do výsledku aj dobu pozastavenia nahrávania
- následne sa tu ešte nachádza aj možnosť nastavenia dvoch dátumov, ktorými môžeme zvoliť časový interval, z ktorého chceme aby sa prepočítali výsledné hodnoty a grafy, defaultne sú nastavené na celkvoú džku nahrávania(teda od prvého záznamu až po posledný)
- pomocou príslušného tlačidla na spodu, sa môžeme dostať späť do hlavného menu

## Vypnutie aplikácie

- aplikácia sa vypína jednoduchým stlačením tlačidla pre uzatvorenie okna
- pokiaľ je náhodou nahrávanie v spustenom alebo pozastavenom stave, vyskočí na uživatela upozornenie, že stav nahrávania nieje zastavený a či naozaj chce odísť z aplikácie

## Ďalšie poznámky

- stav uživatelom vybraných priečinkov (cesta ku TortoiseGit a ku projektu) sa kontroluje vždy pri spustení aplikácie a taktiež vtedy, keď uživatel vstúpy do aplikácie, teda stane sa aplikácia aktívnou, čo by malo zabraňovať pádom aplikácie z dôvodu nevalidných údajov
  - vždy, keď nastane problém, vyskočí uživatelovi okno s informáciou o probléme a nechá ho, nech dané nevalidné cesty opraví
- ak náhodou začne uživatel daný projekt zaznamenávať v lokálnom móde, môže kedykoľvek prepnúť na repozitárový a naopak, pri repozitárovom móde následne musia byť splnené všetky jeho požiadavky