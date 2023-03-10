# Identity Service

This service is responsible for authenticating users. It exposes a REST API that can be used to authenticate users and generate JWT tokens. The service also provides a Swagger UI that can be used to test the API.

Created as part of the [Alura Challenge](https://www.alura.com.br/challenges/back-end) 2021. :rocket:

## Getting Started

:warning: This project is a submodule of the [video-sharing-platform](https://github.com/brunoaragao/video-sharing-platform) project. If you want to run the entire project, please follow the instructions in the main repository.

If you want to run this service only, please follow the instructions below.

### Prerequisites

1. Install the .NET 7 SDK from [here](https://dotnet.microsoft.com/download/dotnet/7.0)
2. Install the dotnet-user-secrets tool using the command `dotnet tool install --global dotnet-user-secrets`

### Setup

1. Clone this repository
2. In the project's root folder, run the following command to set the connection string for the PostgreSQL database:
    ```
    dotnet user-secrets set "ConnectionStrings:IdentityConnection" "Host=localhost;Database=vsp-identity-service;Username=postgres;Password=postgres;"
    ```
    *Note: the connection string above is for a local PostgreSQL instance running on the default port with the default username and password. You can change it to match your local setup.*

## How to run
Run `dotnet watch run` in the project's root folder.

## How to use

The service exposes a REST API that can be used to authenticate users. The API is documented using Swagger and can be accessed at the `/swagger` endpoint.

## How to test
The service has a set of unit tests that can be run using the `dotnet test` command.

## Built With

- [Docker](https://www.docker.com/) - Containerization
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) - Web framework
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM
- [JWT](https://jwt.io/) - Authentication
- [Swagger](https://swagger.io/) - API documentation
- [xUnit](https://xunit.net/) - Unit testing framework

## Acknowledgments

- [Alura](https://www.alura.com.br/)

## Authors

- **[Bruno Arag??o](https://github.com/brunoaragao)**

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
