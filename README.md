# LocationNinja: Your Go-To Geolocation API
<img src="https://github.com/emaadgh/LocationNinja/assets/10380342/25581344-41c6-400a-a601-65926d23c0ce" width="150" height="150">

## Overview
LocationNinja is a robust service built using vertical slice architecture designed to handle various location-based queries. It supports converting IP addresses to physical locations and can be easily integrated into your web applications. The system uses data from third-party providers and caches results in MongoDB for optimized performance.

## Features
- **IP to Location**: Converts IP addresses to physical locations using third-party data.
- **Caching**: Utilize MongoDB for improved performance.
- **API Endpoints**: Exposes RESTful API endpoints for interaction.
- **Dockerizing**: Containerize the LocationNinja using DockerFile and Docker Compose for simplified deployment and management.

## Tools and Technologies
- **.NET 8.0**: The latest version of the .NET framework.
- **ASP.NET Core**: For building the web API.
- **AutoMapper**: For object-object mapping.
- **FluentValidation**: For model validation.
- **MongoDB**: For caching previously queried data.
- **Entity Framework Core**: For database interaction.
- **Swashbuckle**: For generating Swagger API documentation.
- **Docker**: For containerizing the application.
- **Xunit**: For unit testing.
- **Moq**: For mocking dependencies in tests.
- **FluentAssertions**: For more readable and maintainable assertions in tests.

## Architecture Overview
### Components
- **Request For Location**: Initiates requests for various location-based queries.
- **Features**: Core functionalities that process different types of location requests.
- **Related Service**: Handles specific tasks related to each feature.
- **Provider**: Third-party services that provide data for location.
- **Cache**: MongoDB is used to store previously queried data to improve performance.

<img src="https://github.com/emaadgh/LocationNinja/assets/10380342/9498b5ed-f026-4bce-9794-aac9138821cc" width="984" height="471">

## Creator

- [Emaad Ghorbani](https://github.com/emaadgh)

## Contributing

Contributions are welcome! If you'd like to enhance LocationNinja, you can submit pull requests or open issues.

## License

This project is licensed under the MIT License.
