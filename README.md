# E-Commerce API

RESTful backend for an e-commerce platform, built with .NET 8 and Clean Architecture.

**Live Demo (Swagger):** <add-your-full-swagger-url>

---

## Overview

E-Commerce API is the backend of a full-stack online store. It handles authentication, product and order management, online payments, ratings, wishlists, email notifications, and caching. It is consumed by an Angular frontend.

## Features

- Authentication with ASP.NET Core Identity and JWT, delivered through an HttpOnly cookie
- Order management
- Online payments with Stripe
- Email notifications over SMTP (MailKit)
- Product ratings
- Wishlist
- Image upload and management
- Redis caching and in-memory caching
- Request validation with FluentValidation
- CQRS with MediatR
- Global exception handling middleware
- Swagger / OpenAPI documentation
- CORS configured for the Angular client with credentials

## Architecture

Clean Architecture with strict layer separation. Dependencies point inward only.

| Project | Responsibility |
|---|---|
| `Ecom.Domain` | Entities and core business rules. No dependencies on other projects. |
| `Ecom.Application` | Use cases, interfaces, validators, mapping profiles. |
| `Ecom.Infastructure` | EF Core, Identity, JWT generation, Stripe, email, Redis, repositories, Unit of Work. |
| `Ecom.API` | Controllers, middleware, Swagger, CORS, dependency injection. |

Patterns used: Generic Repository, Unit of Work, CQRS, Dependency Injection.

## Tech Stack

- .NET 8, ASP.NET Core Web API
- Entity Framework Core with SQL Server
- ASP.NET Core Identity and JWT Bearer
- MediatR, FluentValidation, AutoMapper
- Stripe.net
- MailKit / MimeKit
- Redis (StackExchange.Redis)
- Swagger (Swashbuckle)

## Run Locally

Requirements: .NET 8 SDK, SQL Server (or LocalDB), Redis, a Stripe test account, an SMTP account.

```bash
git clone https://github.com/<your-username>/<your-repo>.git
cd <your-repo>/Ecom.API
```

Set your configuration with user secrets. Do not store real credentials in `appsettings.json`.

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\\mssqllocaldb;Database=EcomDb;Trusted_Connection=True;"
dotnet user-secrets set "ConnectionStrings:redis" "localhost"
dotnet user-secrets set "Token:Secret" "<long-random-secret>"
dotnet user-secrets set "Token:Issure" "https://localhost:7243/"
dotnet user-secrets set "EmailSetting:Smtp" "smtp.gmail.com"
dotnet user-secrets set "EmailSetting:Port" "587"
dotnet user-secrets set "EmailSetting:From" "<sender-email>"
dotnet user-secrets set "EmailSetting:Username" "<sender-email>"
dotnet user-secrets set "EmailSetting:Password" "<app-password>"
dotnet user-secrets set "StripeSetting:PublishKey" "<stripe-publishable-key>"
dotnet user-secrets set "StripeSetting:SecretKey" "<stripe-secret-key>"
dotnet user-secrets set "StripeSetting:WebhookSecret" "<stripe-webhook-secret>"
```

Apply migrations and run:

```bash
dotnet ef database update --project ../Ecom.Infastructure --startup-project .
dotnet run
```

Open `https://localhost:<port>/swagger`.

## Project Structure

```
EcommeceSolution/
  Ecom.API/
  Ecom.Application/
  Ecom.Domain/
  Ecom.Infastructure/
  EcommeceSolution.sln
```

## Author

**Mohannad Emad** - Senior Software Developer (Angular and .NET Core)

- LinkedIn: https://www.linkedin.com/in/mohannad-emad-eldin-ramadan-499141263
- Email: mohannad.emad535@gmail.com
- Phone: 01144244060
