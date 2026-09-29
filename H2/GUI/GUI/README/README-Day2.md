# Day 2 – Event-handling og UI design pattern

## Event-handling
Jeg har implementeret event-handling i `Home.razor`, hvor brugerens handlinger styrer siden:

- `OnTableChanged(...)` når en tabel vælges
- `SelectRow(...)` når en række vælges
- `AddRow()` når der oprettes data
- `UpdateRow()` når data opdateres
- `DeleteRow()` når data slettes
- `OnLanguageChanged(...)` når sproget skiftes

Når en event sker, opdateres kun den relevante del af UI’et. Det fungerer som delvis rendering.

## UI design pattern
Jeg har brugt et **Master-Detail** pattern:

- **Master** = valgt tabel og tabellens rækker
- **Detail** = formularen til oprettelse og redigering af en række

Det gør brugerflowet ensartet for alle tabeller.

## Delvis rendering
Løsningen bruger delvis rendering, fordi Blazor kun opdaterer den del af siden, der ændrer sig.  
Når jeg skifter tabel eller redigerer data, bliver resten af siden ikke genindlæst.

## Sprogskift
Sprogvalget ligger i `selectedLanguage`, og det bliver ikke nulstillet, når der vælges en ny tabel eller arbejdes med data.  
Derfor bevares dansk/engelsk visning, selv når tabellen skiftes.

## Hvor det er implementeret
- `Components/Pages/Home.razor` → events, UI og master-detail
- `Repositories/DatabaseRepository.cs` → CRUD mod databasen