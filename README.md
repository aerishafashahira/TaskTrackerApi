# Task Tracker API

A RESTful Task Tracker API built with **ASP.NET Core Web API and .NET 8**. The API allows users to manage projects and their associated tasks through standard CRUD operations.

## Features

- Manage projects: create, retrieve, update, and delete projects.
- Manage tasks: create, retrieve, update, and delete tasks.
- Retrieve all tasks belonging to a specific project.
- Associate each task with its parent project using `ProjectId`.
- Validate required project names and task titles.
- Restrict task statuses to `Todo`, `InProgress`, and `Done`.
- Return appropriate HTTP status codes for successful operations and errors.
- Explore and test endpoints using Swagger UI.
- Store sample data in memory without requiring a database.

## Technology Stack

- C#
- .NET 8
- ASP.NET Core Web API
- Swagger / OpenAPI
- In-memory collections (`List<T>`)
- Visual Studio Code

## Prerequisites

Install the following tools before running the project:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/)
- C# development extensions for VS Code

Verify that the .NET SDK is installed:

```powershell
dotnet --list-sdks
```

Make sure an SDK version beginning with `8.0` is available.

## Getting Started

### 1. Clone the repository

Replace the example URL with your actual GitHub repository URL.

```powershell
git clone https://github.com/YOUR-USERNAME/TaskTrackerApi.git
cd TaskTrackerApi
```

If the repository contains the API in a nested folder, navigate into the folder containing `TaskTrackerApi.csproj`.

### 2. Restore dependencies

```powershell
dotnet restore
```

### 3. Build the application

```powershell
dotnet build
```

### 4. Run the application

```powershell
dotnet run
```

The terminal displays the local URLs where the application is listening. Open the displayed HTTP or HTTPS URL in your browser and append `/swagger`.

For example:

```text
https://localhost:7001/swagger
```

The port may differ on your machine.

## Sample Data

The API initializes two sample projects and four sample tasks.

### Projects

| ID | Name | Description |
|---|---|---|
| 1 | Website Redesign | Redesign the company website |
| 2 | Job Application Portal | Build a portal to manage job applications |

### Tasks

| ID | Project ID | Title | Status |
|---|---|---|---|
| 1 | 1 | Create wireframes | Todo |
| 2 | 1 | Design homepage | InProgress |
| 3 | 2 | Build login page | Todo |
| 4 | 2 | Test login form | Done |

## Example Requests

### Create a project

**POST** `/api/projects`

```json
{
  "name": "Portfolio Website",
  "description": "Build my developer portfolio"
}
```

The server generates the project ID and creation timestamp.

### Update a project

**PUT** `/api/projects/1`

```json
{
  "name": "Website Redesign",
  "description": "Redesign and improve the company website"
}
```

### Create a task

**POST** `/api/projects/1/tasks`

```json
{
  "title": "Review website design",
  "description": "Collect feedback on the homepage",
  "status": "Todo",
  "dueDate": null
}
```

The project ID is taken from the URL. The server generates the task ID and creation timestamp.

### Update a task

**PUT** `/api/tasks/1`

```json
{
  "title": "Create wireframes",
  "description": "Prepare and review the website page layouts",
  "status": "InProgress",
  "dueDate": null
}
```

## Validation and Error Handling

The API uses HTTP status codes to communicate the outcome of a request.

| Status code | Meaning |
|---|---|
| 200 OK | Request succeeded |
| 201 Created | A project or task was created |
| 204 No Content | A resource was deleted |
| 400 Bad Request | Input validation failed |
| 404 Not Found | The requested project or task does not exist |

Project names and task titles must not be empty or whitespace-only. Task statuses must be `Todo`, `InProgress`, or `Done`.

The API uses ASP.NET Core's `[ApiController]` behavior for request model-binding and validation errors.

## Project Structure

```text
TaskTrackerApi/
├── Controllers/
│   ├── ProjectsController.cs
│   └── TasksController.cs
├── Data/
│   └── TaskTrackerStore.cs
├── Models/
│   ├── Project.cs
│   └── TaskItem.cs
├── Program.cs
├── appsettings.json
├── TaskTrackerApi.csproj
└── README.md
```

## Testing

Run the application and use Swagger UI to test.
