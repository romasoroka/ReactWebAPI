# Use ASP.NET base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN apt-get update && apt-get install -y libkrb5-3 && rm -rf /var/lib/apt/lists/*

# Build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotificationAPI/NotificationAPI.csproj", "NotificationAPI/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "./NotificationAPI/NotificationAPI.csproj"
COPY . .
WORKDIR "/src/NotificationAPI"
RUN dotnet build "./NotificationAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotificationAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationAPI.dll"]
