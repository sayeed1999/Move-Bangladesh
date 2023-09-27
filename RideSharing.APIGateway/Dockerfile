# Use the official .NET 7 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 7 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RideSharing.APIGateway/RideSharing.APIGateway.csproj", "RideSharing.APIGateway/"]
RUN dotnet restore "RideSharing.APIGateway/RideSharing.APIGateway.csproj"
COPY . .
WORKDIR "/src/RideSharing.APIGateway"
RUN dotnet build "RideSharing.APIGateway.csproj" -c Release -o /app/build

# Create the publish stage
FROM build AS publish
RUN dotnet publish "RideSharing.APIGateway.csproj" -c Release -o /app/publish

# Create the final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RideSharing.APIGateway.dll"]
