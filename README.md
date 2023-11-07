# Docker Compose Deployment - Keycloak with Nginx & Postgres

## Table of Contents

- [Prerequisites](#prerequisites)
- [Setting Up the Project](#setting-up-the-project)
  - [Generate SSL Certificate](#generate-ssl-certificate)
  - [Set Environment Variables for Docker Compose](#set-environment-variables-for-docker-compose)
  - [Set Environment Variables for API development](#set-environment-variables-for-API-development)
- [Running the Containers](#running-the-containers)
  - [Production Environment](#production-environment)
  - [Development Environment](#development-environment)
- [Usage](#usage)
  - [Access Keycloak](#access-keycloak)
  - [Access MailHog](#access-mailhog)
- [Destroying the Containers](#destroying-the-containers)
- [References](#references)

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

<i>Skip this if you are not developing API or writing code and debug.</i>

Open Developer PowerShell from project root directory & run the following commands:

```
setx API__ConnectionStrings__DatabaseConnectionString "<connection_string_here>"
setx API__ClientApplication__AllowedOrigins "http://localhost:3000" (if frontend is running on this domain)
setx API__Keycloak__Host "http://localhost:9990"
setx API__Keycloak__Realm "RideSharing"
```

## Running the Containers

### Production Environment

Open Terminal from project root directory and run the following command to run all containers in detached mode:

```
docker compose up -d
```

### Development Environment

Open Terminal from project root directory and run the following command to run all containers in detached mode:

```
docker compose -f docker-compose.dev.yml up -d
```

## Usage

### Access Keycloak

- Access Keycloak Administrator Console through `http://localhost:9990`(only for dev env) and `https://localhost:9991`.
- Login using default account. (user: admin, pass: admin)
- Navigate to 'RideSharing' realm (tenant)

### Access MailHog

- Access Local MailHog Server through `http://localhost:8025`(only for dev env).

## Destroying the Containers

Run the following command to destroy all containers:

```
docker compose down
```

## References

To learn more about each of these topics, head to the following documentations:-

- [Keycloak documentation](https://www.keycloak.org/documentation)
- [Docker documentation](https://docs.docker.com/)
- [Nginx documentation](https://nginx.org/en/docs/)
