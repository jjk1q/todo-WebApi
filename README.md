# Todo API

A REST API for managing todo items, built with C# and ASP.NET Core.

This is a learning project. I'm building it in stages, starting from a plain
console application and adding one layer at a time, so that every piece is
something I wrote and understand rather than something I copied.

## Status

- [x] Stage 0 — Console application with JSON file storage
- [x] Stage 1 — Web API with a controller and full CRUD
- [x] Stage 2 — DTOs and global error handling (ProblemDetails)
- [x] Stage 3 — EF Core with SQLite
- [ ] Stage 4 — Users
- [ ] Stage 5 — JWT authentication

## Tech stack

- C# / .NET 8
- ASP.NET Core Web API
- Entity Framework Core + SQLite (stage 3)
- Swagger / OpenAPI for testing endpoints

## Architecture

Storage sits behind an interface (`ITodoRepository`) that describes the
operations on todo items: get all, get by id, add, update, delete.

The first implementation writes to a JSON file. The EF Core implementation
is added later without touching the controller, which is the point of the
interface.

Controllers never expose entities directly. Requests and responses use DTOs,
so the internal model can change without breaking the API.

## Error handling

Errors are returned as `ProblemDetails` from a global exception handler:

- `400 Bad Request` — invalid input from the client
- `404 Not Found` — the requested todo does not exist
- `500 Internal Server Error` — unexpected server-side failure

Controllers stay free of try/catch blocks.

## Endpoints

| Method | Route             | Description          |
|--------|-------------------|----------------------|
| GET    | `/api/todos`      | Get all todo items   |
| GET    | `/api/todos/{id}` | Get one todo item    |
| POST   | `/api/todos`      | Create a todo item   |
| PUT    | `/api/todos/{id}` | Update a todo item   |
| DELETE | `/api/todos/{id}` | Delete a todo item   |

## Getting started

### Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) or newer

### Run it

```bash
git clone https://github.com/jjk1q/todo-api.git
cd todo-api
dotnet run
```

The API starts on `https://localhost:5001`. Open `/swagger` in a browser to
try the endpoints.

### Example request

```bash
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d '{"title": "Buy milk", "isDone": false}'
```

## Project structure

```
todo-api/
├── Controllers/     API controllers
├── Models/          Domain models
├── DTOs/            Request and response objects
├── Repositories/    Storage interface and implementations
└── Program.cs       Startup and dependency injection
```

## License

MIT
