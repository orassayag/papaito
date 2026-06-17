# Papaito

Papaito was a customer project built as a real-world ASP.NET Web Forms application. Although it never reached production, it provided hands-on full-stack experience using jQuery on the frontend and LINQ-to-SQL with SQL Server on the backend. It focused on building a structured web system, data access layers, and working with legacy .NET architecture and deployment workflows.

Built in May 2010, it also improved skills in client requirements, Git version control, and maintainable .NET development project.

## Features

- 🎨 Admin dashboard for managing artists, productions, and staff
- 🖼️ Gallery management with image uploads and organization
- 📞 Contact and about page management
- 🎭 Complex (event) management with room configurations
- 🎨 Design and home page customization
- 🎬 Production and publishing workflows
- 👥 Staff management
- 🏢 Studio information management
- 💾 Database integration using LINQ-to-SQL
- ⚡ Responsive frontend using jQuery
- 🔐 User authentication and authorization

### Core Capabilities

- **Admin Interface**: Comprehensive management console for all content
- **Gallery System**: Image gallery with multiple views and organization
- **Content Management**: Dynamic content for home, about, contact, and other pages
- **Event Management**: Complex event planning with room configurations
- **Production Management**: Production tracking and publishing workflows
- **Staff Management**: Staff information and profiles
- **Data Access**: LINQ-to-SQL ORM for database interactions
- **User Management**: Authentication and authorization system

### Technical Excellence

- **ASP.NET Web Forms**: Legacy .NET web framework
- **LINQ-to-SQL**: Object-relational mapping for database access
- **SQL Server**: Relational database backend
- **jQuery**: Client-side JavaScript library for interactivity
- **Layered Architecture**: Separation of concerns with Data Access Layer (DAL)
- **Git Version Control**: Source code management

### Developer Experience

- **Visual Studio Integration**: Full IDE support for .NET development
- **Web Forms Controls**: Reusable server controls
- **SQL Server Management Studio**: Database management tools
- **NuGet Package Management**: Dependency management (where applicable)

## Getting Started

### Prerequisites

- .NET Framework (compatible with the project)
- SQL Server (or SQL Server Express)
- SQL Server Management Studio (SSMS)
- Visual Studio (recommended)

### Installation

1. Clone the repository to your computer.
2. Open the solution file in Visual Studio.
3. Restore any NuGet packages if prompted.
4. Set up the database (see Configuration section).

## Configuration

### Database Setup

1. Open SQL Server Management Studio.
2. Look for the SQL script or database backup in the repository to recreate the database.
3. Run the scripts to create the tables.
4. Update the connection string in `web.config` to point to your local database instance:
   ```xml
   <connectionStrings>
       <add name="..." connectionString="..." />
   </connectionStrings>
   ```

## Usage

### Running the Application

1. Set the Web application project (Papaito) as the Startup Project in Visual Studio.
2. Press `F5` or click `Start Debugging` (IIS Express).
3. The application will launch in your default web browser.

### Admin Dashboard

- Navigate to the admin section to manage content
- Use the provided authentication to access admin features
- Manage artists, productions, staff, and other content through the admin interface

## Available Scripts

(This project uses Visual Studio for build and deployment. For modern project management, see package.json or solution configurations.)

## Architecture Principles

This project follows traditional ASP.NET Web Forms architecture principles:

1. **Separation of Concerns**: Separate Data Access Layer (DAL) from presentation layer
2. **Web Forms Model**: Use of server controls and view state
3. **Event-Driven Programming**: Server-side event handling model
4. **State Management**: Use of session, application, and view state
5. **Master Pages**: Template-based page design for consistent UI
6. **Data Binding**: Two-way data binding with server controls

## Architecture

This project follows a traditional layered architecture with ASP.NET Web Forms:

### Directory Structure

```
Papaito/
├── Dal/                      # Data Access Layer
│   ├── Dal/                 # LINQ-to-SQL Data Context
│   │   ├── Properties/      # Assembly properties
│   │   ├── bin/             # Compiled binaries
│   │   ├── obj/             # Object files
│   │   ├── Dal.csproj       # DAL project file
│   │   ├── PapaDal.cs       # Data access logic
│   │   ├── PapaitoDal.dbml  # LINQ-to-SQL model
│   │   └── app.config       # DAL configuration
│   ├── DataBaseReceiver/    # Database receiver/management
│   ├── Papaito.mdf          # SQL Server database file
│   └── Papaito_log.ldf      # Database log file
├── Papaito/                  # Main Web Application
│   ├── Admin/               # Admin section pages
│   ├── App_Code/            # Shared code (GlobalFunctions, Logger, etc.)
│   ├── Bin/                 # Application binaries
│   ├── Complex/             # Complex (event) management
│   ├── Gallerycss/          # Gallery CSS files
│   ├── Galleryjs/           # Gallery JavaScript files
│   ├── Home/                # Home page resources
│   ├── ImagesR/             # Image resources
│   ├── Logs/                # Log files
│   ├── MasterImages/        # Master page images
│   ├── OrImage/             # Additional images
│   ├── OrImages2/           # More images
│   ├── OrImages3/           # Even more images
│   ├── OrImages4/           # Still more images
│   ├── PreLoad/             # Preload resources
│   └── [various .aspx files] # Web forms pages
└── [solution files]         # Visual Studio solution files
```

### Design Patterns

- **Layered Architecture**: Presentation, Business Logic, Data Access layers
- **Data Access Object (DAO) Pattern**: Encapsulated database access in DAL
- **Master Page Pattern**: Template-based UI consistency
- **Event-Driven Pattern**: Server control event handling
- **Repository Pattern (implicit)**: LINQ-to-SQL as data abstraction

## Development

### Building the Project

1. Open the solution in Visual Studio
2. Right-click the solution in Solution Explorer
3. Select "Build Solution" (or press Ctrl+Shift+B)

### Code Quality

- Follow .NET coding conventions
- Use meaningful names for controls, classes, and methods
- Keep business logic separate from presentation layer
- Use the Logger class for logging

## Best Practices

### Development Practices

1. **Use Version Control**: Commit changes frequently with meaningful messages
2. **Separate Concerns**: Keep UI, business logic, and data access separate
3. **Use Master Pages**: Maintain consistent UI across pages
4. **Handle Errors Gracefully**: Use try-catch blocks and proper error logging
5. **Validate Input**: Always validate user input on both client and server sides

### Database Practices

1. **Use Parameterized Queries**: Prevent SQL injection (handled by LINQ-to-SQL)
2. **Backup Regularly**: Maintain database backups
3. **Index Properly**: Add indexes for frequently queried columns
4. **Normalize Data**: Follow database normalization principles

### Security Practices

1. **Authentication**: Always authenticate admin users
2. **Authorization**: Restrict access to sensitive pages
3. **Input Validation**: Sanitize all user input
4. **Secure Connection Strings**: Protect database credentials

## Support

For questions or issues:

- **GitHub Issues**: [Create an issue](https://github.com/orassayag/papaito/issues)
- **Email**: orassayag@gmail.com

## Built With

- [ASP.NET Web-Forms](https://www.asp.net/web-forms) - The web framework used.
- [SQL](https://azure.microsoft.com/en-us/services/sql-database/) - The database used.
- [LINQ-TO-SQL](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/) - The component of .NET framework.
- [jQuery](https://jquery.com/) - The web framework used.
- [GIT](https://git-scm.com/) - Source management.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags).

## Authors

- **Or Assayag** - _Initial work_ - [orassayag](https://github.com/orassayag)
- **Niv Rabin** - _Initial work_ - [nivmorabin](https://linkedin.com/in/nivmorabin)
- Or Assayag <orassayag@gmail.com>
- GitHub: https://github.com/orassayag
- StackOverFlow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
- LinkedIn: https://linkedin.com/in/orassayag

## License

This application has an MIT license - see the [LICENSE](LICENSE) file for details.

## Author

- **Or Assayag** - _Initial work_ - [orassayag](https://github.com/orassayag)
- Or Assayag <orassayag@gmail.com>
- GitHub: https://github.com/orassayag
- StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
- LinkedIn: https://linkedin.com/in/orassayag

## Acknowledgments

- Built for educational and research purposes
- Respects robots.txt and implements rate limiting
- Uses user-agent rotation to avoid detection
- Implements polite crawling practices
