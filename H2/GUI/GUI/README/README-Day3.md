# Day 3 – Lifetime-events, async og custom control

## Lifetime-events 
Den lifecycle-metode er `OnInitialized()`, som bruges til at hente data, når komponenten starter.

## Custom control
Jeg har lavet en custom control i `Components/DatabaseStats.razor`.  
Den fungerer som et lille database-statuspanel og viser:

- hvor mange tabeller der findes
- hvor mange rækker der er i hver tabel

Komponenten bliver vist på `Home.razor`, når der ikke er valgt en tabel.

## Live data
Custom controlen henter data direkte fra databasen via `DatabaseRepository`.  
Den bruger metoderne:

- `GetTables()`
- `GetRowCount(tableName)`

Det betyder, at visningen altid bygger på aktuelle data fra databasen.

## Automatisk opdatering
Når der oprettes, opdateres eller slettes data i `Home.razor`, kaldes `databaseStats?.Refresh()`.  
Det betyder, at custom controlen opdaterer sin visning, så antallet af rækker matcher den aktuelle database.