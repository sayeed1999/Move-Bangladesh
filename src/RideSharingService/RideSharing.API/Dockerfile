#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RideSharingService/RideSharing.API/RideSharing.API.csproj", "src/RideSharingService/RideSharing.API/"]
COPY ["src/Common/RideSharing.Common/RideSharing.Common.csproj", "src/Common/RideSharing.Common/"]
COPY ["src/RideSharingService/RideSharing.Infrastructure/RideSharing.Infrastructure.csproj", "src/RideSharingService/RideSharing.Infrastructure/"]
COPY ["src/RideSharingService/RideSharing.Entity/RideSharing.Entity.csproj", "src/RideSharingService/RideSharing.Entity/"]
COPY ["src/RideSharingService/RideSharing.Service/RideSharing.Service.csproj", "src/RideSharingService/RideSharing.Service/"]
RUN dotnet restore "src/RideSharingService/RideSharing.API/RideSharing.API.csproj"
COPY . .
WORKDIR "/src/src/RideSharingService/RideSharing.API"
RUN dotnet build "RideSharing.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "RideSharing.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RideSharing.API.dll"]