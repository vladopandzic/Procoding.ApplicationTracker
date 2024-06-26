#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Procoding.ApplicationTracker.Api/Procoding.ApplicationTracker.Api.csproj", "src/Procoding.ApplicationTracker.Api/"]
COPY ["src/Procoding.ApplicationTracker.Application/Procoding.ApplicationTracker.Application.csproj", "src/Procoding.ApplicationTracker.Application/"]
COPY ["src/Procoding.ApplicationTracker.Domain/Procoding.ApplicationTracker.Domain.csproj", "src/Procoding.ApplicationTracker.Domain/"]
COPY ["src/Procoding.ApplicationTracker.DTOs/Procoding.ApplicationTracker.DTOs.csproj", "src/Procoding.ApplicationTracker.DTOs/"]
COPY ["src/Procoding.ApplicationTracker.Infrastructure/Procoding.ApplicationTracker.Infrastructure.csproj", "src/Procoding.ApplicationTracker.Infrastructure/"]
COPY ["src/Procoding.ApplicationTracker.ServiceDefaults/Procoding.ApplicationTracker.ServiceDefaults.csproj", "src/Procoding.ApplicationTracker.ServiceDefaults/"]
RUN dotnet restore "./src/Procoding.ApplicationTracker.Api/Procoding.ApplicationTracker.Api.csproj"
COPY . .
WORKDIR "/src/src/Procoding.ApplicationTracker.Api"
RUN dotnet build "./Procoding.ApplicationTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Procoding.ApplicationTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Procoding.ApplicationTracker.Api.dll"]