# Parking Fee Calculator

### Overview

- The solution is built upon .NET Core 3.1.
- Please check **"Car Park code excercise.docx"** in the same folder for details of the problem to be solved.
- The exit condition for **Night Rate** has been altered to **"Exit before 6am the following day"** in the solution.


### Included Projects

- **CarPark.Api** - Parking fee calculation API. Please change connection strings in appsettings.json and appsettings.Production.json when running in different environments.
- **CarPark** - Core library including required abstractions and services.
- **CarPark.Persistence.SqlServer** - Persistence layer with EF Core and SQL Server. **CarParkContextFactory** is provided for design time operations for EF migrations.
- **CarPark.Persistence.InMemory** - Persistence layer with in-memory collections, mainly for testing.
- **CarPark.UnitTests** - Unit tests.
- **CarPark.ApiTests** - API tests.


### Limitation and Missing Parts


Due to time restriction,

- There is no authentication and authorisation
- Logging works with console logger. 
- No caching is included.
- There is no consideration on timezone offset/difference, etc. All date times are assumed to be local.
- No Swagger is added to the API.
- No substrantial comments are added. However the code readibility has been maximised.
- No automated end-to-end tests have been implemented.


### Testing

- xUnit is being used.
- All tests can be automated.
- In-memory collections are being used for unit testing and API testing. Hence therer is no need to use mocking framework and verify the executions.
- If it is really needed, the in-memory repository can be replaced with a mock. 
- There is no need to unit test the controller.
