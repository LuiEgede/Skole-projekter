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

## Teknikker brugt
### JavaScript
JavaScript bruges til:
- event handling
- asynkrone `fetch()` kald
- opdatering af DOM

### jQuery
jQuery bruges til:
- `$(document).ready(...)`
- `$("#uploadBtn").on("click", ...)`

### C#
C# bruges på serveren til:
- API-endpoints
- filhåndtering
- server-side GUI

---

## Performance
Klienten opdaterer kun fil-listen i DOM’en i stedet for at genindlæse hele siden.  
Der bruges asynkrone kald med `fetch()`, så GUI’en forbliver responsiv.

---

## Sikkerhed
Serveren validerer filnavne med `Path.GetFileName(...)` for at undgå 'farlige' stier.  
Brugerinput bliver ikke sat direkte ind som HTML, men vises og håndteres kontrolleret.

---

## Bemærkninger
Klienten har ikke delete-funktion.  
Delete er kun tilgængelig på server-siden, som fungerer som admin-side.