# Instructions

## Setup Instructions

1. Clone the project to your computer.
2. Install the necessary prerequisites:
   - .NET Framework (compatible with the requirements)
   - SQL Server Management Studio (SSMS)
3. Open the solution in Visual Studio.
4. Restore NuGet packages if prompted.

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

1. Set the Web application project as the Startup Project in Visual Studio.
2. Press `F5` or click `Start Debugging` (IIS Express).
3. The application will launch in your default web browser.

## Project Structure

```text
Dal/             - Data Access Layer using LINQ-TO-SQL
Papaito/         - Main Web Application using ASP.NET Web-Forms
```

## Troubleshooting

### Build Errors
- Ensure you have the correct .NET Framework installed.
- Check if all assemblies and dependencies are correctly referenced.

### Database Connection Failed
- Verify SQL Server is running.
- Ensure the connection string inside `web.config` matches your local server name and authentication method.

## Author

- **Or Assayag** - *Initial work* - [orassayag](https://github.com/orassayag)
- Or Assayag <orassayag@gmail.com>
- GitHub: https://github.com/orassayag
- StackOverflow: https://stackoverflow.com/users/4442606/or-assayag?tab=profile
- LinkedIn: https://linkedin.com/in/orassayag
