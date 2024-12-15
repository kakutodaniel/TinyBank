# TinyBank

## Description
This project is a Web API built with ASP<span>.NET</span> Core 6. It provides a set of endpoints for managing resources in a hypothetical bank, offering some operations.

## Technologies Used
- .NET 6.0
- ASP<span>.NET</span> Core 6.0
- Entity Framework Core 7.0 (in memory)
- Swagger 6.5

## Requirements
To run this project, you will need the following:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Postman or another tool to test the endpoints

## Installation and Configuration
1. Clone the repository:

```bash
git clone https://github.com/kakutodaniel/TinyBank.git
cd your-repository
```

2. Restore NuGet packages:
    Run the following command to restore the project dependencies:

```bash
dotnet restore
```

3. Run the project:
    To start the development server:

```bash
dotnet run
```

4. Import [TinyBank.postman_collection.json](TinyBank.postman_collection.json) and [TinyBank.postman_environment.json](TinyBank.postman_environment.json) to Postman


## Testing the Endpoints
1. After the application is running, check the **port** that was lauched and updated it in the Postman files.
2. From Postman run _**Create User 1**_ and then _**Create User 2**_
3. Run _**Get All Users**_. This will copy **userId** and **accountId** values to environment variables (from both users)
4. Now it's possible to run _**Make a deposit**_, _**Make a withdraw**_, _**Make a transfer**_, _**Get Account Transactions**_, _**Get Account Balance**_ and _**Deactivate User**_

## Improvements
In order to improve the application there are some implementations that should be done as follows:

- **Lock Strategy**: We must implement a distributed lock strategy to ensure that only one transaction occurs at a time and that the balance remains correct. Reference for .Net https://github.com/samcook/RedLock.net

- **Bearer Token**: Get information of the authenticated user from the token

- **API Version**: Add API versioning as a good practice, as the API will evolve

- **Add Tests**: Tests are missing and are important to cover the application

- **Round trips to database**: Reduce, since there are a few in the same request and we can improve performance

- **Validations**: Add validations in the outer layer

- **Mappers**: Create mapper files for better organization