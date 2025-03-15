# Car-Reservation

## Configuration

The Car Reservation System uses the following configuration settings:

### appsettings.json 
The application connects to a SQL Server database with the following connection string:
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

