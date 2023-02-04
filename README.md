
# szolanc

 
A projekt egy szóláncot alkot a megadott szavakból bizonyos szabályok szerint.

  

## A probléma a matematika nyelvén

Reprezentáljuk a feladatot egy **gráfelméleti problémaként**. Vegyünk egy egyszerű gráfot. A csúcsok jelentik a szavakat, az élek pedig a szavak közti átalakíthatóságot - azaz két csúcs akkor lesz összekötve, ha a két szó egymásba átalakítható, azaz állhatnak egymás mellett a szóláncban. Mivel ekkor a gráfban bármely két szomszédos szó állhat egymás mellett a szóláncban és az összes szót fel kell használnunk, a következőképpen egyszerűsödik le a probléma: 

**adjunk meg egy Hamilton-utat a gráfban, ha létezik. **

Ez egy megoldható, NP-teljes probléma.

### Mikor lesz két szó egymásba átalakítható?
Akkor, ha az első szóból a másodikat megkaphatjuk úgy, hogy
 - hozzáadunk egy betűt,
 - kitörlünk egy betűt vagy
 - módosítunk egy betűt.

Pontosan erről a három dologról szól a [Levenshtein-távolság](https://en.wikipedia.org/wiki/Levenshtein_distance) fogalma is. 
Akkor lesz két szó tehát átalakítható, ha a Levenshtein-távolságuk maximum egy.

A megoldásom menete tehát így alakult:

 1. Beolvasom a bemenetet, majd feldarabolom szavakra (ha hibás volt a bemenet, hibát dobok/írok ki)
 2. Létrehozom a gráfot a fentiek alapján
 3. Keresünk a gráfban egy Hamilton-utat; jobb algoritmus híján a csúcsok összes permutációjára megnézem, hogy Hamilton-út-e.
 4. Ha találunk Hamilton-utat, kiírjuk a hozzátartozó szóláncot, ha pedig nem, akkor pedig kiírjuk, hogy nem alkotható szabályos lánc

## Felhasznált NuGet-package-k
Mivel nem szerettem volna feltalálni a spanyolviaszt, a jól körülhatárolt részproblémákra NuGet-package-eket (is) használtam, mégpedig ezeket: 

 - [Fastensthein](https://github.com/DanHarltey/Fastenshtein) - a szavak Levenshtein-távolságának megállapítására
 - [Kaos.Combinatorics](https://github.com/DanHarltey/Fastenshtein) - a permutációk generálására
 - [QuikGraph](https://github.com/KeRNeLith/QuikGraph) - a gráf létrehozására és tárolására
