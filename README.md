# Book Management System (BMS) Backend

This project is a simple Book Management System (BMS) designed with scalability, maintainability, and testability in mind. It leverages **Clean Architecture** principles, ensuring clear separation of concerns and modularity across the codebase.

## High-Level Overview

The system provides an API for managing books and users, supporting operations such as creating, updating, retrieving, and deleting book records. The architecture emphasizes best practices including domain-driven design, Command Query Responsibility Segregation (CQRS), and robust exception handling.

## Architectural Patterns and Structure

### Clean Architecture Layers

This project is structured into the following layers:

- **Domain**: Contains core business entities and logic (e.g., `Book`, `User`).
- **Application**: Houses use cases, CQRS handlers (commands & queries), business rules, and service interfaces.
- **Infrastructure**: Implements external concerns such as data persistence (repositories), database context, authentication, and integrations.
- **API**: The entry point exposing HTTP endpoints, handling requests via controllers.

Each layer depends only on the inner layers, following the dependency rule, and communicates through interfaces and contracts.

### Patterns and Tools Used

- **CQRS**: Separates read (query) and write (command) operations for greater flexibility and scalability.
- **Mediator Pattern (`MediatR`)**: Decouples request handling; controllers send commands/queries through a mediator, which dispatches them to appropriate handlers.
- **Repository Pattern**: Abstracts data access logic using generic repositories, promoting testability and encapsulation.
- **Result Pattern**: All commands and queries return a standardized `Result<T>` object, simplifying consistent exception and error handling.
- **Serilog + Seq**: Structured logging with support for rich log data, correlation, and visualization using [Seq](https://datalust.co/seq).
- **Global Exception Handling**: Middleware and pipeline behaviors ensure all exceptions are captured, logged, and returned as standard responses.
- **Validation and Logging Pipelines**: Pipeline behaviors with MediatR for request validation and logging before/after each handler execution.

## Key Technologies

- **.NET 8**
- **C#**
- **Entity Framework Core** (with in-memory DB for sample/demo)
- **Serilog** (logging) + **Seq** (log aggregation)
- **MediatR** (CQRS and mediator pattern)
- **FluentValidation** (for validating requests)
- **JWT Authentication** (for user auth; infrastructure scaffolding present)

## Example: Flow of a Request

1. **API Layer**: `BooksController` receives an HTTP request and creates a command/query object.
2. **MediatR**: The request is sent to MediatR, which routes it to the appropriate handler in the Application layer.
3. **Application Layer**: Handler orchestrates domain logic, interacts with repositories (infrastructure), and returns a `Result<T>`.
4. **Infrastructure Layer**: Handles data access (via generic repository), authentication, and external integrations.
5. **Logging/Validation**: Every step is logged via Serilog (with logs sent to Seq), and requests are validated before processing.

## Project Structure Snapshot

```
bms.Domain/          # Entities and domain logic
bms.Application/     # Use cases, CQRS handlers, contracts, validation
bms.Infrastrcture/   # Repositories, DbContext, authentication, logging
bmsAPI/              # Web API, controllers, middleware
```

## Getting Started

- Clone the repository and restore dependencies.
- The system uses an in-memory database by default for quick setup.
- Ensure you have a Seq server running for structured log collection (default: `http://localhost:5341`).
- Run the API project (`bmsAPI`) and explore endpoints via Swagger UI.

---

**This project serves as a clean, extensible foundation for book management scenarios and can be extended easily for more complex use cases.**
