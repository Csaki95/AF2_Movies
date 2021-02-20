# Alkfejl II. kötelező program II. (2020)
 Bakancslista Film

## Feladat

 A feladat egy filmes bakancslista MVC alkalmazás létrehozása WinForms, és SQLite adatbázis segítségével.

 ### Eredeti kiírása

- Egy filmes bakancslista alkalmazásának elkészítése C# nyelven WinForms környezetben, mely követi az MVC modellt - ide kerülnek azok a filmek, amit még nem volt ido megnézni a sok ZH-ra/vizsgára tanulás miatt.
- Az adatok SQLite adatbázisban tárolandók.
- Az adatbázis sémáját bakancslista.sql tartalmazza.

#### Film felvétele

Új ablakban kell lennie a hozzáadásnak. Egy film a következo tulajdonságokkal
bír:
| Név | Típus | További információ |
| --- | --- | --- |
| Cím | szöveg | nem üres, egyedi |
| Kategória | szöveg | nem üres |
| Kiadás éve | szám | pozitív |
| Film hossza | szám | pozitív |
| Prioritás | szám | pozitív |

#### Böngészés

- A megnézendo filmeket lehet listázni, viszont a prioritás ne látszódjon, de
érték szerint legyen rendezve a lista (magasabb szám elöl).
- A lista egy DataGridView-be jelenjen meg.

#### Film módosítása

- A listában egy filmre kattintva módosítható legyen.
    - A “film felvétele” Form-ot használva.
    - A DataGridView-ban ne lehessen össze-vissza átírni az értékeket.
- Módosítható elem: prioritás.

## Megvalósítás

### Bemenet
 Minden elvárt funkciónak működnie kell Visual Studio 19-ben 1.0.113.6-as System.Data.SQLite.Core-al.
 
### Javításban segítség
 Main ablakban az isVisible konstans módosításával látható a táblázatban az ID, és a prioritás csak azt kell átírni ellenőrzéshez.
 AddMovie ablakban az editFieldEnabled konstanssal lehet engedélyezni a mezőket módosításnál, de nem fogja úgy se menteni csak a prioritást.