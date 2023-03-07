# Poznamky

## Arhitektura work tracker-u

- Program
- Main_form
- Recording_form
- Configure_form
- Commit_form
- Not_commited_exit_form

## Co chcem

### v mainu

1. *RecordingFormOpening_button* - otvori sa mi okno na nahravanie
    - musim skontrolovat ci mam nastaveny tortoise git folder a subor projektu - ak nieje nieco nastavene, odnaviguje uzivatela na miesto kde to ma nastavit
      - tortois kontrolujem iba ak som v repo mode
    - kontrolujem ci existuje csv-sko na zapisovanie, ak nie, vytvorim
2. *ConfigFormOpening_button* - otvori configuracny formular
    - dva labely pod buttonnom sluzia na upozornenie, ze nieje vybrany source projet a source tortoise-git-u
3. *ChooseCommit_button* - ukaze commit vybraneho datumu v richTextBox-e
    - #TODO

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
5. *ConfigFormOpening_button* - otvori configuracny formular

- ked odchadzam z tohot okna akymkolvek sposobom, musim zanechat zaznamy v konzistentom stave, cize musi by zastaveny zaznam, nesmie byt pusteny ani pozastaveny
- vzdy ked zaktivnim dane okno...a.k.a. kliknem nanho, tak skontrolujem directory csv-cka, tortoise gitu a aj projektu, popripade upozornim uzivatela na to ze sa zmenili direktory, ze si to musi upravit..
- dokym nies vsetky adresare spravne nastavene, nenecham ho stlacit ziadne trackovacie tlacidlo

### v configuracii

1.
2. *ProjectSelection_Button* - otvori mi file manager, v ktorom si vyberiem folder s projektom, bud:
    1. vyuzivam repo
        - to ze dany projekt vyuziva git repozitar zistim tak, ze sa pozriem ci obsahuje .git folder
          - ak sa tam nenachadza, tak sa opytam uzivatela, ci z toho chce spravit repozitar - pytam sa uz vtedy, ked uzivatel vybera dany projekt - napisem aj radu, ze ak neche vyuzivat repo, tak nech si to ide prestavit do nastaveni, ak si ho tam nevytvori, stale bude napisane ze si nevybral repo
          - pytam sa otvorenim CreateRepo_form-u.cs
    2. nevyuzivam repo
        - nemusim kontrolovat git, cize sa ani nepytam ci chcem vytvorit adresar, proste ten subor ktory vyberie, tam potom ulozim to moje csv-cko
5. *BackToPrevForm_button* - po zmacknuti sa vratime z configu do predosleho otvoreneho okna, ci uz main alebo recording okna

- moznost zmenit umiestnenie tortoise-gitu
  - mozno nejakym sposobom upresnit, ktory folder presne ma uzivatel vybrat
- moznost zmenenia lokalizacie
- moznost zmeny trackovacieho modu
- vsetko co nieje platne vybrane sfarbim na cerveno

- **vsetky nastavenia si ukladam tiez do config textaku**

## Myslienky

- zaznamy casov budem ukladat do .git file-u ak je repo mod zapnuty, inak priamo do suboru projektu, nemusim to robit zlozitejsie ako to je
- prvotne nastavenia spravim priamo v aplikacii, nebudem sa zatazovat ziadnymi config vecmi

- vytvorit triedu na kontrolovanie zdrojov, ktora bude mat nejake funkcie, ktore budu vracat error kody v zavislosti na tom co sa pokazilo, co uz neplati, alebo naopak ci je vsetko v pohode

## co treba vzdy kontrolovat

- pri zapisani do csv-cka - kontrolovat, ci je csv stale tam kde ma byt
- pri praci s repozitarom:
  - ci sa mi nezmenilo miesto tortoise-git-u
  - ci stale ma cestu ku .git - file-u

- vzdy ked prechadzam z mainu do recording formulara, kontrolujem vsetky spomenute zdroje

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

- ci sa mozem pocas nahravania vratit do main formularu
- jak sakra zobrazovat tie commity a tie celkove casy celkovo