# Car-Reservation

## Configuration

The Car Reservation System uses the following configuration settings:

### appsettings.json 
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Default": "Server=.;Database=Car-Reservation-Db;Integrated Security=True;TrustServerCertificate=True;"
  },
  "JWT": {
    "Issuer": "http://localhost",
    "Audience": "http://localhost:4200",
    "SecretKey": "PuQJfnYx7LSRxYv6TJwZ9kM3H8Dp4K2gCzVbNmEh8W5tA9qXrG4sBdFc/+UQn1jIoO3iNlZaRyP0wkvh5meK2w==",
    "Lifetime": 7
  }
}

```
Car-Reservation
```
├─── API                 # Presentation Layer (API Controllers, Middleware, Extensions)
│   ├─── Controllers     # Handles HTTP requests and responses
│   ├─── Extension       # Extension methods for configuring services
│   ├─── Middleware      # Custom middleware (e.g., error handling, authentication)
│   └─── Properties      # Assembly info or settings
│
├─── Domain              # Core Business Logic (Entities, Interfaces)
│   ├─── Entities        # Core entities used in the system
│   │   └─── Identity    # Identity-related entities (Users, Roles, Claims)
│   └─── ServicesInterfaces # Service interfaces for dependency inversion
│
├─── Dtos                # Data Transfer Objects (DTOs)
│   ├─── AccountDtos     # DTOs related to authentication and user accounts
│   ├─── ErrorDtos       # DTOs for handling errors
│   └─── PaginationDto   # DTOs for pagination responses
│
├─── Repository          # Data Access Layer (EF Core, Repositories, Unit of Work)
│   ├─── Contexts        # Database context (EF Core)
│   │   └─── CarRentContext 
│   │       ├─── Configurations  # Entity configurations for EF Core
│   │       ├─── Data            # Seed data or static data
│   │       └─── Migrations      # EF Core database migrations
│   ├─── Repositories    # Concrete repository implementations
│   ├─── RepositoryInterfaces # Interfaces for repositories (Repository Pattern)
│   ├─── Specifications  # Specification pattern for querying
│   └─── UnitsOfWork     # Unit of Work pattern for managing transactions
│
└─── Services            # Business Logic Layer (Application Services)
    ├─── AuthServices    # Authentication and Authorization services
```
