# Docker Compose Deployment

## Table of Contents

- [Prerequisites](#prerequisites)
- [Other Docs](#other-docs)
- [Setting Up the Project](#setting-up-the-project)
  - [Generate SSL Certificate](#generate-ssl-certificate)
  - [Set Environment Variables for Docker Compose](#set-environment-variables-for-docker-compose)
  - [Set Environment Variables for API development](#set-environment-variables-for-API-development)
- [Running the Containers](#running-the-containers)
  - [Production Environment](#production-environment)
  - [Development Environment](#development-environment)
- [Usage](#usage)
  - [Access APIs](#access-apis)
  - [Access MailHog](#access-mailhog)
- [Destroying the Containers](#destroying-the-containers)
- [References](#references)

## Prerequisites

Before you begin, ensure you have the following installed & running properly on your server:

- Docker Desktop
- WSL (Windows Subsystem for Linux) if the server OS is Windows

## Other Docs

Head to other deployment readme files: -

- [RabbitMQ](Documentations/RABBITMQ.md)
- [Redis with Management GUI](Documentations/REDIS.md)
- [PostgreSQL with PgAdmin](Documentations/POSTGRESQL.md)
- [Keycloak with Nginx & Postgres](Documentations/KEYCLOAK.md)

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

Create an external network to share between different compose files: -

```
docker network create ridesharing-shared-net
```

Open Terminal from project /docker directory and run the following command to run all containers in detached mode:

```
docker compose
  -f docker-compose.postgres.yml
  -f docker-compose.rabbitmq.yml
  -f docker-compose.redis.yml
  -f docker-compose.dev.yml
  -f docker-compose.nginx.yml up -d
```

### Development Environment

Open Terminal from project /docker directory and run the following command to run all containers in detached mode:

```
docker compose -f docker-compose-keycloak.dev.yml -f docker-compose-postgres.yml -f docker-compose-rabbitmq.yml -f docker-compose-redis.yml up -d
```

## Usage

### Access APIs
- Access RideSharing API through `https:localhost:4000`.
  `GET https://localhost:4000` returns <i>`RideSharing.InternalAPI is running`</i> message.
  
- Access RideSharing Customer API through `https:localhost:5000`.
  `GET https://localhost:5000` returns <i>`RideSharing.CustomerAPI is running`</i> message.

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
