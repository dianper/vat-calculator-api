# Austrian VAT Calculator API

The Austrian VAT Calculator API is a RESTful service designed to calculate VAT (Value Added Tax) for Austrian purchases based on net, gross, or VAT amounts. The API supports multiple VAT rates commonly used in Austria (10%, 13%, and 20%) and follows best practices such as API versioning, testing, and dependency injection.

## Key Features
- **.NET 8**: Built with the latest .NET 8 framework.
- **API Versioning**: Future-proof API structure with URL-based or header-based versioning to support multiple versions.
- **Swagger Documentation**: Integrated Swagger UI for API exploration and testing directly in your browser.
- **Docker Support**: Dockerfile included for containerization and deployment in any environment.
- **Unit and Integration Tests**: Comprehensive test coverage to ensure the reliability of the application.
- **FluentValidator**: Used for validating input requests, ensuring data integrity and preventing invalid operations.
- **Central Package Manager**: Leverages .NET’s Central Package Management to streamline NuGet dependency management.
- **GitHub Actions**: Automated CI pipeline for building and testing the application.

## Installation

### Prerequisites
- .NET 8 SDK
- Docker (optional)

### Setup
1. Clone the repository.
```bash
git clone https://github.com/dianper/vat-calculator-api.git
cd vat-calculator-api
```
2. Restore NuGet packages.
```bash
dotnet restore
```
3. Build the project.
```bash
dotnet build
```

### How to Run
To run the application locally, follow these steps:

1. Run the API.
```bash
dotnet run --project src/Presentation.API/Presentation.API.csproj
```
2. Access Swagger UI: Open your browser and go to http://localhost:5000/swagger/index.html. This page provides a UI to interact with the API and view its documentation.
3. API Versioning: The API supports versioning in the URL. Example: http://localhost:5000/api/v1/vat/calculate

To run the application in a Docker container, follow these steps:
```bash
docker-compose up --build
```
1. Access Swagger UI: Open your browser and go to http://localhost:5000/swagger/index.html.

## API Endpoints

### VAT Calculation
The API provides endpoints to calculate VAT based on either the net amount, gross amount, or VAT amount.

- **POST** /api/v1/vat/calculate
- **Request Body**: `VATRequest`
```json
{
  "net": 100.0,
  "vatRate": 0.20
}
```
- **Response**: `VATResponse`
```json
{
  "net": 100.0,
  "gross": 120.0,
  "vat": 20.0,
  "isValid": true,
  "message": "VAT calculation successful."
}
```

### Error Handling
The API returns detailed error messages in case of:

- Missing or invalid input (non-numeric values, zero amounts)
- Invalid VAT rate (other than 10%, 13%, or 20%)
- More than one input type provided (e.g., net and gross)

### Testing
This project includes both unit and integration tests.

#### Running Unit Tests
```bash
dotnet test --filter UnitTests
```

#### Running Integration Tests
```bash
dotnet test --filter IntegrationTests
```

These tests ensure that the VAT calculations and API behavior function as expected.

## References
This API is inspired by the VAT calculations available on [Calkoo VAT Calculator](https://www.calkoo.com/en/vat-calculator). It has been adapted to meet Austrian VAT requirements.