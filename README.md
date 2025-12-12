# HARD.CORE - Enterprise Resource Planning Solution

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Architecture](#project-architecture)
- [Folder Structure](#folder-structure)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Build Instructions](#build-instructions)
- [Running the Application](#running-the-application)
- [Configuration](#configuration)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

---

## 🎯 Project Overview

**HARD.CORE** is a comprehensive enterprise resource planning (ERP) system built with modern .NET technologies. It features a multi-layered architecture with a RESTful API backend, business logic services, data access layer, and multiple frontend interfaces including ASP.NET Web Forms and a web-based dashboard.

The project is designed to manage:
- User authentication and authorization
- Personnel and profiles management
- Notifications and messaging systems
- Document and file management
- Security actions and audit trails
- Job vacancies and recruitment workflows
- Reporting and analytics

---

## ✨ Features

### Core Functionality
- **Authentication & Authorization**: JWT-based token authentication with role-based access control
- **User Management**: Complete user lifecycle management with profiles and roles
- **Profile Management**: Hierarchical profile structures with inheritance capabilities
- **Document Management**: File upload, storage, and retrieval system
- **Notification System**: Multi-channel notifications (email, system alerts)
- **Security Audit Trail**: Comprehensive logging of all security-related actions
- **Reporting Engine**: Telerik Reporting integration for advanced reporting

### API Features
- **RESTful API**: Built on ASP.NET Core 8.0 with OpenAPI/Swagger documentation
- **API Versioning**: Support for multiple API versions (v1, v2)
- **CORS Support**: Cross-Origin Resource Sharing enabled for frontend integration
- **JWT Authentication**: Secure token-based authentication

### Data Management
- **SQL Server Integration**: Enterprise-grade database with Entity Framework support
- **Directory Services**: Active Directory integration capabilities
- **Data Encryption**: Cryptographic utilities for sensitive data protection

### Enterprise Features
- **Job Vacancies**: Complete recruitment workflow management
- **Authorization Flows**: Multi-step authorization processes
- **Company Management**: Multi-company support
- **Delivery Tracking**: Order/delivery management system
- **Suggestion System**: Employee feedback and suggestions system

---

## 🛠️ Technology Stack

### Backend
- **Runtime**: .NET 8.0 / .NET Framework 4.8
- **Web Framework**: ASP.NET Core 8.0
- **API**: RESTful API with OpenAPI/Swagger
- **Authentication**: JWT Bearer tokens
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core

### Libraries & Tools
- **API Versioning**: Asp.Versioning.Mvc 8.1.0
- **Mapping**: AutoMapper 15.0.1
- **JSON**: Newtonsoft.Json (NuGet)
- **Reporting**: Telerik Reporting 19.0.25.313
- **Security**: JWT Bearer, Active Directory integration

### Frontend
- **Legacy**: ASP.NET Web Forms (.NET Framework 4.8)
- **Presentation**: Razor Pages integration

### Development Tools
- **IDE**: Visual Studio 2022 (v17.10+)
- **Version Control**: Git
- **Build System**: MSBuild / dotnet CLI

---

## 🏗️ Project Architecture

### Layered Architecture

The solution follows a **4-tier layered architecture**:

```
┌──────────────────────────────────────────────┐
│  Presentation Layer                          │
│  (HARD.CORE.API, HARD.CORE.WEB)             │
└──────────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────┐
│  Service Layer                               │
│  (HARD.CORE.SER - Business Logic Services)   │
└──────────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────┐
│  Business Logic Layer                        │
│  (HARD.CORE.NEG - Business Rules)            │
└──────────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────┐
│  Data Access Layer                           │
│  (HARD.CORE.DAT - Database Access)           │
└──────────────────────────────────────────────┘
                       ↓
┌──────────────────────────────────────────────┐
│  Data Models Layer                           │
│  (HARD.CORE.OBJ - Entity Objects)            │
└──────────────────────────────────────────────┘
```

### Module Descriptions

#### **HARD.CORE.API** (Presentation - REST API)
- **Framework**: ASP.NET Core 8.0
- **Purpose**: REST API endpoints for client applications
- **Features**:
  - API versioning (V1, V2)
  - JWT authentication
  - Swagger/OpenAPI documentation
  - CORS configuration
  - Telerik Reporting integration
- **Key Controllers**:
  - `UsuarioController` - User management
  - `AuthController` - Authentication
  - `PerfilController` - Profile management
  - `AvisoController` - Notifications/Alerts
  - `EntregaController` - Delivery management
  - `CorreoController` - Email management
  - `ArchivoController` - File management
  - `SeguridadAccionController` - Security audit logs
  - `MotivoVacanteController` - Job vacancy reasons
  - `CryptographerController` - Encryption utilities
  - `ConfigController` - Configuration management
  - `ReportsController` - Reporting endpoints

#### **HARD.CORE.SER** (Service Layer)
- **Framework**: .NET Framework 4.8
- **Purpose**: Business logic services implementing core functionality
- **Key Services** (as per `*SER.cs` files):
  - `AuthSER` - Authentication operations
  - `UsuarioSER` - User operations
  - `PerfilSER` - Profile operations
  - `EmpresaSER` - Company management
  - `EntregaSER` - Delivery services
  - `CorreosSER` - Email services
  - `NotificacionSER` - Notification handling
  - `CommunesSER` - Common utilities
  - `DireccionSER` - Address/Location services
  - `EncuestaSER` - Survey services
  - `FlujoAutorizacionSER` - Authorization workflows
  - `PagosSER` - Payment services
  - `ProductoSER` - Product management
  - `SeguridadSER` - Security operations
  - `SugerenciaSER` - Suggestion handling

#### **HARD.CORE.NEG** (Business Logic Layer)
- **Framework**: .NET 8.0
- **Purpose**: Business rules and validation logic
- **Key Classes** (as per `*B.cs` files):
  - `UsuarioB` - User business logic
  - `AuthB` - Authentication rules
  - `PerfilB` - Profile rules
  - `EmpresaB` - Company rules
  - `EntregaB` - Delivery rules
  - `CorreoB` - Email rules
  - `ClienteB` - Client management
  - `NotificacionB` - Notification rules
  - `FlujoAutorizacionB` - Authorization workflow logic
  - `PagosB` - Payment rules
  - `ProductoB` - Product rules
  - `SugerenciaB` - Suggestion rules
  - `ArchivoB` - File operations
  - `MenuB` - Menu management
  - `BitacoraB` - Audit trail logging
  - `NivelInglesB` - Language proficiency rules
  - `NivelMinimoEstudiosB` - Education level rules
  - `MotivoVacanteB` - Vacancy reason rules

#### **HARD.CORE.DAT** (Data Access Layer)
- **Framework**: .NET 8.0
- **Purpose**: Database operations and Entity Framework Core integration
- **Features**:
  - SQL Server connection management
  - Entity Framework Core queries
  - Data persistence operations
  - Direct SQL operations support

#### **HARD.CORE.OBJ** (Data Models)
- **Framework**: .NET Standard 2.0
- **Purpose**: Entity/Model classes shared across layers
- **Contents**: Data transfer objects (DTOs) and entity definitions

#### **HARD.CORE.WEB** (Legacy Web Interface)
- **Framework**: ASP.NET Web Forms (.NET Framework 4.8)
- **Purpose**: Legacy web interface for end-users
- **Features**: Administrative interface, user dashboards

---

## 📁 Folder Structure

```
hard.core/
├── README.md                          # This file
├── LICENSE                            # GPL v3 License
├── HARD.CORE/                         # Main solution directory
│   ├── HARD.CORE.sln                 # Visual Studio Solution file
│   ├── limpiarBinarios.bat           # Script to clean binaries
│   │
│   ├── HARD.CORE.API/                # REST API Project (.NET 8.0)
│   │   ├── appsettings.json          # Configuration file
│   │   ├── appsettings.Development.json
│   │   ├── Program.cs                # Main entry point
│   │   ├── HARD.CORE.API.csproj     # Project file
│   │   ├── HARD.CORE.API.http       # HTTP request file for testing
│   │   ├── NuGet.Config             # NuGet configuration
│   │   │
│   │   ├── Config/                   # Configuration classes
│   │   │   └── Config.cs
│   │   │
│   │   ├── Helpers/                  # Helper utilities
│   │   │   ├── ConfigurationHelper.cs
│   │   │   ├── ConfigureSwaggerOptions.cs
│   │   │   ├── DependencyInjectionHelper.cs
│   │   │   ├── JwtAuthenticateHelper.cs
│   │   │   └── ReportSourceHelper.cs
│   │   │
│   │   ├── Controllers/              # API Controllers
│   │   │   ├── Base/
│   │   │   │   └── BaseController.cs
│   │   │   ├── V1/                   # Version 1 endpoints
│   │   │   │   ├── ArchivoController.cs
│   │   │   │   ├── AvisoController.cs
│   │   │   │   ├── CorreoController.cs
│   │   │   │   ├── CryptographerController.cs
│   │   │   │   ├── ConfigController.cs
│   │   │   │   ├── EntregaController.cs
│   │   │   │   ├── MotivoVacanteController.cs
│   │   │   │   ├── PerfilController.cs
│   │   │   │   ├── SeguridadAccionController.cs
│   │   │   │   └── UsuarioController.cs
│   │   │   ├── V2/                   # Version 2 endpoints
│   │   │   │   ├── ArchivoController.cs
│   │   │   │   ├── AuthController.cs
│   │   │   │   └── UsuarioController.cs
│   │   │   └── RPT/                  # Reporting controllers (disabled)
│   │   │       ├── ReportsController.cs
│   │   │       ├── ViewerController.cs
│   │   │       └── ...
│   │   │
│   │   ├── Models/                   # Request/Response DTOs
│   │   ├── Properties/               # Project properties
│   │   ├── bin/                      # Compiled binaries
│   │   └── obj/                      # Build artifacts
│   │
│   ├── HARD.CORE.NEG/                # Business Logic Layer (.NET 8.0)
│   │   ├── HARD.CORE.NEG.csproj     # Project file
│   │   ├── Interfaces/               # Business logic interfaces
│   │   │
│   │   ├── *B.cs files:              # Business classes
│   │   │   ├── ArchivoB.cs
│   │   │   ├── AuthB.cs
│   │   │   ├── AvisoB.cs
│   │   │   ├── BitacoraB.cs
│   │   │   ├── BitacoraEventosB.cs
│   │   │   ├── ClienteB.cs
│   │   │   ├── CorreoB.cs
│   │   │   ├── CorreoVariableB.cs
│   │   │   ├── Cryptographer.cs
│   │   │   ├── EmpresaB.cs
│   │   │   ├── EntregaB.cs
│   │   │   ├── FlujoAutorizacionB.cs
│   │   │   ├── HerenciaPerfilB.cs
│   │   │   ├── MenuB.cs
│   │   │   ├── MotivoVacanteB.cs
│   │   │   ├── NivelInglesB.cs
│   │   │   ├── NivelMinimoEstudiosB.cs
│   │   │   ├── NotificacionB.cs
│   │   │   ├── PagosB.cs
│   │   │   ├── PerfilB.cs
│   │   │   ├── PrecioB.cs
│   │   │   ├── SeguridadAccionB.cs
│   │   │   ├── SugerenciaB.cs
│   │   │   ├── TipoCorreoB.cs
│   │   │   └── UsuarioB.cs
│   │   │
│   │   ├── bin/                      # Compiled binaries
│   │   └── obj/                      # Build artifacts
│   │
│   ├── HARD.CORE.SER/                # Service Layer (.NET Framework 4.8)
│   │   ├── HARD.CORE.SER.csproj     # Project file
│   │   ├── app.config               # Application configuration
│   │   ├── packages.config          # NuGet packages (legacy)
│   │   │
│   │   ├── Helpers/                  # Helper utilities
│   │   ├── Properties/               # Project properties
│   │   │
│   │   ├── *SER.cs files:            # Service classes
│   │   │   ├── AuthSER.cs
│   │   │   ├── AvisoSER.cs
│   │   │   ├── ClienteSER.cs
│   │   │   ├── ComunesSER.cs
│   │   │   ├── CorreosSER.cs
│   │   │   ├── CryptographerSER.cs
│   │   │   ├── DireccionSER.cs
│   │   │   ├── EmpresaSER.cs
│   │   │   ├── EncuestaSER.cs
│   │   │   ├── EntregaSER.cs
│   │   │   ├── FlujoAutorizacionSER.cs
│   │   │   ├── HerenciaPerfilSER.cs
│   │   │   ├── MenuSER.cs
│   │   │   ├── MotivoVacanteSER.cs
│   │   │   ├── NivelInglesSER.cs
│   │   │   ├── NivelMinimoEstudiosSER.cs
│   │   │   ├── NotificacionSER.cs
│   │   │   ├── PagosSER.cs
│   │   │   ├── PerfilSER.cs
│   │   │   ├── ProductoSER.cs
│   │   │   ├── SeguridadAccionSER.cs
│   │   │   ├── SeguridadSER.cs
│   │   │   ├── SugerenciaSER.cs
│   │   │   └── UsuarioSER.cs
│   │   │
│   │   ├── bin/                      # Compiled binaries
│   │   └── obj/                      # Build artifacts
│   │
│   ├── HARD.CORE.DAT/                # Data Access Layer (.NET 8.0)
│   │   ├── HARD.CORE.DAT.csproj     # Project file
│   │   ├── bin/                      # Compiled binaries
│   │   └── obj/                      # Build artifacts
│   │
│   ├── HARD.CORE.OBJ/                # Data Models (.NET Standard 2.0)
│   │   ├── HARD.CORE.OBJ.csproj     # Project file
│   │   ├── bin/                      # Compiled binaries
│   │   └── obj/                      # Build artifacts
│   │
│   ├── HARD.CORE.WEB/                # Legacy Web Interface (ASP.NET)
│   │   ├── Default.aspx              # Home page
│   │   ├── Default.aspx.cs           # Code-behind
│   │   ├── frm_*.aspx                # Administrative forms
│   │   ├── frm_*.aspx.cs             # Form code-behind files
│   │   ├── DescargableExcel.aspx    # Excel export
│   │   └── ...
│   │
│   └── packages/                     # NuGet packages cache
│
└── Shared Binaries/                  # Shared binary resources
```

---

## 📋 Prerequisites

### System Requirements
- **OS**: Windows 10/11, Windows Server 2019+ (for development)
- **RAM**: Minimum 8 GB (16 GB recommended)
- **Disk Space**: 5 GB for development environment

### Software Requirements
- **.NET SDK**: .NET 8.0 or later
- **.NET Framework**: .NET Framework 4.8
- **Visual Studio**: Visual Studio 2022 (Community, Professional, or Enterprise)
  - Workload: ASP.NET and web development
  - Workload: .NET desktop development
- **SQL Server**: SQL Server 2019 or later
- **Git**: Latest version

### Development Tools (Optional)
- **SQL Server Management Studio (SSMS)**: For database management
- **Postman** or **Insomnia**: For API testing
- **Entity Framework Tools**: `dotnet ef` (NuGet global tool)

---

## 📦 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/mrodriguex/hard.core.git
cd hard.core
```

### 2. Restore NuGet Packages

Navigate to the solution directory and restore dependencies:

```bash
cd HARD.CORE
dotnet restore HARD.CORE.sln
```

Or using Visual Studio:
- Open `HARD.CORE.sln` in Visual Studio
- Right-click the solution in Solution Explorer
- Select "Restore NuGet Packages"

### 3. Database Setup

#### Option A: Using SQL Server Management Studio
1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Create a new database named `hardcore_db`
4. Run any provided migration scripts from the `HARD.CORE.DAT` project

#### Option B: Using Entity Framework Core Migrations
```bash
cd HARD.CORE\HARD.CORE.API
dotnet ef database update
```

### 4. Configure Database Connection

Edit `HARD.CORE/HARD.CORE.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "SqlConn_HARDCORE": "Data Source=YOUR_SERVER;Initial Catalog=hardcore_db;Persist Security Info=True;User ID=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=true"
  },
  "Jwt:Key": "ixieBwXEMCNXMFQvZbN4vOC2pQJn0FPK",
  "Jwt:Duration": "120"
}
```

**Important**: Change the connection string and JWT key in production environments.

### 5. Configure Application Settings

Review and update configuration in `appsettings.json`:
- Database connections
- JWT settings
- CORS allowed origins
- Email settings
- API base URLs

---

## 🔨 Build Instructions

### Using .NET CLI

```bash
# Navigate to solution directory
cd HARD.CORE

# Build the entire solution
dotnet build HARD.CORE.sln

# Build specific project
dotnet build HARD.CORE/HARD.CORE.API/HARD.CORE.API.csproj

# Build in Release mode
dotnet build -c Release HARD.CORE.sln
```

### Using Visual Studio

1. Open `HARD.CORE/HARD.CORE.sln` in Visual Studio 2022
2. Select desired configuration: **Debug** or **Release**
3. In Solution Explorer, right-click the solution
4. Select **Build Solution** (Ctrl+Shift+B)

### Using VS Code Task

The workspace includes a pre-configured build task:

```bash
# Run the build task
# Or press Ctrl+Shift+B in VS Code
```

### Clean Build

```bash
# Clean all build artifacts
dotnet clean HARD.CORE.sln

# Or use the provided batch script (Windows only)
cd HARD.CORE
limpiarBinarios.bat
```

---

## 🚀 Running the Application

### Start the API Server

```bash
cd HARD.CORE/HARD.CORE.API
dotnet run
```

The API will start on: `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP)

### Using Visual Studio

1. Open the solution in Visual Studio
2. Set `HARD.CORE.API` as startup project
3. Press **F5** or click **Start Debugging**

### Using Integrated Development Server

The application uses Kestrel (the default ASP.NET Core web server):
- HTTPS: `https://localhost:7209`
- HTTP: `http://localhost:5000`

### Access Swagger API Documentation

Once the API is running, access Swagger documentation at:
- `https://localhost:7209/swagger` (HTTPS)
- `http://localhost:5000/swagger` (HTTP)

---

## ⚙️ Configuration

### Application Settings (`appsettings.json`)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "SqlConn_HARDCORE": "Data Source=SqlServerIP;Initial Catalog=hardcore_db;User ID=user;Password=pass;TrustServerCertificate=true"
  },
  "AllowedHosts": "*",
  "Jwt:Key": "ixieBwXEMCNXMFQvZbN4vOC2pQJn0FPK",
  "Jwt:Duration": "120",
  "DefaultPassword": "Default.123@"
}
```

### JWT Configuration

- **Jwt:Key**: Secret key for signing JWT tokens (change in production)
- **Jwt:Duration**: Token expiration time in minutes

### CORS Configuration

The API is configured to accept requests from any origin by default:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});
```

For production, configure specific allowed origins in `Program.cs`.

### Environment-Specific Configuration

- **Development**: `appsettings.Development.json` (local debugging)
- **Staging**: `appsettings.Staging.json` (pre-production)
- **Production**: `appsettings.Production.json` (live environment)

---

## 📚 API Documentation

### API Versioning

The API supports multiple versions:

#### Version 1 (V1) - Legacy
- Base path: `/api/v1/`
- Controllers: UsuarioController, AuthController, PerfilController, etc.
- Status: Maintained for backward compatibility

#### Version 2 (V2) - Current
- Base path: `/api/v2/`
- Controllers: UsuarioController, AuthController, ArchivoController, etc.
- Status: Actively developed

### Authentication

All protected endpoints require a JWT bearer token:

```
Authorization: Bearer {token}
```

#### Obtain Token

**Endpoint**: `POST /api/v2/auth/login`

**Request**:
```json
{
  "usuario": "username",
  "contrasena": "password"
}
```

**Response**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiracion": 120
}
```

### Core Endpoints

#### Users
- `GET /api/v1/usuario` - List all users
- `GET /api/v1/usuario/{id}` - Get user details
- `POST /api/v1/usuario` - Create user
- `PUT /api/v1/usuario/{id}` - Update user
- `DELETE /api/v1/usuario/{id}` - Delete user

#### Authentication
- `POST /api/v2/auth/login` - User login
- `POST /api/v2/auth/logout` - User logout

#### Profiles
- `GET /api/v1/perfil` - List profiles
- `POST /api/v1/perfil` - Create profile
- `PUT /api/v1/perfil/{id}` - Update profile

#### Notifications
- `GET /api/v1/aviso` - List notifications
- `POST /api/v1/aviso` - Create notification

#### Files
- `GET /api/v1/archivo` - List files
- `POST /api/v1/archivo/upload` - Upload file
- `GET /api/v1/archivo/{id}/download` - Download file

#### Security
- `GET /api/v1/seguridadaccion` - Audit log entries
- `POST /api/v1/seguridadaccion` - Log security action

### Swagger/OpenAPI

Interactive API documentation is available at:
- Development: `http://localhost:5000/swagger`
- Production: `https://api.yourdomain.com/swagger`

### Error Handling

The API returns standard HTTP status codes:
- `200 OK` - Successful request
- `201 Created` - Resource created
- `400 Bad Request` - Invalid input
- `401 Unauthorized` - Missing/invalid authentication
- `403 Forbidden` - Insufficient permissions
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

---

## 🔒 Security Features

### Authentication
- **JWT Bearer Tokens**: Token-based authentication
- **Token Expiration**: Configurable token lifetime (default 120 minutes)
- **Secure Key**: Update default JWT key in production

### Authorization
- **Role-Based Access Control (RBAC)**: User roles determine API access
- **Active Directory Integration**: LDAP/AD authentication support
- **Permission Management**: Granular permission system

### Data Security
- **Cryptography**: Built-in cryptographic utilities
- **Password Hashing**: Secure password storage
- **SQL Injection Prevention**: Entity Framework parameterized queries
- **CORS Protection**: Cross-origin request validation

---

## 📖 Development Workflow

### Setting Up Local Development Environment

```bash
# 1. Clone repository
git clone https://github.com/mrodriguex/hard.core.git
cd hard.core

# 2. Install dependencies
cd HARD.CORE
dotnet restore

# 3. Configure local database connection
# Edit appsettings.Development.json

# 4. Run migrations
dotnet ef database update -p HARD.CORE.DAT -s HARD.CORE.API

# 5. Start development server
dotnet run --project HARD.CORE.API/HARD.CORE.API.csproj
```

### Code Organization

- **Models** (`HARD.CORE.OBJ`): Data transfer objects and entity definitions
- **Business Logic** (`HARD.CORE.NEG`): Core business rules and validation
- **Services** (`HARD.CORE.SER`): Service implementations
- **Data Access** (`HARD.CORE.DAT`): Database operations
- **API** (`HARD.CORE.API`): REST endpoints and HTTP handling

### Adding New Features

1. Define data models in `HARD.CORE.OBJ`
2. Implement business logic in `HARD.CORE.NEG`
3. Create service layer in `HARD.CORE.SER`
4. Add database access in `HARD.CORE.DAT`
5. Expose API endpoints in `HARD.CORE.API/Controllers`

---

## 🧪 Testing

### Running Unit Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test HARD.CORE/HARD.CORE.Tests.csproj

# With coverage
dotnet test /p:CollectCoverage=true
```

### API Testing

#### Using Postman
1. Import API endpoints from Swagger: `https://localhost:7209/swagger/v1/swagger.json`
2. Create authentication request to obtain JWT token
3. Set token in Authorization header for protected endpoints

#### Using cURL
```bash
# Login
curl -X POST https://localhost:7209/api/v2/auth/login \
  -H "Content-Type: application/json" \
  -d '{"usuario":"admin","contrasena":"password"}'

# Authenticated request
curl -X GET https://localhost:7209/api/v1/usuario \
  -H "Authorization: Bearer {token}"
```

#### Using VS Code HTTP Extension
See `HARD.CORE.API/HARD.CORE.API.http` for pre-configured test requests.

---

## 📝 Project Dependencies

### HARD.CORE.API
- Asp.Versioning.Mvc 8.1.0
- Asp.Versioning.Mvc.ApiExplorer 8.1.0
- AutoMapper 15.0.1
- Microsoft.AspNetCore.Authentication.JwtBearer 8.0.8
- Microsoft.AspNetCore.Mvc.NewtonsoftJson 8.0.10
- Swashbuckle.AspNetCore 7.2.0
- Telerik Reporting 19.0.25.313

### HARD.CORE.NEG
- None (internal library)

### HARD.CORE.SER
- EPPlus (spreadsheet handling)
- Entity Framework (data access)

### HARD.CORE.DAT
- Microsoft.Data.SqlClient 6.0.1
- System.DirectoryServices 9.0.2

---

## 🚢 Deployment

### Prerequisites for Production
- .NET 8.0 Runtime
- SQL Server 2019+
- HTTPS certificate (SSL/TLS)
- Secure environment variables

### Build for Production

```bash
# Release build
dotnet publish -c Release -o ./publish HARD.CORE/HARD.CORE.API/HARD.CORE.API.csproj
```

### Docker Support (Optional)

Create a `Dockerfile` in `HARD.CORE.API`:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet build "HARD.CORE.API.csproj" -c Release
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80 443
ENTRYPOINT ["dotnet", "HARD.CORE.API.dll"]
```

### IIS Deployment

1. Build release version
2. Create IIS application pool (.NET CLR v4.0)
3. Deploy published files to IIS directory
4. Configure URL rewrite and HTTPS binding
5. Set up application pool recycling and monitoring

---

## 🐛 Troubleshooting

### Common Issues

#### 1. Database Connection Fails
```
Error: Cannot connect to database
Solution: 
- Verify SQL Server is running
- Check connection string in appsettings.json
- Verify user credentials and permissions
- Check firewall rules
```

#### 2. JWT Token Not Recognized
```
Error: 401 Unauthorized
Solution:
- Verify JWT key matches configuration
- Check token expiration
- Ensure Authorization header format: "Bearer {token}"
- Verify CORS configuration allows authentication
```

#### 3. CORS Errors
```
Error: CORS policy blocked request
Solution:
- Check allowed origins in Program.cs
- Verify Content-Type headers
- Enable credentials if needed
- Test with Postman (bypasses CORS)
```

#### 4. Build Fails
```
Error: Build errors during compilation
Solution:
- Run: dotnet clean
- Delete obj/ and bin/ folders
- Restore packages: dotnet restore
- Rebuild: dotnet build
```

---

## 📖 Documentation Resources

### Official Documentation
- [Microsoft .NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [JWT Authentication](https://tools.ietf.org/html/rfc7519)

### Additional Resources
- Project issue tracker: GitHub Issues
- Contribution guidelines: See CONTRIBUTING.md
- Architecture decision records: See ADR directory

---

## 👥 Contributing

We welcome contributions! Please follow these guidelines:

### Getting Started
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Commit with clear messages (`git commit -m 'Add amazing feature'`)
5. Push to your branch (`git push origin feature/amazing-feature`)
6. Open a Pull Request

### Code Standards
- Follow C# naming conventions (PascalCase for classes, camelCase for variables)
- Add XML documentation comments to public methods
- Write unit tests for new functionality
- Ensure all tests pass before submitting PR
- Update README.md if adding new features

### Pull Request Process
1. Describe changes clearly in PR title and description
2. Reference related issues (#123)
3. Include before/after screenshots for UI changes
4. Ensure CI/CD pipeline passes
5. Request review from maintainers

---

## 📄 License

This project is licensed under the **GNU General Public License v3.0** - see the [LICENSE](LICENSE) file for details.

### Key Terms of GPL v3:
- ✅ You can use this software commercially
- ✅ You can modify and distribute it
- ✅ You can use it privately
- ❌ You must disclose source code
- ❌ You must include license and copyright notice
- ❌ You must include a changelog if modified
- ❌ Same license must apply to derivatives

---

## 📞 Support & Contact

### Getting Help
- **Issues**: GitHub Issues for bug reports
- **Discussions**: GitHub Discussions for questions
- **Email**: Contact project maintainer
- **Wiki**: Check project wiki for guides

### Repository Information
- **Owner**: mrodriguex
- **Repository**: hard.core
- **Main Branch**: main
- **Latest Version**: Check Releases

---

## 🗺️ Project Roadmap

### Current Phase
- ✅ Core REST API implementation
- ✅ JWT authentication
- ✅ User and profile management
- ✅ API versioning (v1, v2)

### Planned Features
- 🔄 Expanded reporting capabilities
- 🔄 Real-time notifications (SignalR)
- 🔄 Advanced search and filtering
- 🔄 Mobile app support
- 🔄 Performance optimization
- 🔄 Enhanced audit logging

### Future Enhancements
- 📅 GraphQL API support
- 📅 Microservices architecture
- 📅 Kubernetes deployment
- 📅 Advanced analytics dashboard

---

## 📊 Project Statistics

- **Total Projects**: 6 (API, NEG, SER, DAT, OBJ, WEB)
- **Controllers**: 15+ API endpoints
- **Business Classes**: 20+ business logic classes
- **Service Classes**: 20+ service implementations
- **Target Frameworks**: .NET 8.0, .NET Framework 4.8, .NET Standard 2.0
- **License**: GPL v3

---

## 📝 Changelog

See [CHANGELOG.md](CHANGELOG.md) for version history and updates.

---

**Last Updated**: December 12, 2025  
**Maintained By**: Manuel Rodriguez  
**Repository**: https://github.com/mrodriguex/hard.core