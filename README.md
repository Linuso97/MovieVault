MovieVault Web Application

MovieVault is a web application designed for movie enthusiasts who want to discover, save, and manage a collection of their favorite movies. This app allows users to search for movies, view detailed information fetched from an external API, and curate a personal collection by saving movies to a database.

Project Overview

MovieVault showcases my ability to create a full-stack web application using ASP.NET Core MVC, integrating an external API with local database operations. It demonstrates proficiency in building a user-friendly web interface while ensuring smooth data handling, with key features focused on CRUD functionality and API consumption.

Features

Search Movies: Users can search for movies using keywords, accessing detailed information such as the title, genre, release date, and a brief synopsis. This data is fetched dynamically from the OMDb API.
Save Movies to Collection: Users can add movies to a personal collection stored in a SQL Server database, making it easy to keep track of movies they want to watch.
View Collection: Users can view all saved movies and get a detailed view of each movie in their collection.
Delete Movies from Collection: Users can remove movies from their collection if they’ve watched them or want to clean up their list.
Responsive Design: The app is designed to be accessible and responsive on various devices.
Technology Stack
Backend: ASP.NET Core Web API (C#) for API integration, data processing, and database management.
Frontend: HTML, JavaScript, and Bootstrap for a responsive and user-friendly interface. ASP.NET Core MVC is used for routing and connecting the frontend with backend services.
Database: SQL Server to persist movie data locally.
External API: OMDb API for fetching movie information.

API Integration: I began by integrating the OMDb API into the app, allowing real-time access to extensive movie data. I created methods to fetch and parse this data in a way that can be easily displayed or saved.

Database Setup: Using SQL Server, I implemented a relational database to store movies in a way that is easy to manage and retrieve. Entity Framework Core was used for database operations, handling migrations and schema generation.

CRUD Implementation: Core CRUD operations (Create, Read, Update, Delete) were set up to manage the local collection. This included creating endpoints for each operation in the API and connecting them to the MVC views.

User Interface Design: I implemented the front-end using ASP.NET Core MVC, focusing on a simple and intuitive user interface. Razor views were used for dynamic content rendering, providing users with an interactive experience.

How to Run the Application
To run this project, clone the repository and follow these steps:

Install Dependencies: Ensure that you have .NET 8.0 SDK installed and a SQL Server instance.
Database Migration: Run the following command to apply database migrations

dotnet ef database update

Start the Server: Use Visual Studio or the CLI to run the application
Access the Application: Navigate to http://localhost:**** (specified port) in your browser to use MovieVault.
