# HR Management System

A robust, enterprise-grade Human Resource Management system built using **Clean Architecture** principles and **ASP.NET Core 8.0**. This solution showcases a modular, highly testable, and decoupled design by implementing the **SOLID** principles, dual data-access methodologies (EF Core & Raw ADO.NET), role-based JWT authentication, and automated unit testing.

---

## 🏗️ Architecture Overview

The solution is divided into **6 projects**, separating concerns strictly according to Clean Architecture guidelines. Outer layers depend on inner layers, but the core business logic remains entirely isolated.

┌────────────────────────────────────────────────────────┐
│                 HRManagement.Web (MVC)                 │
│                           &                            │
│                HRManagement.API (REST)                 │
└───────────────────────────┬────────────────────────────┘
                            │
                            ▼
┌────────────────────────────────────────────────────────┐
│                HRManagement.Application                │
│                    (Business Logic)                    │
└───────────────────────────┬────────────────────────────┘
                            │
                            ▼
┌────────────────────────────────────────────────────────┐
│                   HRManagement.Core                    │
│             (Entities, Interfaces, Rules)              │
└───────────────────────────▲────────────────────────────┘
                            │
                            │ (Implements Interfaces)
┌───────────────────────────┴────────────────────────────┐
│              HRManagement.Infrastructure               │
│             (EF Core, ADO.NET, SQL Server)             │
└────────────────────────────────────────────────────────┘              
                

### The 6 Projects Explained

1. **`HRManagement.Core` (Domain Layer):** The innermost core. Contains domain entities (`Employee`, `Leave`), custom domain exceptions, and repository interfaces. It has **zero** external dependencies.
2. **`HRManagement.Application` (Use Case Layer):** Coordinates business workflows. Contains services (`EmployeeService`, `LeaveService`, `AuthService`) implementing core business rules. 
3. **`HRManagement.Infrastructure` (Data Layer):** Manages external concerns. Implements the repository contracts defined in the Core layer using both **Entity Framework Core** and raw **ADO.NET**.
4. **`HRManagement.API` (Presentation Layer):** A headless RESTful API exposed via controllers. Handles HTTP requests/responses, speaks in JSON, and features interactive documentation via Swagger.
5. **`HRManagement.Web` (Presentation Layer):** A traditional user-facing web portal built using ASP.NET Core MVC (Model-View-Controller) and interactive Razor views.
6. **`HRManagement.Tests` (Testing Layer):** Validates application behavior and guarantees structural stability using **xUnit** and **Moq**.

---

## ⚡ Key Features & Technical Highlights

* **Clean Architecture & SOLID:** Strict separation of concerns. Core business logic remains untainted by databases, UI frameworks, or third-party packages.
* **Dual Persistence Layer:** Implements both **EF Core (Code-First Migration workflows)** for rapid development and **Raw ADO.NET (with parameterized queries)** to showcase low-level performance tuning and SQL Injection prevention.
* **Secure Authentication & Authorization:** Implements robust **JWT (JSON Web Token)** Bearer authentication paired with rigid Role-Based Access Control (RBAC) across endpoints (`Admin`, `Manager`, `Employee`).
* **Global Error Handling:** Custom async middleware catching application-wide exceptions and transforming them into normalized, production-safe **ProblemDetails (RFC 7807)** JSON payloads.
* **Defensive Design:** Complete utilization of **Dependency Injection (DI)** with scoped lifetimes to enforce the Dependency Inversion Principle.

---

## 🔄 End-to-End Data Flow

When an administrator clicks **"Edit Employee"**, the system executes the following lifecycle:

[Browser Client] ──(HTTP GET + JWT)──> [API Pipeline / Auth Middleware]
│
(Validates Claims)
│
▼
[SQL Server] <──(Raw SQL / LINQ)── [EmployeeRepository] <── [EmployeeService] <── [EmployeeController]

1. **Request:** The client dispatches an HTTP request with a bearer token.
2. **Pipeline:** Middleware validates the JWT, checks constraints like `[Authorize(Roles = "Admin")]`, and activates the controller.
3. **Service Layer:** The controller maps inputs to DTOs and invokes the decoupled `EmployeeService`.
4. **Data Access:** The service requests data from `IEmployeeRepository`. The concrete infrastructure implementation fetches the records securely from SQL Server.
5. **Response:** The system maps the returned domain entity to a safe Response DTO and returns an `HTTP 200 OK` JSON stream back to the UI.

---

## 🛠️ Tech Stack & Tooling

* **Framework:** .NET 8.0 (ASP.NET Core Web API & MVC)
* **Database:** Microsoft SQL Server
* **ORM & Data Access:** Entity Framework Core & ADO.NET (`SqlClient`)
* **Security:** System.IdentityModel.Tokens.Jwt (HMAC-SHA256)
* **Testing Suite:** xUnit, Moq
* **API Documentation:** Swagger / OpenAPI

---

## 🚀 Getting Started

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [SQL Server Express / LocalDB](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation & Setup

1. **Clone the Repository**
   ```bash
   git clone [https://github.com/your-username/HRManagement.git](https://github.com/your-username/HRManagement.git)
   cd HRManagement
2. Configure App Settings
Update the connection strings and JWT configuration details inside HRManagement.API/appsettings.json.

3. Apply Database Migrations
Using the .NET CLI, execute the EF Core tools to provision your local database instance: dotnet ef database update --project HRManagement.Infrastructure --startup-project HRManagement.API

4.Run the Application
Launch the API backend project: dotnet run --project HRManagement.API

(Optional) Launch the MVC Web interface: dotnet run --project HRManagement.Web

5.Run the Test Suite
Ensure all units execute flawlessly: dotnet test

