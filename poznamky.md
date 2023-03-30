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
    - musim skontrolovat ci mam nastaveny tortoise git folder a subor projektu - ak nieje nieco nastavene, odnaviguje uzivatela na miesto kde to ma nastavit pomcou *MessageBox*
      - tortois kontrolujem iba ak som v repo mode
    - kontrolujem ci existuje csv-sko na zapisovanie, ak nie, vytvorim
2. *ConfigFormOpening_button* - otvori configuracny formular
    - dva labely pod buttonnom sluzia na upozornenie, ze nieje vybrany source projekt a source tortoise-git-u
3. *ChooseCommit_button* - ukaze commit vybraneho datumu v richTextBox-e
    - budem si vazne vyberat len datum a ukaze mi to prvy commit tohto datumu
    - nasledne tam budem mat sipky, ktorymi budem moct listovat medzi commitmi tohto datumu

- pridam label, ktory mi bude vraviet v akom stave recordingu akurat som, nakolko pocas recordingu budem moct vyjst z record formulara
- zaroven este pridam Mode_label, ktory mi hovori v ktorom mode som - ci local alebo repo

### v recordingu

1. *Play_roundButton* - zapise do csv suboru ze sa zacalo trackovanie
    - zmeni sa vnutorny stav na play-stav
2. *Pause_roundButton* - zapise sa do csv suboru zastavenie trackovania
    - zmeni sa vnutorny stav na zastavenie
3. *Stop_roundButton* - pri repo mode:
    - otvori sa okno s otazkou, ci chcem commitnut
      - ak nechem, zapise sa do csv suboru zastavenie trackovania s not commit indikatorom a vratim sa do **recordingu**
      - ak chcem commitnut, tak otvorim Tortoise git,
        - po jeho zavreti pozriem, ci sa vazne commitlo:
        - ak nie, tak znova otvorim okno s tym ci chcem commitnut
        - ak ano, zapisem zaznam do csv-cka a dam mu indikator commitu, vratim sa do **recordingu**
    - pri nie repo mode - proste zapisem do csv suboru a zostavam **recordingu**
    - zmenim sa vnutorny stav na stop-stav
4. *ReturnToMain_button* - vrati ma do main manu, moze ma vratit kedykolvek, teda za akehokolvek stavu trackovania
5. *ConfigFormOpening_button* - otvori configuracny formular

- nebudem zanechavat csv-cko v nejakom konzistentnom stave, proste co tam posledne uzivatel zada, to tam bude, ked tak sa ho len mozem opytat, ked bude chciet zavriet okno, ci nechce zastavit nahravanie
- vzdy ked zaktivnim dane okno...a.k.a. kliknem nanho, tak skontrolujem directory csv-cka, tortoise gitu a aj projektu, popripade upozornim uzivatela na to ze sa zmenili direktory, ze si to musi upravit..
- dokym niesu vsetky adresare spravne nastavene, nenecham ho stlacit ziadne trackovacie tlacidlo

### v configuracii

1. *ChooseTGitDir_button* - otvori mi file manager, kde si mozem najst folder s projektom
    - nejakym sposobom musim skontrolovat, ci to je vazne subor s tortosiom...
    #TODO
2. *ProjectSelection_Button* - otvori mi file manager, v ktorom si vyberiem folder s projektom, bud:
    1. vyuzivam repo
        - to ze dany projekt vyuziva git repozitar zistim tak, ze sa pozriem ci obsahuje .git folder
          - ak sa tam nenachadza, tak sa opytam uzivatela, ci z toho chce spravit repozitar - pytam sa uz vtedy, ked uzivatel vybera dany projekt - napisem aj radu, ze ak neche vyuzivat repo, tak nech si to ide prestavit do nastaveni, ak si ho tam nevytvori, stale bude napisane ze si nevybral repo
          - pytam sa otvorenim CreateRepo_form-u.cs
    2. nevyuzivam repo
        - nemusim kontrolovat git, cize sa ani nepytam ci chcem vytvorit adresar, proste ten subor ktory vyberie, tam potom ulozim to moje csv-cko
    - skontrolujem ci na prisluchajucom mieste je csv-cko, ked tak upozornim na to v NoCSVAttention_label, ale inak neriesim
    - nastavim stav nahravania podla posledneho zanzmu v csv-cku, ak sa tam ziadne csv-cko nenachadza, nastavim ho na noTrackingTable stav
3. *checkBoxy sk,en* - na menenie jazyka, vzdy len jeden zakliknuty, lokalizaciu skusim aplikovat instantne po zakliknuti...
4. trackBar - menenie medzi repo a local modom
    - kotrolujem projekt, csv-cko a popripade tortoise git ulozisko
5. *BackToPrevForm_button* - po zmacknuti sa vratime z configu do predosleho otvoreneho okna, ci uz main alebo recording okna

- moznost zmenit umiestnenie tortoise-gitu
  - mozno nejakym sposobom upresnit, ktory folder presne ma uzivatel vybrat
- moznost zmenenia lokalizacie
- moznost zmeny trackovacieho modu
- vsetko co nieje platne vybrane sfarbim na cerveno
- skontrolovat ci uz sa tam csv nachadza v danom projekte a popripade dat prec upozornenie, ak tam 
- vypisovat v labeloch iba meno projektu, nie celu path k nemu..., ak nevalidny projekt, tak vypisovat No valid project cervene napriklad.

- **vsetky nastavenia si ukladam tiez do config textaku**

## Myslienky

- zaznamy casov budem ukladat do .git file-u ak je repo mod zapnuty, inak priamo do suboru projektu, nemusim to robit zlozitejsie ako to je
- prvotne nastavenia spravim priamo v aplikacii, nebudem sa zatazovat ziadnymi config vecmi

- vytvorit triedu na kontrolovanie zdrojov, ktora bude mat nejake funkcie, ktore budu vracat error kody v zavislosti na tom co sa pokazilo, co uz neplati, alebo naopak ci je vsetko v pohode

- formulare si inicializovat v programe a ulozit si ich a otvarat stale tie iste, nevytvarat nove stale

## co treba vzdy kontrolovat

- pri zapisani do csv-cka - kontrolovat, ci je csv stale tam kde ma byt
- pri praci s repozitarom:
  - ci sa mi nezmenilo miesto tortoise-git-u
  - ci stale ma cestu ku .git - file-u

- vzdy ked prechadzam z mainu do recording formulara, kontrolujem vsetky spomenute zdroje
- vzdy ked kliknem enternem akekolvek okno aplikacie, zkontrolujem zdroje a popripade nastavim vhodne indikatory
- command na testovanie toho, ci sa nachadzam v directory s repozitorom:

```bash
git rev-parse --is-inside-work-tree
```

- ak sa nachadzam v nejakom directory a som v repo mode ale dany directory neobsahuje ihned git repozitar, backtracknem do repozitara, kde sa nachadza .git a ulozim tam csv-cko ale bude mat v nazve tu cestu nejako....
- aebo proste dovoloim recordovat iba projekty priamo s .git repozitarom

## pri spusteni aplikacie

- nastavim vsetko podla config file-u
- kontrolujem, ci stale existuje cesta ku tortoise-git-u

1. mod repo:
    - kontrolujem ci stale existuje dany projekt a ci v nom je stale repozitar - ak niektora z tychto veci neplati, napisem, ze nieje vybrany cielovy projekt
2. mod nonrepo:
    - kontrolujem, ci existje cesta do projektu

## Zatvaranie aplikacie

#TODO

## Problemy na vyriesenie

> nekonzistencia csv-cka sposobena hrabanim sa v directory pocas nahravania - budem kontrolovat ci tam je vzdy ked kliknem do recording formularu

- ci sa mozem pocas nahravania vratit do main formularu - mozem
- jak sakra zobrazovat tie commity a tie celkove casy celkovo