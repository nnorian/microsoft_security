# SafeVault - Secure Password Management System

A secure, terminal-based password vault application built with .NET 9.0, demonstrating best practices in application security and secure coding principles.

## Features

- **Secure User Authentication**: BCrypt password hashing with salt
- **Input Sanitization**: Protection against common injection attacks
- **Role-Based Access Control**: Admin and user role separation
- **Comprehensive Security Tests**: 14 automated security regression tests
- **In-Memory Testing**: Mock repository for fast, isolated unit tests

## Security Features

SafeVault implements multiple layers of security protection:

### 1. Password Security
- BCrypt hashing algorithm with automatic salt generation
- Passwords are never stored in plain text
- Secure password verification

### 2. Input Sanitization
Protection against:
- **SQL Injection**: Removes SQL keywords and dangerous characters
- **Cross-Site Scripting (XSS)**: Strips HTML tags and script elements
- **Command Injection**: Filters shell commands and execution attempts
- **Path Traversal**: Prevents directory traversal attacks

### 3. Authentication & Authorization
- Null and empty input validation
- Secure session management
- Role-based access control middleware
- Unauthorized access prevention

## Project Structure

```
microsoft_security/
├── SafeVault/                      # Main application
│   ├── Database/
│   │   └── DbConfig.cs            # Database configuration
│   ├── Middleware/
│   │   └── RoleAuthorisation.cs   # Role-based access control
│   ├── Models/
│   │   └── User.cs                # User model
│   ├── Services/
│   │   ├── AuthService.cs         # Authentication logic
│   │   ├── InputSanitizer.cs      # Input sanitization
│   │   └── UserRepository.cs      # Data access layer
│   ├── Program.cs                 # Entry point
│   └── SafeVault.csproj
└── SafeVaultTests/                # Test suite
    ├── SecurityRegressionTests.cs # Security-focused tests
    ├── TestAuth.cs                # Authentication tests
    ├── TestInputValidation.cs     # Input validation tests
    └── SafeVaultTests.csproj
```

## Requirements

- **.NET 9.0 SDK** or higher
- MySQL database (optional - tests use in-memory repository)

## Installation

1. Clone the repository:
```bash
cd /home/nnorian/dev/microsoft_fullstack/microsoft_security
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build SafeVault/SafeVault.csproj
```

## Running the Application

Start the SafeVault terminal application:

```bash
dotnet run --project SafeVault/SafeVault.csproj
```

### Usage

1. Choose option `1` to register a new user
2. Choose option `2` to login with existing credentials
3. All inputs are automatically sanitized for security

## Running Tests

Execute all security regression tests:

```bash
dotnet test SafeVaultTests/SafeVaultTests.csproj
```

Run tests with detailed output:

```bash
dotnet test SafeVaultTests/SafeVaultTests.csproj --verbosity normal
```

### Test Coverage

The test suite includes 14 comprehensive tests:

#### Security Regression Tests (10)
1. Password hashing irreversibility
2. SQL injection prevention
3. XSS prevention
4. Unauthorized access prevention
5. Role-based access control
6. Password complexity validation
7. Command injection prevention
8. Path traversal prevention
9. Session fixation prevention
10. Empty/null input handling

#### Authentication Tests (2)
1. Invalid login attempt handling
2. User role authorization

#### Input Validation Tests (2)
1. SQL injection string sanitization
2. XSS payload sanitization

## Configuration

### Database Configuration

Edit `SafeVault/Database/DbConfig.cs` to configure your MySQL connection:

```csharp
public static class DbConfig
{
    public const string Conn = "Server=localhost;Database=SafeVault;User=root;Password=your_password;";
}
```

### Database Schema

```sql
CREATE DATABASE SafeVault;

USE SafeVault;

CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL DEFAULT 'user',
    Email VARCHAR(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

## Dependencies

- **BCrypt.Net-Next** (4.0.3) - Password hashing
- **MySql.Data** (9.2.0) - MySQL database connectivity
- **Microsoft.AspNetCore.Http** (2.2.0) - HTTP abstractions
- **NUnit** (4.4.0) - Testing framework
- **NUnit3TestAdapter** (5.2.0) - Test adapter
- **Microsoft.NET.Test.Sdk** (18.0.1) - Test SDK

## Security Best Practices Implemented

1. **Defense in Depth**: Multiple layers of security controls
2. **Input Validation**: All user input is sanitized before processing
3. **Secure Password Storage**: Industry-standard BCrypt hashing
4. **Principle of Least Privilege**: Role-based access control
5. **Secure Defaults**: Fail-safe defaults in authentication
6. **Error Handling**: Secure error messages without information leakage
7. **Testing**: Comprehensive automated security tests

## Testing Architecture

SafeVault uses the **Repository Pattern** with in-memory implementations for testing:

- `UserRepository`: Production MySQL implementation
- `InMemoryUserRepository`: Fast, isolated test implementation

This allows tests to run without database dependencies while maintaining production code integrity.

## OWASP Top 10 Coverage

SafeVault addresses multiple OWASP Top 10 vulnerabilities:

- ✅ A01: Broken Access Control
- ✅ A02: Cryptographic Failures
- ✅ A03: Injection
- ✅ A07: Identification and Authentication Failures

## Contributing

When contributing to SafeVault, please ensure:

1. All security tests pass
2. New features include corresponding security tests
3. Code follows secure coding practices
4. Input validation is implemented for all user inputs

## License

This project is intended for educational purposes to demonstrate secure coding practices in C# and .NET.

## Test Results

```
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:    14, Skipped:     0, Total:    14, Duration: 954 ms
```

All 14 tests passing ✅

---

**Built with security in mind. Keep your data safe with SafeVault.**
