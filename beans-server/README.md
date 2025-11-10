# BEANS

A demonstration stock control api project, written in C#, .net v9, and using SignalR for real-time updates to clients.

## Pre-requisites
- .NET 9 SDK
- IDE such as Visual Studio or VS Code
- Postman or Swagger UI for API testing
- Git for version control
- (Optional) Docker for containerization

- Clone the repo and run using `dotnet run` in the project directory.


## Features
- RESTful API for managing stock items (beans)
- Real-time updates using SignalR

## API Endpoints
- `POST /stock/add`
  - Adds specified quantity of beans to stock.
  - Request body: integer (e.g., `4`)
  - Response: JSON object with updated stock count.

- `POST /stock/purchase`
  - Removes specified quantity of beans from stock if available.
  - Request body: integer (e.g., `2`)
  - Response: JSON with updated stock or error message if insufficient stock.

- `GET /stock/total`
  - Returns current total stock count.
  - Response: JSON object with stock number.

## Testing
- Use Swagger UI at `https://localhost:{port}/swagger` for interactive API tests.
- Alternatively, use the provided `.http` file for automated API call scripts.

**Prerequisites:**
- xUnit testing framework
- Moq mocking library
- xUnit Visual Studio test runner


**Running Tests:**
- In Visual Studio: Use __Test Explorer__ (Test > Test Explorer) or run __Test > Run All Tests__
- Command line: `dotnet test` from the solution directory
- Individual test: `dotnet test --filter "MethodName"`

**Test Coverage:**
- `AddStock_IncreasesStock` - Validates stock addition and SignalR notifications
- `PurchaseStock_DecreasesStock` - Validates stock reduction when purchasing
- `GetTotalStock_ReturnsStock` - Validates current stock retrieval

All tests include proper mocking of SignalR hub context to isolate controller logic.