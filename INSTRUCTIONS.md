# Instructions

## Table of Contents

1. [Version](#version)
2. [Last Updated](#last-updated)
3. [System Requirements](#system-requirements)
4. [Prerequisites](#prerequisites)
5. [Setup and Usage Instructions](#setup-and-usage-instructions)
6. [Initial Setup](#initial-setup)
7. [Install Dependencies](#install-dependencies)
8. [Running Scripts](#running-scripts)
9. [Development Commands](#development-commands)
10. [Available Commands](#available-commands)
11. [Best Practices](#best-practices)
12. [Documentation](#documentation)
13. [Extending the Application](#extending-the-application)
14. [External Resources](#external-resources)

## Version

1.0.0

## Last Updated

June 2026

## System Requirements

- **Operating System**: Windows (for optimal .NET Framework support)
- **.NET Framework**: Compatible version (see project properties)
- **SQL Server**: SQL Server or SQL Server Express
- **Visual Studio**: 2010 or later (recommended)
- **Memory**: 2GB RAM minimum, 4GB recommended
- **Disk Space**: 1GB for application and dependencies

## Prerequisites

You'll need to install the following software:

- .NET Framework (compatible with the project requirements)
- SQL Server Management Studio (SSMS)
- Visual Studio (recommended for development)
- Git (for version control)

## Setup and Usage Instructions

Follow these steps to set up and use the application.

## Initial Setup

1. Clone the project to your computer:
   ```bash
   git clone https://github.com/orassayag/papaito.git
   cd papaito
   ```

2. Open the solution in Visual Studio.

## Install Dependencies

1. Restore NuGet packages if prompted by Visual Studio.
2. Ensure all project references are correctly set up.
3. Verify that the Data Access Layer (DAL) project is referenced by the main web application.

## Database Setup

1. Open SQL Server Management Studio.
2. Look for the SQL script or database backup in the repository to recreate the database.
3. Run the scripts to create the tables.
4. Update the connection string in `web.config` to point to your local database instance:
   ```xml
   <connectionStrings>
       <add name="..." connectionString="..." />
   </connectionStrings>
   ```

## Running the Application

1. Set the Web application project (Papaito) as the Startup Project in Visual Studio.
2. Press `F5` or click `Start Debugging` (IIS Express).
3. The application will launch in your default web browser.

## Running Scripts

(This project uses Visual Studio for build and deployment. Scripts are managed through the Visual Studio IDE.)

## Development Commands

### Building the Project

- **Build Solution**: Press `Ctrl+Shift+B` or right-click solution → Build Solution
- **Rebuild Solution**: Right-click solution → Rebuild Solution
- **Clean Solution**: Right-click solution → Clean Solution

### Debugging

- **Start Debugging**: Press `F5`
- **Start Without Debugging**: Press `Ctrl+F5`
- **Attach to Process**: Debug → Attach to Process

### Database Management

- **Server Explorer**: View → Server Explorer (for database connections)
- **SQL Server Object Explorer**: View → SQL Server Object Explorer

## Available Commands

### Visual Studio Commands

- **Build**: `Build Solution` (Ctrl+Shift+B)
- **Debug**: `Start Debugging` (F5)
- **Run Without Debugging**: `Start Without Debugging` (Ctrl+F5)
- **Clean**: `Clean Solution`
- **Rebuild**: `Rebuild Solution`

### Package Manager Console Commands

(If using Entity Framework or other NuGet packages that require it)

- `Install-Package <package-name>`: Install a NuGet package
- `Update-Package <package-name>`: Update a NuGet package
- `Uninstall-Package <package-name>`: Uninstall a NuGet package

## Project Structure

```text
Dal/             - Data Access Layer using LINQ-TO-SQL
Papaito/         - Main Web Application using ASP.NET Web-Forms
```

## Best Practices

### Development Best Practices

1. **Use Source Control**: Commit changes frequently with descriptive commit messages
2. **Follow Coding Conventions**: Adhere to .NET Framework coding guidelines
3. **Use Meaningful Names**: Choose clear, descriptive names for variables, methods, and controls
4. **Comment Your Code**: Add comments for complex logic or non-obvious decisions
5. **Test Changes**: Test your changes thoroughly before committing
6. **Use Master Pages**: Maintain consistent UI across pages using master pages
7. **Separate Concerns**: Keep business logic in App_Code or separate classes, not in code-behind files
8. **Use the Logger**: Utilize the Logger class for logging instead of Console.WriteLine or Response.Write

### Database Best Practices

1. **Backup Regularly**: Always backup your database before making structural changes
2. **Use Transactions**: For multiple related database operations, use transactions to ensure data integrity
3. **Index Strategically**: Add indexes to frequently queried columns to improve performance
4. **Normalize Data**: Follow database normalization principles to reduce redundancy
5. **Use Parameterized Queries**: LINQ-to-SQL handles this automatically, but be cautious with raw SQL

### Security Best Practices

1. **Validate All Input**: Sanitize and validate user input on both client and server sides
2. **Use Authentication**: Always authenticate admin users before granting access
3. **Secure Connection Strings**: Avoid hardcoding connection strings; use configuration files
4. **Handle Errors Gracefully**: Don't expose detailed error messages to end users
5. **Use HTTPS**: In production, always use HTTPS to encrypt data in transit

## Documentation

### Related Documents

- [README.md](README.md) - Project overview and features
- [CONTRIBUTING.md](CONTRIBUTING.md) - Development and contribution guidelines
- [CHANGELOG.md](CHANGELOG.md) - Version history and changes
- [LICENSE](LICENSE) - License information

### Code Documentation

- Look for XML documentation comments in the source code
- Check `App_Code` directory for shared utility classes and their documentation
- Review LINQ-to-SQL model in `Dal/Dal/PapaitoDal.dbml` for database structure

## Extending the Application

### Adding New Pages

1. Right-click the Papaito project in Solution Explorer
2. Select Add → New Item
3. Choose "Web Form using Master Page" (recommended for consistency)
4. Select the appropriate master page (MasterStudio.master or Admin.master)
5. Add your controls and logic to the new page

### Adding Database Tables

1. Open the LINQ-to-SQL designer (`Dal/Dal/PapaitoDal.dbml`)
2. Drag tables from Server Explorer to the designer surface
3. Save the changes - the designer will automatically generate the entity classes
4. Update `PapaDal.cs` if you need custom data access methods

### Adding Admin Features

1. Create a new web form in the `Admin/` directory
2. Use `Admin.master` as the master page
3. Add authentication checks in the code-behind to ensure only authorized users can access
4. Implement your admin functionality using the DAL for data access

### Adding Client-Side Functionality

1. Add jQuery scripts to the `Galleryjs/` directory or appropriate location
2. Reference the scripts in your web forms
3. Use jQuery for DOM manipulation, AJAX calls, and client-side validation

## Troubleshooting

### Build Errors

- Ensure you have the correct .NET Framework installed.
- Check if all assemblies and dependencies are correctly referenced.
- Clean the solution and rebuild.

### Database Connection Failed

- Verify SQL Server is running.
- Ensure the connection string inside `web.config` matches your local server name and authentication method.
- Check that the database file exists and is not corrupted.

### Page Not Found Errors

- Verify the page exists in the correct directory
- Check URL routing (if used)
- Ensure the web server is running and configured correctly

### JavaScript Errors

- Use browser developer tools (F12) to view console errors
- Verify jQuery is correctly referenced
- Check for syntax errors in your JavaScript code

## External Resources

- [ASP.NET Web Forms Documentation](https://learn.microsoft.com/en-us/aspnet/web-forms/)
- [LINQ-to-SQL Documentation](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/)
- [jQuery Documentation](https://jquery.com/)
- [SQL Server Documentation](https://learn.microsoft.com/en-us/sql/sql-server/)
- [Visual Studio Documentation](https://learn.microsoft.com/en-us/visualstudio/)
- [Git Documentation](https://git-scm.com/doc)

## Author

- **Or Assayag** - _Initial work_ - [orassayag](https://github.com/orassayag)
- Or Assayag <orassayag@gmail.com>
- GitHub: https://github.com/orassayag
- StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
- LinkedIn: https://linkedin.com/in/orassayag
