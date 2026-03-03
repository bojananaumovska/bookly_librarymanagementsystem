# Bookly – Enterprise Library Management System

Bookly is a production-structured ASP.NET MVC 5 web application that models a real world library domain with secure authentication, role-based authorization, business rule enforcement, and relational integrity using Entity Framework.

This project emphasizes clean architecture principles, domain modeling, and security conscious implementation.

---

# 🎬 Live Demonstration

## ▶ Demo Video

Watch the full system walkthrough here:

The demo covers:
- User registration & authentication
- Role-based access (Admin, Librarian, User)
- Book & inventory management
- Loan lifecycle validation
- Reservation workflow
- Security protections in action

---

# Application Screenshots

## Dashboard

## Book Management

## Loan Management

## Reservation Management

## Role-Based Access Example

---

# 🗄 Database Architecture

## ER Diagram

The database schema is generated using Entity Framework Code-First and integrates ASP.NET Identity tables with domain entities.

Key relationships:

- Book → Author (1:N)
- Book → Category (1:N)
- Book → BookCopy (1:N)
- BookCopy → Loan (1:N)
- User → Loan (1:N)
- User → Reservation (1:N)

This structure enforces referential integrity and models real-world inventory constraints.

---

# 1. System Architecture

The application follows the ASP.NET MVC architectural pattern:

• **Models** – Domain entities with validation & integrity constraints  
• **Controllers** – Application service layer handling business workflows  
• **Views** – Razor-based UI rendering  

---

# 2. Domain Modeling

The system models a real library workflow with proper abstraction of physical inventory.

## Core Entities

### Book

Represents a logical book entity.

- ISBN-13 validation enforced via regex
- Strong input validation constraints
- Linked to Author and Category
- One-to-many relationship with BookCopy
- One-to-many relationship with Reservation

---

### BookCopy

Represents a physical inventory unit.

This abstraction enables:
- Multiple physical copies of the same title
- Independent loan tracking per copy
- Prevention of duplicate borrowing

---

### Loan

Represents borrowing lifecycle.

Constructor-level defaults:

- LoanDate = current time
- DueDate = +14 days
- Status = Active

Business invariant enforced:

A BookCopy cannot have multiple Active loans simultaneously.

---

### Reservation

Models future allocation intent.

Status-driven lifecycle:
- Active
- Cancelled
- Completed
- Expired

---

### ApplicationUser (Identity Extension)

Extends ASP.NET IdentityUser with:

- FirstName
- LastName
- CreatedAt
- Navigation collections (Loans, Reservations)

This creates a cohesive relationship between authentication and domain behavior.

---

# 3. Database Strategy

Technology stack:

- Entity Framework 6
- Code-First migrations
- SQL Server LocalDB
- IdentityDbContext integration

Relational integrity is enforced via explicit foreign keys:

- Book → Author
- Book → Category
- BookCopy → Book
- Loan → BookCopy
- Loan → ApplicationUser
- Reservation → Book
- Reservation → ApplicationUser

This ensures transactional consistency.

---

# 4. Authentication & Authorization

## Authentication

Implemented using:

- ASP.NET Identity
- Cookie-based authentication
- Claims-based identity generation

Secure session handling with Identity middleware.

---

## Role-Based Authorization (RBAC)

- Admin
- Librarian
- User

### Access Control Model

Admin:
- Full system privileges

Librarian:
- Manage books, loans & reservations

User:
- Read-only access to personal records

---

# 5. Security Implementation

Security is treated as a first-class concern.

## Input Validation

- Required
- StringLength
- Range
- RegularExpression
- DataType

All constraints enforced before persistence.

---

## XSS Mitigation

- HTML tag blocking via regex
- Razor automatic output encoding

---

## CSRF Protection

All state-changing POST endpoints use:

`[ValidateAntiForgeryToken]`

---

## Overposting Protection

Sensitive operations use:

`[Bind(Include = "...")]`

---

## Business Rule Enforcement

- Prevent double-loaning of same BookCopy
- Enforce role permissions before state mutation
- Restrict record visibility based on identity

---

# 6. Client-Side Enhancements

- jQuery
- JavaScript enhanced tables
- Bootstrap UI components

Features include:

- Sorting
- Filtering
- Pagination
- Responsive layout

---

# 7. Deployment & Execution

Requirements:

- Visual Studio
- SQL Server LocalDB
- .NET Framework (MVC 5 compatible)

Steps:

1. Clone repository
2. Restore NuGet packages
3. Run Update-Database (if migrations enabled)
4. Run application

Default connection: `DefaultConnection`

---

# 8. Engineering Principles Demonstrated

- Proper domain abstraction (Book vs BookCopy)
- Referential integrity enforcement
- Role based access control
- Defense-in-depth validation
- Secure form handling
- Clean separation of concerns

---

# 9. Future Enhancements

- Automated loan overdue detection
- Email notification service
- REST API layer
- Audit logging
- Soft-delete implementation
- Migration to ASP.NET Core
