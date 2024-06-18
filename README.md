# LocationNinja: Your Go-To Geolocation API
<img src="https://github.com/emaadgh/LocationNinja/assets/10380342/25581344-41c6-400a-a601-65926d23c0ce" width="150" height="150">

## Overview
LocationNinja is a robust service built using vertical slice architecture designed to handle various location-based queries. It supports converting IP addresses to physical locations and can be easily integrated into your web applications. The system uses data from third-party providers and caches results in MongoDB for optimized performance.

## Features
- **IP to Location**: Converts IP addresses to physical locations using third-party data.
- **Caching**: Utilize MongoDB for improved performance.
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

## Getting Started

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/emaadgh/LocationNinja.git
    ```

2. **Install Dependencies**:
    - Ensure you have **.NET SDK 8** installed.
    - Open a terminal and navigate to the project folder:
        ```bash
        cd LocationNinja
        ```
    - Restore dependencies:
        ```bash
        dotnet restore
        ```
3. **Pull and Run MongoDB Docker Image**:
    - Ensure you have Docker installed and running.
    - Pull the MongoDB Docker image:
        ```bash
        docker pull mongo
        ```
    - Run the MongoDB container:
        ```bash
        docker run -d -p 27017:27017 --name locationninja-mongo mongo
        ```


4. **Run the Application**:
    - Start the API by running the following command in your terminal:
        ```bash
        dotnet run
        ```

5. **Explore the API**:
    - Once the API is running, you can use tools like **Swagger** to interact with the endpoints.
    - Visit the Swagger UI at `https://localhost:<PORT>/swagger/index.html` to explore the API documentation interactively.

## AppSettings.json Configuration

Please note that `appsettings.json` files are not included in the Git repository. These files typically contain sensitive information such as database connection strings, API keys, and other configuration settings specific to the environment.

For local testing, you can create your own `appsettings.json` file in the root directory of the LocationNinja project and add the necessary configurations.

### Local Configuration

Here's an example of how you can configure your `appsettings.json` file:

```json
{
  "MongoDatabase": {
    "Host": "mongodb://localhost:27017",
    "DatabaseName": "locationNinja"
  },
  "Features": {
    "IpLocation": {
      "IpApiBaseUrl": "http://ip-api.com/json/"
    }
  },
  // Other configurations...
}
```
If you deploy the LocationNinja to Azure App Service, you can add the connection strings in the Connection Strings section of the Configuration settings in the Azure portal. Azure App Service securely encrypts connection strings at rest and transmits them over an encrypted channel, ensuring the security of your sensitive information.

## Creator

- [Emaad Ghorbani](https://github.com/emaadgh)

## Contributing

Contributions are welcome! If you'd like to enhance LocationNinja, you can submit pull requests or open issues.

## License

This project is licensed under the MIT License.
