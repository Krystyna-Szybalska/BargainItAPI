# BargainItAPI


## Project Description
This API is my solution to recruitment task for intern .Net developer position.

### Architecture
The solution architecture consists of multiple projects, each with a specific role in implementing the CQRS pattern.

**1. BargainIt.API**
   
   The API project receives incoming requests, performs authorization checks, validates input, and delegates commands or queries to the appropriate handlers in the Application project.
   
**2. BargainIt.Application**
   
   The Application project contains the core business logic of the system. It implements the command and query handlers, which are responsible for handling write operations (commands) and retrieving data (queries) from the underlying persistence layer. The Application project orchestrates the interactions between the API and the Persistence project.
   
**3. BargainIt.Persistance**

  The Persistence project handles data persistence and access to the PostgreSQL database. It includes repositories, data models, and database-specific configuration. 

**4. BargainIt.UnitTests**

  The UnitTests project utilizes the NUnit testing framework with Fluent Assertions and BDD style syntax of Given, When, Then, tests are arranged according to Arrange-Act-Assert pattern.

**5. BargainIt.IntegrationTests**

  The IntegrationTests project contains IntegrationTests for controllers.

**BargainIt.Shared** and **BargainIt.Tests.Shared** contain shared utilites for other projects.

## Usage
Ensure that Docker is running on your system. 
Build and run the Docker container. Execute the following command in the terminal:

``` docker-compose up -d ```

And start the project:

```dotnet run ```


## Technologies
* C#
 
* ASP.NET
  
* Entity Framework Core
  
* NUnit
  
* MediatR
  
* CQRS
  
* Swagger
  
* Docker

## License
This project is licensed under the MIT License.


