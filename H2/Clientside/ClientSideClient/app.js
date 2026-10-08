const apiBaseUrl = "http://localhost:5178/api/files";

$(document).ready(function () {
    loadFiles();
    $("#uploadBtn").on("click", uploadFile);
});

async function loadFiles() {
    $("#loadStatus").text("Loading files...");

    try {
        const response = await fetch(apiBaseUrl);
        if (!response.ok) {
            throw new Error("Failed to load files.");
        }

        const files = await response.json();
        renderFiles(files);
        $("#loadStatus").text(`Loaded ${files.length} file(s).`);
    } catch (error) {
        $("#loadStatus").text("Could not load files.");
        console.error(error);
    }
}

async function uploadFile() {
    const fileInput = document.getElementById("fileInput");
    const file = fileInput.files[0];

    if (!file) {
        $("#uploadStatus").text("Please choose a file first.");
        return;
    }

    const formData = new FormData();
    formData.append("file", file);

    $("#uploadStatus").text("Uploading file...");

    try {
        const response = await fetch(`${apiBaseUrl}/upload`, {
            method: "POST",
            body: formData
        });

        if (!response.ok) {
            throw new Error("Upload failed.");
        }

        const result = await response.json();
        $("#uploadStatus").text(result.message || "File uploaded successfully.");
        fileInput.value = "";
        await loadFiles();
    } catch (error) {
        $("#uploadStatus").text("Upload failed.");
        console.error(error);
    }
}

function renderFiles(files) {
    const fileList = $("#fileList");
    fileList.empty();

    if (files.length === 0) {
        fileList.append("<li>No files available on the server.</li>");
        return;
    }

    files.forEach(file => {
        const downloadUrl = `${apiBaseUrl}/download/${encodeURIComponent(file.fileName)}`;
        const row = `
            <li>
                <div>
                    <strong>${file.fileName}</strong><br />
                    <small>${file.sizeInBytes} bytes</small>
                </div>
                <div class="file-actions">
                    <a class="file-link" href="${downloadUrl}">Download</a>
                </div>
            </li>`;

        fileList.append(row);
    });
}