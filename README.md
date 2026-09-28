# TEcomerc - E-commerce Technical Test

A minimal e-commerce backend implemented in .NET 10 intended as a technical test. This repository includes an API, persistence with SQLite, unit and end-to-end tests, and sample authentication using JWT.

## Project capabilities

- REST API for order management (create, read, list, update)
- Authentication endpoint that issues JWT tokens
- SQLite-backed repository implementations (EF Core)
- End-to-end tests using Microsoft.AspNetCore.Mvc.Testing
- Clean separation between Application, Domain, and Infrastructure layers

## Tech stack

- .NET 10
- C#
- ASP.NET Core Web API
- Entity Framework Core with SQLite
- xUnit for tests
- Microsoft.AspNetCore.Mvc.Testing for E2E tests

## Getting started (Visual Studio 2022)

1. Open the solution in Visual Studio 2022.
2. Ensure the .NET SDK 10 is installed.
3. Build the solution: __Build > Build Solution__ or run the __BuildSolution__ command.
4. Set the API project as the startup project and run (F5) or run without debugging (Ctrl+F5).

> **Note:** The repository uses an `.editorconfig` and `CONTRIBUTING.md` for coding standards. Follow those files for formatting and contribution rules.

## CLI instructions

From the repository root:

- Restore and build

    ```bash
    dotnet restore
    dotnet build
    ```

- Run the API

    ```bash
    dotnet run --project src/Api/Api.csproj
    ```

- Run tests

    ```bash
    dotnet test
    ```

## Configuration & Database

- The default persistence uses SQLite located under the application configuration. The database file path and migrations (if any) are configured in the infrastructure project.
- To reset the database, delete the SQLite file used by the app (path configurable) and restart the application. On first run, the app will initialize the schema.

## Authentication

- The API exposes an authentication endpoint used by the E2E tests (see tests/E2E/%auth%login/POST.cs).
- Client credentials used in tests:
  - **User:** `dev@martech.com`
  - **Password:** `Senha@123`
- Successful login returns a JSON response containing a `JWT` token string. Use this token in `Authorization: Bearer <token>` when calling protected endpoints.

## Example request (login)

**POST** `/auth/login`

POST /auth/login HTTP/1.1
Content-Type: application/json

{ "user": "dev@martech.com", "password": "Senha@123" }

## Orders API (overview)

Typical capabilities (check controllers for full routes and payloads):

- Create order
- Read order by id
- List orders with paging and optional inclusion of items
- Update order fields (status, customerId, etc.)

The infrastructure includes `SqliteOrderRepository` which persists orders and supports paging and item inclusion.

## Tests

- Unit tests and E2E tests are included. E2E tests use an in-memory test server created with `WebApplicationFactory<Program>` and target authentication and order flows.
- Run all tests with `dotnet test`.

## Development notes

- Follow the repository's `.editorconfig` and `CONTRIBUTING.md` for coding style and contribution conventions.
- The project targets .NET 10. Use the matching SDK and runtime.
- When debugging tests in Visual Studio, you can attach the debugger or run tests with the Test Explorer.

## Contributing

- Fork the repository, create a feature branch, follow code standards, add tests for new behavior, and open a pull request targeting `master`.

## License

This project is provided for evaluation and technical testing purposes. Check the repository license or policy in the root if present.