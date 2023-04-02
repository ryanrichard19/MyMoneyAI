# MyMoneyAI

MyMoneyAI is a personal finance assistant that uses AI-driven insights and recommendations to help users manage their finances effectively. This application is built using .NET 7, ASP.NET Core, Entity Framework Core, GPT-4, Project MAUI, and SolidJS.


## Features

- User authentication and authorization using ASP.NET Core Identity and JWT tokens
- Models for handling personal finance data, such as accounts, transactions, and budgets
- API endpoints for managing financial data and generating insights
- Integration with GPT-4 for AI-driven financial advice and recommendations
- Mobile application built with Project MAUI
- Web application built with SolidJS

## Project Structure

The MyMoneyAI project is organized into several components:

- MyMoneyAI.API: The backend API, built with .NET 7 and ASP.NET Core
- MyMoneyAI.Models: The domain models for the application
- MyMoneyAI.Repositories: Repositories for handling database operations using Entity Framework Core
- MyMoneyAI.Services: Services for implementing business logic and interacting with GPT-4
- MyMoneyAI.Controllers: API controllers for exposing application functionality via RESTful endpoints
- MyMoneyAI.Tests: Unit tests for various components of the application
- MyMoneyAI.Mobile: The mobile application built with Project MAUI
- MyMoneyAI.Web: The web application built with SolidJS

## Getting Started

1. Clone the repository
2. Set up the necessary API keys and other configuration values
3. Install the required NuGet packages and frontend dependencies
4. Run the backend API using Visual Studio or the .NET CLI
5. Develop and run the frontend applications using Project MAUI for mobile and SolidJS for web

## Contributing

Contributions are welcome! Please```


# Application Architecture

*Include a high-level diagram of the application architecture showing the different components and how they interact*

## Features

- User registration and authentication
- User management
- Transaction tracking
- Budgeting
- Category management
- Financial advice powered by GPT-4

## Components

### API

*Include a diagram illustrating the API components such as controllers, services, repositories, and the database*

### Mobile App

*Include a diagram illustrating the main screens and components of the Project MAUI mobile app*

### Web Interface

*Include a diagram illustrating the main components and views of the SolidJS web interface*

## Getting Started

*Provide instructions for setting up the project locally, including any required tools, dependencies, and configuration*

## Contributing

*Provide guidelines for contributing to the project, such as how to submit issues, create pull requests, and participate in discussions*

## License

*Include licensing information, if applicable*


dotnet ef migrations add InitialCreate --project MyMoneyAI.Infrastructure --startup-project MyMoneyAI.API

dotnet ef database update --project MyMoneyAI.Infrastructure --startup-project MyMoneyAI.API
