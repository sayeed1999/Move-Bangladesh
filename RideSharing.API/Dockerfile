# Use the official .NET 7 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 7 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RideSharing.API/RideSharing.API.csproj", "RideSharing.API/"]
RUN dotnet restore "RideSharing.API/RideSharing.API.csproj"
COPY . .
WORKDIR "/src/RideSharing.API"
RUN dotnet build "RideSharing.API.csproj" -c Release -o /app/build

# Create the publish stage
FROM build AS publish
RUN dotnet publish "RideSharing.API.csproj" -c Release -o /app/publish

# Create the final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RideSharing.API.dll"]
