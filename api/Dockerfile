# Use the official .NET SDK image to build the API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy .csproj and restore dependencies
COPY api.csproj ./
RUN dotnet restore

# Copy the remaining files and build the API in Release mode
COPY . ./
RUN dotnet publish -c Release -o out

# Use a runtime image to run the API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the correct port that the API will listen on
EXPOSE 8080

# Run the API
ENTRYPOINT ["dotnet", "api.dll"]
