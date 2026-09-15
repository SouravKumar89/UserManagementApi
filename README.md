# User Management API

A simple ASP.NET Core Web API for managing users with CRUD operations, validation, logging middleware, and API key authentication.

## Features

* **CRUD Operations** – GET, POST, PUT, and DELETE users.
* **Validation** – Validates user name, email, and age using Data Annotations.
* **Logging Middleware** – Logs HTTP method, request path, response status, and execution time.
* **Authentication Middleware** – Validates an API key before allowing requests.
* **Swagger/OpenAPI** – Provides an interactive API testing interface.
* **In-Memory Storage** – User data is stored in a static list for demonstration purposes.

## Project Structure

```text
UserManagementApi/
├── Controllers/
│   └── UsersController.cs
├── Middleware/
│   ├── LoggingMiddleware.cs
│   └── AuthenticationMiddleware.cs
├── Models/
│   └── User.cs
└── Program.cs
```

## Getting Started

Create and run the project:

```bash
dotnet new webapi -n UserManagementApi
cd UserManagementApi
dotnet run
```

Open Swagger:

```text
https://localhost:<port>/swagger
```

**API Requests:** You can also use the `requests.http` file included in the project to test the APIs directly from VS Code. This requires the **REST Client** extension to be installed.

## API Endpoints

| Method | Endpoint          | Description    |
| ------ | ----------------- | -------------- |
| GET    | `/api/users`      | Get all users  |
| GET    | `/api/users/{id}` | Get user by ID |
| POST   | `/api/users`      | Create a user  |
| PUT    | `/api/users/{id}` | Update a user  |
| DELETE | `/api/users/{id}` | Delete a user  |

## Authentication

API requests require the following header:

```text
X-API-Key: user-manage-secret-key
```

Requests without a valid API key return:

```text
401 Unauthorized
```

## Validation

User data is validated using Data Annotations:

```csharp
[Required]
[StringLength(50, MinimumLength = 2)]
public string Name { get; set; }

[Required]
[EmailAddress]
public string Email { get; set; }

[Range(18, 100)]
public int Age { get; set; }
```

Invalid data returns:

```text
400 Bad Request
```

## Middleware Pipeline


The **Logging Middleware** records request and response information, while the **Authentication Middleware** verifies the API key before the request reaches the controller.

## Example POST Request

```http
POST /api/users
X-API-Key: user-manage-secret-key
Content-Type: application/json
```

```json
{
  "name": "John",
  "email": "john@example.com",
  "age": 28
}
```

## Technologies

* C#
* ASP.NET Core Web API
* .NET
* Swagger/OpenAPI
* Custom Middleware
* Data Annotations
* Dependency Injection
