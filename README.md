# TaskPlanner
Task Planner Application

## Overview
TaskPlanner is a web application built using .NET Core Razor Pages that allows users to manage their tasks efficiently. Users can add, remove, and mark tasks as completed. Each task includes a Name, Description, and Due Date.

## Features
- Add new tasks with details
- Edit existing tasks
- Delete tasks
- Mark tasks as completed
- Supports localization in English and Ukrainian

## Project Structure
- **Pages**: Contains Razor Pages for task management.
- **Models**: Contains the Task model definition.
- **Services**: Implements task management services with file-based storage.
- **Data**: Stores tasks in a JSON file.
- **Resources**: Contains localization files for English and Ukrainian.
- **wwwroot**: Contains static files such as CSS and JavaScript.
- **Configuration Files**: Includes settings and project metadata.

## Prerequisites

To run this TaskPlanner application, you need to install the .NET SDK on your operating system.

### Windows

#### Option 1: Using Windows Installer (Recommended)
1. Visit the [.NET Download page](https://dotnet.microsoft.com/download)
2. Download the .NET SDK for Windows
3. Run the installer and follow the installation wizard

#### Option 2: Using Package Managers
**Using Chocolatey:**
```powershell
choco install dotnet-sdk
```

**Using winget:**
```powershell
winget install Microsoft.DotNet.SDK.8
```

### macOS

#### Option 1: Using Homebrew (Recommended)
```bash
brew install --cask dotnet
```

#### Option 2: Manual Download
1. Visit the [.NET Download page](https://dotnet.microsoft.com/download)
2. Download the .NET SDK for macOS
3. Run the .pkg installer

### Linux

#### Ubuntu/Debian
```bash
# Add Microsoft package repository
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Install .NET SDK
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

#### CentOS/RHEL/Fedora
```bash
# Add Microsoft package repository
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm

# Install .NET SDK
sudo yum install dotnet-sdk-8.0
```

#### Arch Linux
```bash
sudo pacman -S dotnet-sdk
```

### Verify Installation
After installation on any OS, verify that .NET is properly installed:
```bash
dotnet --version
```

### IDE/Editor Recommendations
- **Visual Studio Code** with C# extension (cross-platform)
- **Visual Studio** (Windows/Mac)
- **JetBrains Rider** (cross-platform)

### Running the Application
Once you have the prerequisites installed:

1. Open terminal/command prompt and navigate to the project directory
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Trust the HTTPS development certificate (first time only):
   ```bash
   dotnet dev-certs https --trust
   ```
5. Run the application:
   ```bash
   dotnet run
   ```
6. Open your browser and navigate to:
   - **HTTPS (Recommended):** `https://localhost:5001`
   - **HTTP:** `http://localhost:5000`

**Note for Safari users:** Use the HTTPS URL (`https://localhost:5001`) to avoid "HTTPS-Only" errors.

## Getting Started
1. Clone the repository.
2. Navigate to the project directory.
3. Trust the HTTPS certificate: `dotnet dev-certs https --trust`
4. Run the application using the command: `dotnet run`.
5. Access the application in your web browser at:
   - **HTTPS (Recommended):** `https://localhost:5001`
   - **HTTP:** `http://localhost:5000`

## Testing

TaskPlanner includes a comprehensive test suite to ensure code quality and reliability. The tests are organized in a separate project called `TaskPlanner.Tests`.

### Test Project Structure
- **Models Tests**: Unit tests for TaskItem model validation and behavior
- **Services Tests**: Tests for FileTaskService CRUD operations and data persistence
- **Page Model Tests**: Tests for Razor Page models and their interactions
- **Integration Tests**: End-to-end tests for web application functionality

### Test Framework and Tools
- **xUnit**: Primary testing framework
- **Moq**: Mocking framework for isolating dependencies
- **Microsoft.AspNetCore.Mvc.Testing**: Integration testing support
- **Test Coverage**: 36 tests covering models, services, page models, and integration scenarios

### Running Tests

#### Run All Tests
```bash
# Navigate to the test project directory
cd TaskPlanner.Tests

# Run all tests
dotnet test
```

#### Run Tests with Detailed Output
```bash
dotnet test --logger "console;verbosity=detailed"
```

#### Run Tests with Coverage (if coverage tools are installed)
```bash
dotnet test --collect:"XPlat Code Coverage"
```

#### Run Specific Test Categories
```bash
# Run only model tests
dotnet test --filter "FullyQualifiedName~Models"

# Run only service tests
dotnet test --filter "FullyQualifiedName~Services"

# Run only page model tests
dotnet test --filter "FullyQualifiedName~Pages"

# Run only integration tests
dotnet test --filter "FullyQualifiedName~Integration"
```

### Test Categories

#### 1. Model Tests (`TaskItemTests`)
- Task creation and property validation
- ID generation and uniqueness
- Completion status management
- Date validation for past and future dates

#### 2. Service Tests (`FileTaskServiceTests`)
- CRUD operations (Create, Read, Update, Delete)
- File-based data persistence
- Task retrieval by ID
- Multiple task management
- Error handling for non-existent tasks
- Task completion functionality

#### 3. Page Model Tests (`TaskPageModelTests`)
- Index page task listing
- Create page functionality
- Edit page operations
- Delete page confirmation
- Model validation
- Redirect behavior

#### 4. Integration Tests (`WebApplicationIntegrationTests`)
- Application startup and configuration
- HTTP endpoint accessibility
- Page rendering verification
- Invalid request handling
- Error page responses

### Test Data Management
- Tests use temporary files for data storage to avoid conflicts
- Automatic cleanup of test data after each test run
- Isolated test execution to prevent interference between tests

### Continuous Integration
The test suite is designed to run in CI/CD environments and provides:
- Fast execution (< 1 second for most tests)
- Clear failure reporting
- Comprehensive coverage of critical functionality
- Reliable and repeatable results

### Development Workflow
1. **Write tests first** (TDD approach recommended)
2. **Run tests frequently** during development
3. **Ensure all tests pass** before committing changes
4. **Add tests** for new features and bug fixes

## Localization
The application supports English and Ukrainian languages. Localization files are provided in the `Resources` directory.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any suggestions or improvements.