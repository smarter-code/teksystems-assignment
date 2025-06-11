# ThreadPilot

---

## 1. Architecture Overview

ThreadPilot is composed of two RESTful API microservices:

- **Vehicle Service API**: Provides vehicle information by registration number.
- **Insurance Service API**: Provides insurance information by personal identification number, and integrates with the Vehicle Service API to enrich insurance data with vehicle details.

### Solution Structure

```plaintext
ThreadPilot/
├── src/
│   ├── Core/
│   │   ├── ThreadPilot.Domain/          # Domain entities and value objects
│   │   └── ThreadPilot.Application/     # Use cases and business logic
│   ├── Infrastructure/
│   │   ├── ThreadPilot.Infrastructure/  # Cross-cutting concerns
│   │   └── ThreadPilot.Persistence/     # Data access with EF Core
│   └── API/
│       ├── ThreadPilot.VehicleService.API/
│       └── ThreadPilot.InsuranceService.API/
└── tests/
    ├── ThreadPilot.Application.UnitTests/
    ├── ThreadPilot.VehicleService.IntegrationTests/
    └── ThreadPilot.InsuranceService.IntegrationTests/
```

### Endpoints

- **Vehicle Service**: `GET /api/vehicles/{registrationNumber}`
- **Insurance Service**: `GET /api/insurances/person/{personalIdentificationNumber}`

### Integration Details

The Insurance Service API calls the Vehicle Service API using a **typed HTTP client** registered via dependency injection. This client leverages **in-memory caching** to avoid redundant network calls and improve performance. The design follows [Microsoft's recommendations for typed clients](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-9.0#typed-clients) to avoid socket exhaustion and ensure resilience.

### Database

The solution uses a **SQLite** database, which is included in the repository under the `src/API` folder as `ThreadPilot.db`. This database is automatically generated and migrated when running the Insurance Service API for the first time.

### Database Design

The following diagram illustrates the database schema:

![Database Design](https://i.imgur.com/TYGREvm.png)

---

## 2. Technology Stack & Rationale

I selected the following technologies for their robustness, maintainability, and alignment with modern .NET best practices:

- **.NET 8 / ASP.NET Core**: For high performance, cross-platform support, and modern API development.
- **Entity Framework Core**: For ORM and database migrations.
- **SQLite**: Lightweight, file-based database ideal for local development and testing.
- **MediatR**: Implements CQRS and decouples request/response logic.
- **FluentValidation**: For expressive, maintainable input validation.
- **xUnit & Moq**: For unit and integration testing.
- **MemoryCache**: For efficient in-memory caching of vehicle data.
- **GitHub Actions**: For CI/CD automation (build and test on every push).
- **Swagger/OpenAPI**: For interactive API documentation and exploration.

---

## 3. Design Best Practices

Throughout the solution, I have implemented several best practices:

- **Domain-Driven Design (DDD)**: Clear separation of domain models, value objects, and business logic.
- **CQRS (Command Query Responsibility Segregation)**: Queries and commands are handled separately, improving maintainability and scalability.
- **Dependency Injection**: All services and clients are injected, supporting loose coupling and testability.
- **Async/Await**: All I/O operations are asynchronous for scalability.
- **NoTracking Queries**: Used for read-only operations to improve performance.
- **Proper Indexing**: Ensures efficient SQL queries.
- **Centralized Error Handling**: Global exception middleware and results pattern.
- **Input Validation**: FluentValidation for robust, maintainable validation.
- **Value Objects**: Encapsulate domain concepts and enforce invariants.
- **Vertical Feature Slicing**: Organizes code by feature/use-case, not by layer.
- **Clean Architecture**: Clear separation of concerns between core, infrastructure, and API layers.
- **Typed HTTP Clients**: For integration, resilience, and to avoid socket exhaustion.
- **Caching**: In-memory caching for vehicle data to reduce latency and load.

---

## 4. Running the Solution (Step-by-Step)

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 (recommended)

### 1. Open the Solution in Visual Studio

- Open `ThreadPilot.sln` in Visual Studio 2022.

### 2. Configure API URLs and Keys

- The **Vehicle Service API** runs by default at `http://localhost:5155` (see `src/API/ThreadPilot.VehicleService.API/Properties/launchSettings.json`).
- The **Insurance Service API** runs by default at `http://localhost:5267` (see `src/API/ThreadPilot.InsuranceService.API/Properties/launchSettings.json`).
- API keys are set in each service's `appsettings.json`:
  - Vehicle Service: `X-API-Key: vehicle-service-api-key-123456`
  - Insurance Service: `X-API-Key: insurance-service-api-key-123456`
- The Insurance Service must be configured to call the correct Vehicle Service URL and API key in its `appsettings.json`:

```json
"VehicleService": {
    "BaseUrl": "http://localhost:5155",
    "ApiKey": "vehicle-service-api-key-123456"
}
```

> **Note:** For simplicity, API keys are stored in config files. In production, I recommend moving secrets to **Azure Key Vault** and integrating with **Azure App Configuration**.

### 3. Set Startup Projects

- In Visual Studio, right-click the solution in Solution Explorer and choose **Set Startup Projects...**
- Select **Multiple startup projects**.
- Set the `Action` for both `ThreadPilot.VehicleService.API` and `ThreadPilot.InsuranceService.API` to **Start**.
- Click **OK**.

### 4. Run the APIs

- Press **F5** (or click the green Start button) to launch both APIs.
- Visual Studio will open browser tabs for both services, typically at:
  - Vehicle Service: [http://localhost:5155/swagger](http://localhost:5155/swagger)
  - Insurance Service: [http://localhost:5267/swagger](http://localhost:5267/swagger)

### 5. Explore the APIs

- Use the **Swagger UI** in your browser to explore and test the endpoints for both services.
- Use the API keys above in the `X-API-Key` header for all requests (Swagger UI allows you to set headers for requests).

---

## 5. Testing Approach

I have implemented both **unit tests** and **integration tests** to ensure correctness and reliability:

- **Integration Tests**: Located under `tests/API/ThreadPilot.API.IntegrationTests/VehicleService/`, these tests spin up the full API (in-memory DB) and verify endpoint behavior, including authentication, validation, and data retrieval. Example: `VehiclesControllerTests.cs` covers valid/invalid requests, API key checks, and error scenarios.
- **Unit Tests**: (Structure in place for `tests/ThreadPilot.Application.UnitTests/`), designed to cover business logic and domain rules. (Add more as the solution evolves.)

All tests can be run with:
```bash
dotnet test
```

---

## 6. CI/CD

The solution uses **GitHub Actions** for continuous integration. On every push or manual trigger, the workflow:
- Checks out the code
- Sets up .NET 8
- Restores dependencies
- Builds the solution
- Runs all tests

Workflow file: `.github/workflows/build-on-demand.yml`

---

## 7. Error Handling, Extensibility, and Security

- **Error Handling**: Centralized via global exception middleware, FluentValidation, and the results pattern.
- **Extensibility**: Achieved through clean architecture, separation of concerns, and feature-based organization.
- **Security**: API key authentication is enforced for all endpoints. For production, I recommend moving to JWT or certificate-based authentication. Secrets should be managed via Azure Key Vault/App Config.
- **HTTP Client Best Practices**: The Vehicle Service client uses a typed HTTP client with in-memory caching to improve performance and avoid socket exhaustion ([see Microsoft docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-9.0#typed-clients)).

---

## 8. API Versioning

API versioning can be implemented using:
- **URL style**: `/v1/api/vehicles/{registrationNumber}`
- **Query string**: `/api/vehicles/{registrationNumber}?version=1.0`

In ASP.NET Core, add the `Microsoft.AspNetCore.Mvc.Versioning` NuGet package and configure in `Program.cs`:

```csharp
services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader(); // or QueryStringApiVersionReader
});
```

---

## 9. Potential Improvements

If I had more time, I would consider:
- Using the **Ice Panel C4 model** for architectural diagrams.
- Adding **Application Insights** for monitoring.
- Establishing a comprehensive **API versioning strategy**.
- Exploring **Azure Functions** for cost-effective hosting.
- Moving secrets to **Azure Key Vault** and configs to **App Configuration**.
- Switching to **JWT tokens** for authentication.
- Evaluating alternatives to **MediatR** to reduce dependencies.
- Using **TestContainers** for more robust integration testing.
- Consolidating service logic in the Application Layer to avoid redundant network calls.
- Restructuring the insurance database for more flexible pricing.
- Adopting a **true microservices pattern** with separate databases per service.
- **Dockerizing** the solution for easier deployment.
- Using **external Redis cache** for vehicle info in production (instead of in-memory cache).
- Adding **HTTP client retries** with exponential backoff for resiliency.

---

## 10. Onboarding New Developers

To get started quickly, new developers should have experience with:
- **.NET 8 / ASP.NET Core**
- **Clean Architecture** principles
- **Domain-Driven Design (DDD)**
- **CQRS** and MediatR
- **Entity Framework Core**
- **RESTful API design**

I recommend starting by reviewing the solution structure, running the APIs locally, and exploring the codebase by feature folder. Familiarity with vertical slicing and feature-based organization will be very helpful.

---

## 11. Challenges Faced

The **data modeling phase** was particularly tricky as it required balancing domain-driven design with practical implementation constraints. I opted to separate insurance tables by insurance type to allow flexibility for schema evolution without bloating a single centralized table for all insurance types (I can discuss this further during the interview).

---

## 12. About Me

At **Billo** (a competitor to Kivra), I worked extensively with domain-driven design, integration tests, and microservices. In my last assignment, I implemented the CQRS pattern and the vertical slicing (feature folders) you see here. I believe this background gives me a strong foundation in building robust, maintainable, and scalable solutions like ThreadPilot.
