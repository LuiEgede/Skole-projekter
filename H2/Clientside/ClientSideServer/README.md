# ClientSide Case

Dette projekt består af to dele:

- `ClientSideServer` – en ASP.NET Core Web API + server GUI
- `ClientSideClient` – en ren HTML/CSS/JavaScript klient

Projektet implementerer fil upload/download mellem en klient og en server.

---

## Struktur

### Server
Serveren ligger i `ClientSideServer` og indeholder:

- `Controllers/FilesController.cs`
- `Pages/Files.cshtml`
- `Pages/Files.cshtml.cs`
- `UploadedFiles/`

### Client
Klienten ligger i `ClientSideClient` og indeholder:

- `index.html`
- `styles.css`
- `app.js`

---

## Funktionalitet

### Server API
Serveren har disse endpoints:

- `GET /api/files` – henter fil-liste
- `POST /api/files/upload` – uploader fil
- `GET /api/files/download/{fileName}` – downloader fil
- `DELETE /api/files/{fileName}` – sletter fil

### Server GUI
Serveren har også en lille GUI-side på:

- `/Files`

Her kan man se filer og slette dem fra serveren.

### Client GUI
Klienten kan:

- vælge en fil og uploade den
- se alle filer på serveren
- downloade filer
- opdatere fil-listen automatisk efter upload

---

## Hvor koden er placeret

- `ClientSideServer/Controllers/FilesController.cs`  
  Indeholder serverens API-endpoints og filhåndtering.

- `ClientSideServer/Pages/Files.cshtml` og `Files.cshtml.cs`  
  Indeholder serverens GUI-side til filoversigt og delete-funktion.

- `ClientSideClient/index.html`  
  Indeholder klientens HTML-opbygning.

- `ClientSideClient/styles.css`  
  Indeholder simpel styling til klienten.

- `ClientSideClient/app.js`  
  Indeholder jQuery events, `fetch()` kald og DOM-opdatering.

---

## JavaScript og C#

JavaScript bruges til:
- event handling
- asynkrone `fetch()` kald
- opdatering af DOM

C# bruges på serveren til:
- API-endpoints
- filhåndtering
- server-side GUI

### JavaScript vs. OOP-sprog
JavaScript er fleksibelt og bruges her primært til brugerinteraktion i browseren.  
C# er et OOP-sprog og bruges til struktureret serverlogik, endpoints og filhåndtering.  
I projektet bruger jeg JavaScript til GUI og C# til backend.

---

## Controller based API vs. Minimal API
Jeg har valgt controller-based API, fordi det giver en tydelig struktur med separate controller-filer og klart definerede HTTP-metoder.

Minimal API ville have været kortere og mere direkte i `Program.cs`, men controller-based er lettere at forklare, lettere at vedligeholde og passer godt til denne opgave.

---

## Render tree og DOM
Klienten er ikke bygget i Blazor, så den bruger ikke Blazor render tree direkte.  
I stedet opdaterer JavaScript DOM’en, når `fetch()` henter data fra serveren.

Et eksempel er `renderFiles(files)` i `app.js`, som kun opdaterer `#fileList`, i stedet for at genindlæse hele siden.

Det giver en delvis opdatering af UI’et, som opfylder kravet om asynkron og dynamisk visning.

---

## jQuery
jQuery bruges til at håndtere brugerens interaktion med GUI’en:

- `$(document).ready(...)` starter klienten, når siden er indlæst
- `$("#uploadBtn").on("click", uploadFile)` håndterer upload-knappen

Det gør det tydeligt, hvordan events kobles til handlinger i GUI’en.

---

## Performance
Klienten opdaterer kun fil-listen i DOM’en i stedet for at genindlæse hele siden.  
Der bruges asynkrone kald med `fetch()`, så GUI’en forbliver responsiv.

Funktionerne er genbrugt på tværs af klienten:
- `loadFiles()` henter fil-listen
- `uploadFile()` uploader en fil
- `renderFiles()` tegner listen igen

Det giver mindre kode og færre unødvendige opdateringer.

---

## Sikkerhed
Serveren stoler ikke på rå data fra klienten.

- Filnavne renses med `Path.GetFileName(...)`
- Brugerinput bliver ikke sat direkte ind som HTML
- Filhåndteringen sker via serverens API-endpoints
- Kun serveren håndterer delete af filer

Det reducerer risikoen for manipulerede stier og usikker håndtering af input.

---

## Bemærkninger
Klienten har ikke delete-funktion.  
Delete er kun tilgængelig på server-siden, som fungerer som admin-side.

Klienten bruger `fetch()` i stedet for AJAX.