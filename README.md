# Parking Fee Calculator

## The Problem

Build an API to accept car entry time and exit time as parameters, and return parking fee to the caller applications. 

##### Early Bird Rate
- Flat Rate
- $13.00
- Entry between 6:00 AM to 9:00 AM
- Exit between 3:30 PM to 11:30 PM

##### Night Rate
- Flat Rate
- $6.50
- Entry between 6:00 PM to midnight (weekdays)
- Exit before 6am the following day

##### Weekend Rate
- Flat Rate
- $10.00
- Entry anytime past midnight on Friday to Sunday
- Exit any time before midnight of Sunday

If a customer enters the carpark before midnight on Friday and if they qualify for Night rate on a Saturday morning, then the program should charge the night rate instead of weekend rate.  


For any other entry and exit times the program should refer the following table for calculating the total price. 

- $5.00 per hour for parking up to 3 hours
- $20.00 daily flat rate applies for parking over 3 hours



## Overview

- The solution is built upon .NET Core 3.1 and Entity Frame Core with SQL Server.
- The persistence layer can also be implemented using NoSQL databases such as MongoDB.


## Included Projects

- **CarPark.Api** - Parking fee calculation API. Please change connection strings in appsettings.json and appsettings.Production.json when running in different environments.
- **CarPark** - Core library including required abstractions and services.
- **CarPark.Persistence.SqlServer** - Persistence layer with EF Core and SQL Server. **CarParkContextFactory** is provided for design time operations for EF migrations.
- **CarPark.Persistence.InMemory** - Persistence layer with in-memory collections, mainly for testing.
- **CarPark.UnitTests** - Unit tests.
- **CarPark.ApiTests** - API tests.


## Limitation and Missing Parts


Due to time restriction,

- There is no authentication and authorisation. You can simply implement it using client credential flow with OAuth. 
- Logging works with console logger only. However it is very easy to add something like Serilog as an abstraction layer for logging with a broad range of logging media and services.
- No caching is included.
- There is no consideration on timezone offset/difference, etc. All date times are assumed to be local.
- No Swagger is added to the API.
- No substrantial comments are added. However the code readibility has been maximised.
- No automated end-to-end tests have been implemented.


## Testing

- xUnit is being used.
- All tests can be automated with your CI/CD pipeline.
- In-memory collections are being used for unit testing and API testing. Hence therer is no need to use mocking framework. If verification of executions is really needed, you can add mocking framework or use custom stub services with call intercept to enable verification.
- If it is really needed, the in-memory repository can be replaced with a mock repository. 
- There is no need to unit test the controller as the API tests are more realistic.
