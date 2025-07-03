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
4. Run the application:
   ```bash
   dotnet run
   ```
5. Open your browser and navigate to `http://localhost:5000`

## Getting Started
1. Clone the repository.
2. Navigate to the project directory.
3. Run the application using the command: `dotnet run`.
4. Access the application in your web browser at `http://localhost:5000`.

## Localization
The application supports English and Ukrainian languages. Localization files are provided in the `Resources` directory.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any suggestions or improvements.