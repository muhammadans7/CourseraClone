# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the .csproj file(s) and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application files and build the project
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Serve the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80 and run the application
EXPOSE 80
ENTRYPOINT ["dotnet", "courseraClone.dll"]
