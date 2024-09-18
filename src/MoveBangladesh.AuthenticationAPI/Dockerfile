#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MoveBangladesh.AuthenticationAPI/MoveBangladesh.AuthenticationAPI.csproj", "src/MoveBangladesh.AuthenticationAPI/"]
COPY ["src/MoveBangladesh.Common/MoveBangladesh.Common.csproj", "src/MoveBangladesh.Common/"]
RUN dotnet restore "./src/MoveBangladesh.AuthenticationAPI/MoveBangladesh.AuthenticationAPI.csproj"
COPY . .
WORKDIR "/src/src/MoveBangladesh.AuthenticationAPI"
RUN dotnet build "./MoveBangladesh.AuthenticationAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MoveBangladesh.AuthenticationAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoveBangladesh.AuthenticationAPI.dll"]
