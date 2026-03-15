# Bookly – Enterprise Library Management System

**Bookly** is a production-ready ASP.NET MVC 5 web application modeling a real-world library with secure authentication, role-based authorization, business rule enforcement, and relational integrity using Entity Framework. It emphasizes clean architecture, domain modeling, and security-conscious implementation.

---

## 🎬 Live Demonstration

### Demo Video


---

## 📸 Application Screenshots

| Feature        | Screenshot |
|----------------|------------|
| Books          | ![Books](Content/Images/Screenshots/books.png) |
| Authors        | ![Authors](Content/Images/Screenshots/authors.png) |
| Book Copies    | ![Book Copies](Content/Images/Screenshots/book_copies.png) |
| Categories     | ![Categories](Content/Images/Screenshots/category.png) |
| Loans          | ![Loans](Content/Images/Screenshots/loans.png) |
| Reservations   | ![Reservations](Content/Images/Screenshots/reservations.png) |

---

## 🗄 Database Architecture

### ER Diagram
The database is generated using Entity Framework Code-First and integrates ASP.NET Identity tables with domain entities.

**Key Relationships:**

- Book → Author (1:N)  
- Book → Category (1:N)  
- Book → BookCopy (1:N)  
- BookCopy → Loan (1:N)  
- User → Loan (1:N)  
- User → Reservation (1:N)  

This structure enforces referential integrity and models real-world inventory constraints.

---

## 1. System Architecture

**Pattern:** ASP.NET MVC  

- **Models** – Domain entities with validation & integrity constraints  
- **Controllers** – Handles business workflows  
- **Views** – Razor-based UI rendering  

---

## 2. Authentication & Authorization

### Authentication
- ASP.NET Identity  
- Cookie-based authentication  
- Claims-based identity generation  
- Secure session handling with Identity middleware  

### Role-Based Authorization (RBAC)
**Roles:** Admin, Librarian, User  

**Access Control Model:**

- **Admin:** Full system privileges  
- **Librarian:** Manage books, loans & reservations  
- **User:** Read-only access to personal records  

---

## 3. Security Implementation

### Input Validation
- Required, StringLength, Range, RegularExpression, DataType  
- Enforced before persistence  

### XSS Mitigation
- HTML tag blocking via regex  
- Razor automatic output encoding  

### CSRF Protection
- `[ValidateAntiForgeryToken]` on all state-changing POST endpoints  

### Overposting Protection
- `[Bind(Include = "...")]` on sensitive operations  

### Business Rule Enforcement
- Prevent double-loaning of the same BookCopy  
- Enforce role permissions before state mutation  
- Restrict record visibility based on identity  

---

## 4. Client-Side Enhancements
- jQuery & JavaScript-enhanced tables  
- Bootstrap UI components  
- Features: sorting, filtering, pagination, responsive layout  

---

## 5. Deployment & Exencution

**Requirements:**
- Visual Studio  
- SQL Server LocalDB  
- .NET Framework (MVC 5 compatible)  

**Steps:**
1. Clone repository  
2. Restore NuGet packages  
3. Run `Update-Database` (if migrations enabled)  
4. Run application  

**Default connection string:** `DefaultConnection`

---

## 6. Currently Working On

- **Containerizing the application** – packaging the web app and database in Docker containers to simplify deployment, testing, and environment setup.
- **Email notification service** – sending automatic emails to users for loan creation, due dates, and overdue notifications.  
- **Audit logging** – tracking user actions like creating, editing, or deleting loans and books for accountability.  
- **Migration to ASP.NET Core** – upgrading the project to the modern ASP.NET Core framework for better performance, cross-platform support, and maintainability.  
- **AI-based Book Recommendation System** – suggesting books to users based on their past loans using an AI model, so each user sees personalized book suggestions.
