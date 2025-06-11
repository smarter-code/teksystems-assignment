
# ThreadPilot README

## Architecture

### Architecture Overview

The solution consists of two RESTful API microservices:

1. **Vehicle Service API** - Endpoint for retrieving vehicle information by registration number.
2. **Insurance Service API** - Endpoint for retrieving insurance information by personal identification number.

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

### Prerequisites

To run this solution, you'll need:

- .NET 8 SDK
- Visual Studio 2022 / VS Code / Rider (optional)

---

## Design Decisions

### Best Practices Implemented

- **Domain-Driven Design** for separating core logic and business models.
- **Repository Pattern** with DbContext abstraction for persistence.
- **Dependency Injection** to decouple components.
- **Async/Await** patterns for asynchronous operations.
- **NoTracking Queries** for performance in read operations.
- Proper **Indexing** for SQL queries to optimize performance.
- **Centralized Error Handling** through global middleware.
- **Input Validation and Normalization** using FluentValidation.
- **Value Objects** to encapsulate domain concepts.
- **Vertical Feature Slicing** to organize by use-case.

---

## Running the Solution Locally
###############

### Getting Started

#### 1. Build the Solution

Run the following in the project root:

```bash
dotnet build
```

#### 2. Run Vehicle Service API

Navigate to the Vehicle Service API folder:

```bash
cd src/API/ThreadPilot.VehicleService.API
dotnet run
```

The Vehicle Service API will start on `http://localhost:<Port1>`.

#### 3. Run Insurance Service API (in a new terminal)

Update the `appsettings.json` in the Insurance Service and specify:

```json
"VehicleService": {
    "BaseUrl": "http://localhost:<Port1>",
    "ApiKey": "vehicle-service-api-key-123456"
}
```

Then navigate to the Insurance Service API folder:

```bash
cd src/API/ThreadPilot.InsuranceService.API
dotnet run --urls "http://localhost:<Port2>"
```

The Insurance Service API will start on `http://localhost:<Port2>`.

### API Documentation

Both services include Swagger UI for API exploration:

- **Vehicle Service**: `http://localhost:<Port1>/swagger`
- **Insurance Service**: `http://localhost:<Port2>/swagger`

### Authentication

API Key headers are required for both services:

- Vehicle Service: `X-API-Key: vehicle-service-api-key-123456`
- Insurance Service: `X-API-Key: insurance-service-api-key-123456`

### Endpoints

#### Vehicle Service

- **GET** `/api/vehicles/{registrationNumber}`: Retrieves vehicle details based on the registration number.

#### Insurance Service

- **GET** `/api/insurances/person/{personalIdentificationNumber}`: Retrieves all insurances for a given person.

---

## Error Handling, Extensibility, and Security

- **Error Handling**: Implemented using FluentValidation, Results Pattern, and Global Exception handlers.
- **Extensibility**: Achieved with clean architecture principles, ensuring separation of concerns and modular design.
- **Security**: APIs authenticate using API keys but can be extended to use certificate authentication or JWT tokens.

---

## API Versioning Approach

The application can adopt one of the following strategies for API versioning:

- URL styles like `/v1/endpoint`, `/v2/endpoint`.
- Query string versions like `/endpoint?version=1.0`.

---

## Potential Improvements (If Time Allowed)

Given more time, enhancements that could improve the solution include:

- Implementing **Ice Panel C4 model** for better architectural clarity.
- Adding **Application Insights** for more robust monitoring.
- Refactoring HTTP clients to use **HTTP Client Factory** to avoid socket exhaustion.
- Including database **screenshots** and **Entity-Relationship diagrams (ERDs)**.
- Completing **integration tests** for both services.
- Establishing a comprehensive **API versioning strategy**.
- Exploring the use of **Azure Functions** to save costs compared to hosting on App Service Plans.
- Leveraging **Azure Key Vault** and **App Configuration** for storing sensitive data and configurations securely.
- Switching to **JWT tokens** for secure API authentication instead of API keys.
- Evaluating alternatives to **MediatR** to reduce NuGet package dependencies.
- Mocking dependencies with **TestContainers** for robust testing environments.
- Consolidating service logic in the Application Layer to avoid redundant network calls.
- Indexing external IDs in the databases for better querying efficiency.
- Restructuring the insurance database for better pricing flexibility.
- Fully adopting a **true microservices pattern** by segregating databases.
- **Dockerizing the solution** for simplified deployment and scalability.
- Introducing **caching** for HTTP client calls.
- Mocking `DateTime.Now` for test consistency.
- Reviewing person number normalization formats for correctness.

---

## Challenges Faced

The **data modeling phase** was particularly tricky as it required balancing domain-driven design with practical implementation constraints.
