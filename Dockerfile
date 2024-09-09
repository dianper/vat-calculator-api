#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 80

# Copy the necessary files for the build
COPY VATCalculator.sln ./
COPY Directory.Build.props ./
COPY Directory.Build.targets ./
COPY Packages.props ./

# Copy all project files for the services and tests
COPY src/Application.Services/Application.Services.csproj ./src/Application.Services/
COPY src/Presentation.API/Presentation.API.csproj ./src/Presentation.API/
COPY src/CrossCutting.Configuration/CrossCutting.Configuration.csproj ./src/CrossCutting.Configuration/
COPY tests/Application.Services.UnitTests/Application.Services.UnitTests.csproj ./tests/Application.Services.UnitTests/
COPY tests/Presentation.API.IntegrationTests/Presentation.API.IntegrationTests.csproj ./tests/Presentation.API.IntegrationTests/
COPY tests/Presentation.API.UnitTests/Presentation.API.UnitTests.csproj ./tests/Presentation.API.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy all files
COPY ./src ./src
COPY ./tests ./tests

# Build the project
RUN dotnet build --no-restore -c Release

# Publish the project
RUN dotnet publish src/Presentation.API/Presentation.API.csproj -c Release -o /app --no-restore

# Use the official ASP.NET Core runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory to /app
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app .

# Run the application
ENTRYPOINT ["dotnet", "Presentation.API.dll"]