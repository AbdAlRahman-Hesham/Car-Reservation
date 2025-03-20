# Car-Reservation Project

Welcome to the **Car-Reservation** project! This project is built using **ASP.NET API** for the backend and **React** for the frontend.

---
## 🔗 Links
- **Postman Collection**: [Car-Reservation API](https://www.postman.com/abdooawad/workspace/car-reservation/collection/38103183-0b3b7b7c-be3a-4346-9e40-5948fd9a7a46?action=share&creator=38103183)
- **Application URL**: [Car-Reservation API](http://car-reservation.runasp.net/swagger)
- **Project ERD**: [Car-Reservation ERD](https://drive.google.com/file/d/1m621vShpqT-ZnlM_tDr8QHoKSSz3eIji/view)

---
## ⚙️ Configuration
The project requires specific configurations set in the `appsettings.json` file.

### Example `appsettings.json`
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
    "SecretKey": "The Secret Key",
    "Lifetime": 7
  },
  "EmailSettings": {
    "Server": "smtp.gmail.com",
    "Port": "587",
    "UserName": "The User Name",
    "Password": "The Password",
    "Email": "The Email"
  },
  "Stripe": {
    "Secretkey": "sk_************************************************************",
    "PublishableKey": "pk_********************************************************"
  },
  "Cloudinary": {
    "CloudName": "The CloudName",
    "APISecret": "*************************************",
    "APIKey": "**********************************"
  }
}
```
### 🔑 Key Configuration Details
- **Logging**: Defines log levels for debugging and monitoring.
- **Database Connection**: Uses SQL Server (`Car-Reservation-Db`).
- **JWT Authentication**: Handles authentication settings.
- **Email Settings**: Configures SMTP settings for sending emails.

---
## 📁 Project Structure
The backend project follows a structured, modular design.

```
📂 Car-Reservation
│── 📂 API                 # Presentation Layer (Controllers, Middleware, Extensions)
│   │── 📂 Controllers     # Handles HTTP requests
│   │── 📂 Extension       # Service configuration helpers
│   │── 📂 Middleware      # Custom middleware (error handling, authentication)
│   └── 📂 Properties      # Project properties
│
│── 📂 Domain              # Core Business Logic (Entities, Interfaces)
│   │── 📂 Entities        # Core domain entities
│   │   └── 📂 Identity    # Identity management (Users, Roles, Claims)
│   └── 📂 ServicesInterfaces # Abstraction for service logic
│
│── 📂 Dtos                # Data Transfer Objects (DTOs)
│   │── 📂 AccountDtos     # User authentication DTOs
│   │── 📂 ErrorDtos       # Standardized error response DTOs
│   └── 📂 PaginationDto   # DTOs for paginated API responses
│
│── 📂 Repository          # Data Access Layer (EF Core, Repositories, Unit of Work)
│   │── 📂 Contexts        # Database context
│   │   │── 📂 Configurations  # EF Core entity configurations
│   │   │── 📂 Data            # Database seed data
│   │   └── 📂 Migrations      # EF Core migrations
│   │── 📂 Repositories    # Repository implementations
│   │── 📂 RepositoryInterfaces # Interfaces for repositories
│   └── 📂 UnitsOfWork     # Manages transactions
│
└── 📂 Services            # Business Logic Layer
    │── 📂 AuthServices    # Authentication services
```

---
## 🚀 Backend Setup Instructions
Follow these steps to set up and run the **Car-Reservation** project locally.

### ✅ Prerequisites
Ensure you have the following installed:
- **Visual Studio** (with ASP.NET and Entity Framework Core workloads)
- **Git** (for version control)
- **SQL Server** (or another configured database)

### 📥 Step 1: Clone the Backend Repository
```bash
git clone https://github.com/AbdAlRahman-Hesham/Car-Reservation.git
cd Car-Reservation
```

### 💻 Step 2: Open the Project in Visual Studio
1. Open **Visual Studio**.
2. Click **Open a project or solution**.
3. Navigate to the `Car-Reservation` folder and open the `.sln` file.

### 📦 Step 3: Restore Dependencies
```bash
dotnet restore
```

### 📊 Step 4: Apply Database Migrations
```bash
dotnet ef database update
```

### ▶️ Step 5: Run the API
1. Set **Car-Reservation.API** as the startup project.
2. Press **F5** to start debugging.
3. The API should now be available at `https://localhost:7014/` (or another assigned port).

---
## 🌐 Frontend Setup Instructions
Follow these steps to set up the React frontend project.

### 📥 Step 1: Clone the Frontend Repository
```bash
git clone https://github.com/AbdAlRahman-Hesham/Car-Reservation.git
cd Car-Reservation-Frontend
```

### 📦 Step 2: Install Dependencies
Ensure you have **Node.js** and **npm** installed, then run:
```bash
npm install
```

### 🔧 Step 3: Configure Environment Variables
Create a `.env` file in the project root and add:
```
REACT_APP_API_URL=https://localhost:7014/
```

### ▶️ Step 4: Run the React App
```bash
npm start
```
The app should now be available at `http://localhost:3000/`.

---
## 🤝 Contribution Guidelines
Want to contribute? Follow these steps:

1. **Maintain Code Structure**: Follow the existing folder and naming conventions.
2. **Use Meaningful Commits**: Write clear commit messages.
3. **Document Your Code**: Add comments and update documentation when needed.
4. **Submit Pull Requests**: Create a PR for review before merging changes.

## 🤝 Contributors
<a href="https://github.com/remarkablemark">
  <img src="https://github.com/remarkablemark.png?size=50">
</a>


   
