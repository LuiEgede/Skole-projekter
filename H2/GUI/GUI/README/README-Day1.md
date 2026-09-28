## Applikationstype
Dette projekt er en **webbaseret applikation** bygget med Blazor.  
En webbaseret applikation kører i browseren og kommunikerer med en server.  
Det er forskelligt fra:

- **Native applikationer**: installeres og kører direkte på operativsystemet, fx en Windows desktop-app.

- **Embedded applikationer**: er indbygget i enheder som biler, printere eller microcontrollere.

---

## Rendering og delvis rendering
**Rendering** betyder, at brugergrænsefladen bliver "tegnet" ud fra kode og state.

**Delvis rendering** betyder, at kun en del af siden opdateres i stedet for at hele siden genindlæses.  
I dette projekt opdateres kun den del af komponenten, der viser tabellen eller teksten, når jeg vælger en tabel eller skifter sprog.

### Hvad jeg har brugt
Jeg har brugt **Blazors indbyggede rendering**.  
Blazor håndterer delvis rendering automatisk, når komponentens state ændres.

---

## Render tree og DOM
### Render tree
Render tree er Blazors interne beskrivelse af, hvordan UI’et skal se ud.  
Når state ændrer sig, laver Blazor et nyt render tree og sammenligner det med det forrige.

### DOM
DOM (Document Object Model) er browserens struktur for siden.  
Når Blazor opdager ændringer i render tree, opdaterer den kun de nødvendige dele af DOM’en.

### I dette projekt
Det er derfor tabellen kan opdateres uden at hele siden genindlæses.  
Blazor opdaterer komponenten, sammenligner render tree og ændrer derefter kun de nødvendige DOM-elementer.

---

## Delvis rendering i min GUI
Jeg har implementeret delvis rendering i `Home.razor`.

### Valg af tabel
Når brugeren vælger en tabel, køres denne metode:

private void OnTableChanged(ChangeEventArgs e)
{
    selectedTable = e.Value?.ToString() ?? string.Empty;
    tableData = string.IsNullOrWhiteSpace(selectedTable)
        ? Enumerable.Empty<TableRow>()
        : DatabaseRepository.GetTableData(selectedTable);
}