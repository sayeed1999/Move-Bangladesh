## Run/Stop Docker containers:-

- Run "docker compose up -d" to run all containers at once.
- Run "docker compose down" to tear down all containers.

## Set environment variables in development (from Visual Studio):-

- Open Developer PowerShell & run the following commands:
- setx API__ConnectionStrings__DatabaseConnectionString "<connection_string_here>"
- setx API__ClientApplication__AllowedOrigins "http://localhost:3000" (if frontend is running on this domain)
- setx API__Keycloak__Host "http://localhost:9990"
- setx API__Keycloak__Realm "RideSharing"

## Keycloak usage:-

- Access Keycloak administrator console through http://localhost:9990 or https://localhost:9991.
- Login to default admin account. (default user: admin, pass: admin)
- Navigate to 'RideSharing' realm (tenant)

The apis are accessible through localhost:5000, 5001, 5002, 5003. https port is not working..

Access the db through these url:-

- localhost:6002 -> In SQL Server, use (localhost, 6002) in server name

Access the apis through these urls:-

- localhost:5000 -> API Gateway
- localhost:5002 -> RideSharing.API
- localhost:5003 -> RideSharing.CustomerAPI


Also db urls are not accessing.