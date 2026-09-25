## 1. Performance-analyse
Jeg har fokuseret på de forespørgsler, der søger og filtrerer data, især ved opslag på kunder, biler og værkstedsordrer. Det er de typer queries, der oftest bruges i programmet, og derfor kan de have betydning for performance, hvis tabellerne vokser.

Jeg har valgt at optimere de kolonner, der ofte bruges til opslag, Email og LicensePlate. Det gør, at databasen hurtigere kan finde data uden at skulle gennemgå hele tabellen.

## 2. Indexering
I min løsning fungerer primærnøglerne som clustered index, fordi MySQL organiserer data effektivt omkring dem. Derudover har jeg lavet nonclustered indexes på kolonner som Email og LicensePlate, fordi de ofte bruges til søgning.

Koden til at lave indekserne:
CREATE INDEX idx_customers_email ON Customers(Email);
CREATE INDEX idx_cars_licenseplate ON Cars(LicensePlate);