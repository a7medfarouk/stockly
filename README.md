# Stockly

A full-stack inventory and order management system built with ASP.NET Core Web API and Blazor WebAssembly.

## Overview

Stockly lets you manage a product catalog and process orders against live stock. Key features include:

- View, add, edit, and delete products
- Search products by name
- Create orders with one or more products
- Automatic stock validation before order is placed
- Automatic total calculation
- Stock reduction on successful order

## Tech Stack

| Layer | Technology |
|---|---|
| Backend API | ASP.NET Core 9 Web API |
| Frontend | Blazor WebAssembly (.NET 9) |
| Database | SQL Server (LocalDB) via Entity Framework Core 9 |
| API Docs | Swagger / Swashbuckle |

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9)
- SQL Server or SQL Server LocalDB (comes with Visual Studio)
- Visual Studio 2022+ or VS Code with C# extension

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/Stockly.git
cd Stockly
```

### 2. Configure the database connection

Open `Stockly.API/appsettings.json` and verify the connection string matches your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=StocklyDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

If you are using a full SQL Server instance instead of LocalDB, replace `(localdb)\\MSSQLLocalDB` with your server name.

### 3. Apply database migrations

Navigate to the API project and run:

```bash
cd Stockly.API
dotnet ef database update
```

This creates the `StocklyDb` database and all tables automatically.

### 4. Run the API

```bash
dotnet run --project Stockly.API
```

The API starts on `http://localhost:5237`. Swagger UI is available at:

```
http://localhost:5237/swagger
```

### 5. Run the Blazor client

Open a second terminal:

```bash
dotnet run --project Stockly.Client
```

The frontend starts on `http://localhost:5016` 
> Both projects must be running at the same time for the app to work.

## Project Structure

```
Stockly/
├── Stockly.API/
│   ├── Controllers/        # HTTP endpoints
│   ├── Services/           # Business logic (stock validation, totals)
│   ├── Repositories/       # Database access via EF Core
│   ├── Models/             # Database entities
│   ├── DTOs/               # Request and response shapes
│   ├── Data/               # DbContext
│   ├── Migrations/         # EF Core migrations
│   └── Program.cs          # App configuration and DI setup
│
└── Stockly.Client/
    ├── Pages/              # Blazor pages (Dashboard, Products, Orders)
    ├── Services/           # HTTP client wrappers for the API
    ├── Models/             # Client-side DTOs
    ├── Layout/             # Sidebar and main layout
    └── wwwroot/            # Static assets and CSS
```

## API Endpoints

### Products

| Method | Route | Description |
|---|---|---|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| GET | `/api/products/search?name=` | Search products by name |
| POST | `/api/products` | Create a product |
| PUT | `/api/products/{id}` | Update a product |
| DELETE | `/api/products/{id}` | Delete a product |

### Orders

| Method | Route | Description |
|---|---|---|
| GET | `/api/orders` | Get all orders |
| GET | `/api/orders/{id}` | Get order with line items |
| POST | `/api/orders` | Create an order |

Full request/response schemas are documented in the Swagger UI.

## Frontend Pages

| Route | Page |
|---|---|
| `/` | Dashboard with stats and recent orders |
| `/products` | Product list with search and delete |
| `/products/add` | Add a new product |
| `/products/edit/{id}` | Edit an existing product |
| `/orders` | Order history |
| `/orders/create` | Create a new order |
| `/orders/{id}` | Order detail view |
