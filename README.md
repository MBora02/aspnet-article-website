# ArticleWebsite

ArticleWebsite is a layered web application built with **ASP.NET Core**.  
The project follows **Onion Architecture** principles to keep business logic independent from infrastructure and presentation concerns.

The main goal of the project is to provide a clean and maintainable structure while demonstrating modern .NET application practices.

---

## Overview

The solution is divided into multiple layers, each with a clear responsibility.  
Dependencies always flow inward, ensuring that core business logic remains isolated.

The application consists of:
- A **Web API** that exposes application functionality
- A **Web UI** (MVC / Razor Views) that consumes the API
- A shared **DTO layer** for data transfer
- A persistence layer for database access

---

## Architecture

The project follows **Onion Architecture**.

### Layers

#### Domain
- Contains core business entities and domain rules
- Has no dependency on any other layer

#### Application
- Contains application use cases and business logic
- Uses CQRS-style separation with MediatR
- Includes validation logic using FluentValidation
- Depends only on the Domain layer

#### Infrastructure (Persistence)
- Handles database access and external services
- Uses Entity Framework Core with SQL Server
- Implements repository interfaces defined in the Application layer
- Contains file system related services

#### Presentation – Web API
- Exposes RESTful endpoints
- Handles authentication and request routing
- Communicates with the Application layer via MediatR

#### Presentation – Web UI
- ASP.NET Core MVC application using Razor Views
- Communicates with the Web API using HttpClient
- Does not directly access the database or business logic

> Architecture diagrams will be added later.

---

## Design Approach

- **Onion Architecture** for separation of concerns
- **CQRS-style request handling** using MediatR
- **DTO-based communication** between layers
- **Repository abstraction** for data access
- **Dependency Injection** via ASP.NET Core built-in container

---

## Technologies

- ASP.NET Core (.NET 8)
- ASP.NET Core Web API
- ASP.NET Core MVC (Razor Views)
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- JWT Bearer Authentication
- HttpClientFactory

---

## Notes

- Some features such as image uploads and PDF-related functionality may have known issues.
- These parts are planned to be improved and stabilized in future updates.
- Architecture diagrams and screenshots will be added later.

---

## Project Structure

```text```
ArticleWebsite
├── Core
│   ├── ArticleWebsite.Domain
│   └── ArticleWebsite.Application
├── Infrastructure
│   └── ArticleWebsite.Persistence
├── Presentation
│   └── ArticleWebsite.WebApi
└── Frontends
    ├── ArticleWebsite.WebUI
    └── ArticleWebsite.Dto
