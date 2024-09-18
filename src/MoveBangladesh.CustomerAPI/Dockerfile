#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MoveBangladesh.CustomerAPI/MoveBangladesh.CustomerAPI.csproj", "src/MoveBangladesh.CustomerAPI/"]
COPY ["src/MoveBangladesh.Common/MoveBangladesh.Common.csproj", "src/MoveBangladesh.Common/"]
COPY ["src/MoveBangladesh.Domain/MoveBangladesh.Domain.csproj", "src/MoveBangladesh.Domain/"]
COPY ["src/MoveBangladesh.Infrastructure/MoveBangladesh.Infrastructure.csproj", "src/MoveBangladesh.Infrastructure/"]
COPY ["src/MoveBangladesh.Application/MoveBangladesh.Application.csproj", "src/MoveBangladesh.Application/"]
RUN dotnet restore "./src/MoveBangladesh.CustomerAPI/MoveBangladesh.CustomerAPI.csproj"
COPY . .
WORKDIR "/src/src/MoveBangladesh.CustomerAPI"
RUN dotnet build "./MoveBangladesh.CustomerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MoveBangladesh.CustomerAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoveBangladesh.CustomerAPI.dll"]
