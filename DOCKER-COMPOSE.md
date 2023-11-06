# Docker Compose - Keycloak with Nginx & Postgres

This readme is intended to explain how to setup and run the project using docker containers.

## Prerequisites

Before you begin, ensure you have the following installed & running properly on your server:

- Docker Desktop
- WSL (Windows Subsystem for Linux) if the server OS is Windows

## Setting Up the Project

### Generate SSL Certificate

To obtain a valid SSL certificate for your server, open bash terminal and run the following command:

```
openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout server.key.pem -out server.crt.pem
```

This will generate two files on the home directory of the server:
- SSL certificate file (`server.key.pem`)
- SSL certificate key file (`server.crt.pem`)

### Set Environment Variables for Docker Compose

The `docker-compose.yml` file requires proper environment variables set before execution.

Following the `.env.example` file, create another file `.env` in the same directory, and set proper values. DO NOT expose them!

## Set Environment Variables for API development

Open Developer PowerShell & run the following commands:

```
setx API__ConnectionStrings__DatabaseConnectionString "<connection_string_here>"
setx API__ClientApplication__AllowedOrigins "http://localhost:3000" (if frontend is running on this domain)
setx API__Keycloak__Host "https://localhost:9991"
setx API__Keycloak__Realm "RideSharing"
```

## Running the Project:-

Open terminal from project root directory. Run the following command to run all containers in detached mode:

```
docker compose build --no-cache
docker compose up -d
```

## Usage

- Access Keycloak Administrator Console through `http://localhost:9990`(only for dev env) and `https://localhost:9991`.
- Login using default account. (user: admin, pass: admin)
- Navigate to 'RideSharing' realm (tenant)

## Destroying the Project:-

Run the following command to destroy all containers:

```
docker compose down
```


