{\rtf1\ansi\ansicpg1252\cocoartf2820
\cocoatextscaling0\cocoaplatform0{\fonttbl\f0\fnil\fcharset0 Menlo-Regular;\f1\fnil\fcharset0 Menlo-Bold;}
{\colortbl;\red255\green255\blue255;\red0\green0\blue0;\red0\green0\blue255;\red255\green255\blue254;
\red144\green1\blue18;\red0\green0\blue117;}
{\*\expandedcolortbl;;\cssrgb\c0\c0\c0;\cssrgb\c0\c0\c100000;\cssrgb\c100000\c100000\c99608;
\cssrgb\c63922\c8235\c8235;\cssrgb\c0\c6667\c53333;}
\paperw11900\paperh16840\margl1440\margr1440\vieww11520\viewh8400\viewkind0
\deftab720
\pard\pardeftab720\partightenfactor0

\f0\fs28 \cf0 \expnd0\expndtw0\kerning0
\outl0\strokewidth0 \strokec2 \
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 # ThreadPilot README\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ## Architecture\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ### Architecture Overview\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 The solution consists of two RESTful API microservices:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 1. 
\f1\b \cf0 \strokec2 **Vehicle Service API**
\f0\b0  - Endpoint for retrieving vehicle information by registration number.\cb1 \
\cf3 \cb4 \strokec3 2. 
\f1\b \cf0 \strokec2 **Insurance Service API**
\f0\b0  - Endpoint for retrieving insurance information by personal identification number.\cb1 \
\
\cf3 \cb4 \strokec3 ### Solution Structure\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```plaintext\cf0 \cb1 \strokec2 \
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 ThreadPilot/\cb1 \
\cb4 \uc0\u9500 \u9472 \u9472  src/\cb1 \
\cb4 \uc0\u9474    \u9500 \u9472 \u9472  Core/\cb1 \
\cb4 \uc0\u9474    \u9474    \u9500 \u9472 \u9472  ThreadPilot.Domain/          # Domain entities and value objects\cb1 \
\cb4 \uc0\u9474    \u9474    \u9492 \u9472 \u9472  ThreadPilot.Application/     # Use cases and business logic\cb1 \
\cb4 \uc0\u9474    \u9500 \u9472 \u9472  Infrastructure/\cb1 \
\cb4 \uc0\u9474    \u9474    \u9500 \u9472 \u9472  ThreadPilot.Infrastructure/  # Cross-cutting concerns\cb1 \
\cb4 \uc0\u9474    \u9474    \u9492 \u9472 \u9472  ThreadPilot.Persistence/     # Data access with EF Core\cb1 \
\cb4 \uc0\u9474    \u9492 \u9472 \u9472  API/\cb1 \
\cb4 \uc0\u9474        \u9500 \u9472 \u9472  ThreadPilot.VehicleService.API/\cb1 \
\cb4 \uc0\u9474        \u9492 \u9472 \u9472  ThreadPilot.InsuranceService.API/\cb1 \
\cb4 \uc0\u9492 \u9472 \u9472  tests/\cb1 \
\cb4     \uc0\u9500 \u9472 \u9472  ThreadPilot.Application.UnitTests/\cb1 \
\cb4     \uc0\u9500 \u9472 \u9472  ThreadPilot.VehicleService.IntegrationTests/\cb1 \
\cb4     \uc0\u9492 \u9472 \u9472  ThreadPilot.InsuranceService.IntegrationTests/\cb1 \
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 ### Prerequisites\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 To run this solution, you'll need:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 - \cf0 \strokec2 .NET 8 SDK\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Visual Studio 2022 / VS Code / Rider (optional)\cb1 \
\
\cf3 \cb4 \strokec3 ---\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ## Design Decisions\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ### Best Practices Implemented\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Domain-Driven Design**
\f0\b0  for separating core logic and business models.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Repository Pattern**
\f0\b0  with DbContext abstraction for persistence.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Dependency Injection**
\f0\b0  to decouple components.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Async/Await**
\f0\b0  patterns for asynchronous operations.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **NoTracking Queries**
\f0\b0  for performance in read operations.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Proper 
\f1\b **Indexing**
\f0\b0  for SQL queries to optimize performance.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Centralized Error Handling**
\f0\b0  through global middleware.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Input Validation and Normalization**
\f0\b0  using FluentValidation.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Value Objects**
\f0\b0  to encapsulate domain concepts.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Vertical Feature Slicing**
\f0\b0  to organize by use-case.\cb1 \
\
\cf3 \cb4 \strokec3 ---\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ## Running the Solution Locally\cf0 \cb1 \strokec2 \
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 ###############\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 ### Getting Started\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 #### 1. Build the Solution\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 Run the following in the project root:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```bash\cf0 \cb1 \strokec2 \
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 dotnet build\cb1 \
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 #### 2. Run Vehicle Service API\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 Navigate to the Vehicle Service API folder:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```bash\cf0 \cb1 \strokec2 \
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 cd src/API/ThreadPilot.VehicleService.API\cb1 \
\cb4 dotnet run\cb1 \
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 The Vehicle Service API will start on \cf6 \strokec6 `http://localhost:<Port1>`\cf0 \strokec2 .\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 #### 3. Run Insurance Service API (in a new terminal)\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 Update the \cf6 \strokec6 `appsettings.json`\cf0 \strokec2  in the Insurance Service and specify:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```json\cf0 \cb1 \strokec2 \
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 "VehicleService": \{\cb1 \
\cb4     "BaseUrl": "http://localhost:<Port1>",\cb1 \
\cb4     "ApiKey": "vehicle-service-api-key-123456"\cb1 \
\cb4 \}\cb1 \
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 Then navigate to the Insurance Service API folder:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```bash\cf0 \cb1 \strokec2 \
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 cd src/API/ThreadPilot.InsuranceService.API\cb1 \
\cb4 dotnet run --urls "http://localhost:<Port2>"\cb1 \
\pard\pardeftab720\partightenfactor0
\cf5 \cb4 \strokec5 ```\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 The Insurance Service API will start on \cf6 \strokec6 `http://localhost:<Port2>`\cf0 \strokec2 .\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 ### API Documentation\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 Both services include Swagger UI for API exploration:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Vehicle Service**
\f0\b0 : \cf6 \strokec6 `http://localhost:<Port1>/swagger`\cf0 \cb1 \strokec2 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Insurance Service**
\f0\b0 : \cf6 \strokec6 `http://localhost:<Port2>/swagger`\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ### Authentication\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 API Key headers are required for both services:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Vehicle Service: \cf6 \strokec6 `X-API-Key: vehicle-service-api-key-123456`\cf0 \cb1 \strokec2 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Insurance Service: \cf6 \strokec6 `X-API-Key: insurance-service-api-key-123456`\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ### Endpoints\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 #### Vehicle Service\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **GET**
\f0\b0  \cf6 \strokec6 `/api/vehicles/\{registrationNumber\}`\cf0 \strokec2 : Retrieves vehicle details based on the registration number.\cb1 \
\
\cf3 \cb4 \strokec3 #### Insurance Service\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **GET**
\f0\b0  \cf6 \strokec6 `/api/insurances/person/\{personalIdentificationNumber\}`\cf0 \strokec2 : Retrieves all insurances for a given person.\cb1 \
\
\cf3 \cb4 \strokec3 ---\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ## Error Handling, Extensibility, and Security\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Error Handling**
\f0\b0 : Implemented using FluentValidation, Results Pattern, and Global Exception handlers.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Extensibility**
\f0\b0 : Achieved with clean architecture principles, ensuring separation of concerns and modular design.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Security**
\f0\b0 : APIs authenticate using API keys but can be extended to use certificate authentication or JWT tokens.\cb1 \
\
\cf3 \cb4 \strokec3 ---\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ## API Versioning Approach\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 The application can adopt one of the following strategies for API versioning:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 - \cf0 \strokec2 URL styles like \cf6 \strokec6 `/v1/endpoint`\cf0 \strokec2 , \cf6 \strokec6 `/v2/endpoint`\cf0 \strokec2 .\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Query string versions like \cf6 \strokec6 `/endpoint?version=1.0`\cf0 \strokec2 .\cb1 \
\
\cf3 \cb4 \strokec3 ---\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ## Potential Improvements (If Time Allowed)\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 Given more time, enhancements that could improve the solution include:\cb1 \
\
\pard\pardeftab720\partightenfactor0
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Implementing 
\f1\b **Ice Panel C4 model**
\f0\b0  for better architectural clarity.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Adding 
\f1\b **Application Insights**
\f0\b0  for more robust monitoring.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Refactoring HTTP clients to use 
\f1\b **HTTP Client Factory**
\f0\b0  to avoid socket exhaustion.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Including database 
\f1\b **screenshots**
\f0\b0  and 
\f1\b **Entity-Relationship diagrams (ERDs)**
\f0\b0 .\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Completing 
\f1\b **integration tests**
\f0\b0  for both services.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Establishing a comprehensive 
\f1\b **API versioning strategy**
\f0\b0 .\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Exploring the use of 
\f1\b **Azure Functions**
\f0\b0  to save costs compared to hosting on App Service Plans.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Leveraging 
\f1\b **Azure Key Vault**
\f0\b0  and 
\f1\b **App Configuration**
\f0\b0  for storing sensitive data and configurations securely.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Switching to 
\f1\b **JWT tokens**
\f0\b0  for secure API authentication instead of API keys.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Evaluating alternatives to 
\f1\b **MediatR**
\f0\b0  to reduce NuGet package dependencies.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Mocking dependencies with 
\f1\b **TestContainers**
\f0\b0  for robust testing environments.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Consolidating service logic in the Application Layer to avoid redundant network calls.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Indexing external IDs in the databases for better querying efficiency.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Restructuring the insurance database for better pricing flexibility.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Fully adopting a 
\f1\b **true microservices pattern**
\f0\b0  by segregating databases.\cb1 \
\cf3 \cb4 \strokec3 - 
\f1\b \cf0 \strokec2 **Dockerizing the solution**
\f0\b0  for simplified deployment and scalability.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Introducing 
\f1\b **caching**
\f0\b0  for HTTP client calls.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Mocking \cf6 \strokec6 `DateTime.Now`\cf0 \strokec2  for test consistency.\cb1 \
\cf3 \cb4 \strokec3 - \cf0 \strokec2 Reviewing person number normalization formats for correctness.\cb1 \
\
\cf3 \cb4 \strokec3 ---\cf0 \cb1 \strokec2 \
\
\cf3 \cb4 \strokec3 ## Challenges Faced\cf0 \cb1 \strokec2 \
\
\pard\pardeftab720\partightenfactor0
\cf0 \cb4 The 
\f1\b **data modeling phase**
\f0\b0  was particularly tricky as it required balancing domain-driven design with practical implementation constraints.\cb1 \
\
}