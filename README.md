# CRUZ_10082026

A RESTful web service in C# that securely processes uploaded CSV files and returns the results.

## Features

- Upload and process CSV files
- Calculate the count and average of the Score column
- View the history of processed files
- API key authentication
- Swagger UI for testing
- Docker support

## Required Tools

- Docker
- Visual Studio 2026

## Endpoints
<img width="1455" height="204" alt="image" src="https://github.com/user-attachments/assets/62dd6ae7-fea1-414f-94db-6309dfb4ff3b" />

Description: 
- `POST /api/Files/process-csv` - processes a CSV file and returns the result
- `GET /api/Files/report` - returns the history of processed files

## API Key

<img width="1564" height="713" alt="AdobeExpressPhotos_7167e74f9b5b4a5392bd7fe8339ace0c_CopyEdited" src="https://github.com/user-attachments/assets/4e066284-696e-4bab-8759-4d9925d1f691" />

The API requires an `X-API-Key` header.

For local development, the API key is configured in `appsettings.json`.

Swagger can be used to provide the API key through the **Authorize** button.

Default Dev Key : 12345 

## Run locally

Open the project in Visual Studio and run it.

Make sure you are using HTTPS.

Swagger will open at:

```text
https://localhost:<port>/swagger
```
### CSV Format

The CSV file must contain a `Score` column.

```csv
Name,Score
Name1,1
Name2,2
Name3,3.5
Name4,4
Name5,5
