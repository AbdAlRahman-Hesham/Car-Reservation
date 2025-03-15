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
### Backend Project Structure 
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
(Due to technical issues, the search service is temporarily unavailable.)

# Car Reservation Project - Setup and Contribution Guide

This guide provides step-by-step instructions for setting up the **Car Reservation** project on your local machine and outlines the contribution guidelines for developers.

---

## Setup Instructions To Setup Backend Project

### Step 1: Install Required Tools
Make sure you have the following installed on your machine:
- **Visual Studio** (with ASP.NET and Entity Framework workloads)
- **Git**
- **SQL Server** (or use SQLite if configured)

---

### Step 2: Clone the Project from GitHub
1. Open **Git Bash** or **Command Prompt**.
2. Run the following command to clone the repository:
   ```bash
   git clone https://github.com/your-repo/car-reservation.git
   ```
3. Navigate to the project folder:
   ```bash
   cd car-reservation
   ```

---

### Step 3: Open the Project in Visual Studio
1. Open **Visual Studio**.
2. Click **Open a project or solution**.
3. Navigate to the cloned `car-reservation` folder and select the `.sln` file.

---

### Step 4: Restore NuGet Packages
1. In Visual Studio, open the **Package Manager Console (PMC)**:
   - Go to **Tools → NuGet Package Manager → Package Manager Console**.
2. Run the following command to install all dependencies:
   ```bash
   dotnet restore
   ```

---

### Step 5: Apply Database Migrations
1. Open the **Package Manager Console** again.
2. Run the following command to update the database:
   ```bash
   dotnet ef database update
   ```

---

### Step 6: Run the Project
1. Set **Car-Reservation.API** as the startup project.
2. Click **Start (F5)** in Visual Studio.
3. The API should now be running at `https://localhost:7014/` (or another assigned port).

---



