# SportsPro

SportsPro is an ASP.NET Core MVC web application for managing sports equipment products, customers, technicians, incidents, and product registrations.

The application demonstrates the use of the Model-View-Controller (MVC) architecture, Entity Framework Core, database migrations, CRUD functionality, and relational data management.

## Features

* **Product Management** — View, add, edit, and delete products.
* **Customer Management** — View, add, edit, and delete customer records.
* **Technician Management** — Manage technician information.
* **Incident Management** — Track and manage customer incidents associated with products and technicians.
* **Registration Management** — Register products to customers and manage existing registrations.
* **CRUD Functionality** — Create, read, update, and delete records through the web interface.
* **Sample Data** — The application uses Entity Framework Core data seeding to provide initial records.

## Technologies

* **C#**
* **ASP.NET Core MVC**
* **.NET 10**
* **Entity Framework Core 10**
* **SQLite**
* **HTML/CSS**
* **Razor Views**
* **Git/GitHub**

## Database

The original project used Microsoft SQL Server LocalDB, which is primarily designed for Windows environments. To make the application compatible with macOS and other platforms, the database implementation was adapted to use **SQLite**.

Entity Framework Core migrations are used to create and update the SQLite database schema.

## Running the Application

### Prerequisites

* .NET 10 SDK

### Clone the Repository

```bash
git clone https://github.com/YOUR-USERNAME/SportsPro.git
cd SportsPro
```

### Restore Dependencies

```bash
dotnet restore
```

### Apply Database Migrations

```bash
dotnet ef database update
```

### Run the Application

```bash
dotnet run
```

The terminal will provide a local URL where the application can be accessed in a web browser.

## Project Structure

```text
SportsPro/
├── Controllers/                 # MVC controllers
├── Migrations/                  # Entity Framework Core migrations
├── Models/                      # Data models and view models
├── Views/                       # Razor views
├── wwwroot/                     # Static files
├── Program.cs                   # Application configuration and startup
├── SportsPro.csproj             # Project configuration and dependencies
├── appsettings.Development.json # Development-specific configuration
└── appsettings.json             # Application configuration
```

## Project Highlights

This project provided experience with:

* Building and modifying an ASP.NET Core MVC application
* Implementing CRUD operations
* Working with Entity Framework Core
* Creating and applying database migrations
* Managing relational data and model relationships
* Debugging routing and controller/view issues
* Adapting a Windows-oriented SQL Server LocalDB application for cross-platform use with SQLite
* Using Git for version control and maintaining incremental commits

## Future Improvements

Potential future improvements include:

* Additional validation and error handling
* Improved UI/UX
* Additional automated testing
* Deployment to a cloud hosting platform
* Further database and application security improvements
