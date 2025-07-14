# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["React.ProjectService.gRPC/React.ProjectService.gRPC.csproj", "React.ProjectService.gRPC/"]
COPY ["React.ProjectService.Application/React.ProjectService.Application.csproj", "React.ProjectService.Application/"]
COPY ["React.ProjectService.Domain/React.ProjectService.Domain.csproj", "React.ProjectService.Domain/"]
COPY ["React.ProjectService.Infrastructure/React.ProjectService.Infrastructure.csproj", "React.ProjectService.Infrastructure/"]
RUN dotnet restore "./React.ProjectService.gRPC/React.ProjectService.gRPC.csproj"
COPY . .
WORKDIR "/src/React.ProjectService.gRPC"
RUN dotnet build "./React.ProjectService.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./React.ProjectService.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "React.ProjectService.gRPC.dll"]