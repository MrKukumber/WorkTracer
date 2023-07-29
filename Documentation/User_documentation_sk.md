# Uživatelská dokumentácia - Work Tracer

## Popis

---

- Aplikácia s grafickým uživatelským rozhraním slúžiaca na zaznamenávanie času strávenom na práci na danom projekte.  

---

- Obsahuje prezeranie výsledkov, nastavenie jazyka a módu nahrávania a prezeranie commitov uživatela v danom projekte
- ako mód projektu si uživateľ môže vybrať z dvoch možností
  - *lokálny mód*
    - vybraný projekt nemusí obsahovať git-ový repozitár
    - nieje potrebné, aby bol vybraný priečinok s Tortoise Git aplikáciou
    - okná zobrazujúce commity budú vypisovať príslušnú hlášku, že je nastavený lokálny mód
  - *repozitárový mód*
    - vybraný projekt by musí byť zahrnutý v nejakom gitovom repozitáry
    - je potrebné, aby bol vybraný priečinok s Tortoise Git aplikáciou *
    - vyšie zmienené okná budú ukazovať príslušné commity projektu
- za jazyk si uživatel môže vybrať z:
  - *anglického*
  - *slovenského*

> \* pokiaľ chce užívateľ pracovať aj v repozitárovom móde, musí mať pred spustením nainštalovanú aplikáciu [TortoiseGit](https://tortoisegit.org/download/), ideálne verzie 2.14.0, pri novších a starších verziách nieje zaručená funkčnosť aplikácie 

## Zaobchádzanie s aplikáciou

### spustenie aplikácie

- aplikácia sa súšťa pomocou spustitelného súboru .exe alebo odkazu naňho
- po spustení sa objaví na obrazovke hlavné menu

### hlavné menu

<img src="screen_shots\main_form_screen_shot.jpg" width=350>

- vydíme tu tri tlačidlá a okno zobrazujúce posledný commit projektu (momentálne s nápisom oznamujúcim, že je zapnutý lokálny mód)
- tlačidlá slúžia na:
  - *Začni nahrávať* - zobrazí uživatelovi okno, kde môže začať zaznamenávať svoju prácu
  - *Pozri postup práce* - otvorí sa okno, kde môže uživateľ prezerať výsledné hodnoty nahrávania
  - *Otvor nastavenia* - otvorí okno nastavení

- ďalej sa tu nachádzajú nápisi oznamujúce aktuálny stav a mód nahrávania
- taktiež pod tlačidlom *Otvor nastavenia* sa v prípade nutnosti  zobrazia nápisy označujúce neplatnosť niektorého z dvoch zdrojových priečinkov

### nastavenia

<img src="screen_shots/config_form_screen_shot.jpg" width = 350>

- nachádzajú sa tu 4 možnosti nastavenia aplikácie:
  - **Vyber mód** - pomocou *track bar*-u si môžeme vybrať medzi repozitárovým a lokálnym módom
  - **Nastav jazyk** - znova pomocou track bar-u si môžeme nastaviť jazyk aplikácie podľa svojej ľúbosit
  - **Vyber adresár s tortoise git**
    - po zmačknutí tlačidla *Vyber* sa otvorí dialógové okno, v ktorom uživatel môže vybrať priečinok, v ktorom je umiestnený spustitelný súbor *TortoiseGitProc.exe*
    - daný súbor sa bude najskôr nachádzať v *cesta_ku_priecinku_s_TortoiseGit\Nazov_priecinka_vybrany_pri_instalacii\bin* (napr.: C:\Program Files\TortoiseGit\bin)
    - nemusí byť vybraný v lokálnom móde
  - **Vyber adresár s projektom**
    - po zmačknutí tlačidla *Vyber* sa otvorí dialógové okno, v ktorom si uživatel môže vybrať priečinok s projektom, ktorý bude chcieť zaznamenávať
    - pokiaľ je zapnutý repozitárový mód, je potrebné, aby priečinok bol súčasťou nejakého git-ového repozitára
      - ak nieje súčasťou žiadneho repozitára, opýta sa aplikácia uživatela, či tam chce vytvoriť nový repozitár
- tlačidlom **Vráť sa** sa vrátime do okna, z ktorého sme do nastavení prišli

- pokiaľ je niektorý zo zdrojov nevalídny, vypíše sa cesta k danému zdroju červeným písmom a ukáže sa správa oznamujúca tento problém

### nahrávanie

<img src="screen_shots/recording_form_screen_shot.jpg" width = 250>

- v okne sa nachádzajú tri tlačidlá pre pre nahrávanie, track bar pre výber vývojových fází a dve tlačidlá pre vstup do nastavení (**Otvor nastavenie**)a pre návrat do hlavného menu (**Vráť sa do hlavného menu**)
- tlačidlá pre náhrávanie z ľava sú:
  1. tlačidlo pre **spustenie** nahrávania
  2. tlačidlo pre **pozastavenie** nahrávania
  3. tlačidlo pre **zastavenie nahrávania**



