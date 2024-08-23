# Docker Compose Deployment

## Table of Contents

- [Prerequisites](#prerequisites)
- [Other Docs](#other-docs)
- [Setting Up the Project](#setting-up-the-project)
  - [Set Environment Variables for Docker Compose](#set-environment-variables-for-docker-compose)
  - [Set Environment Variables for API development](#set-environment-variables-for-API-development)
- [Running the Containers](#running-the-containers)
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

### Set Environment Variables for Docker Compose

The `docker-compose.yml` file requires proper environment variables set before execution.

Following the `.env.example` file, create another file `.env` in the same directory, and set proper values. DO NOT expose them!

## Set Environment Variables for API development

<i>Skip this if you are not developing API or writing code and debug.</i>

Open Developer PowerShell from project root directory & run the following commands:

```
setx API__ConnectionStrings__DatabaseConnectionString "<connection_string_here>"
setx API__ClientApplication__AllowedOrigins "http://localhost:3000" (if frontend is running on this domain)
```

## Running the Containers

Create an external network to share between different compose files: -

```
docker network create ridesharing-shared-net
```

Open Terminal from project /docker directory and run the following command to run all containers in detached mode:

```
docker compose -f docker-compose.postgres.yml -f docker-compose.rabbitmq.yml -f docker-compose.redis.yml -f docker-compose.dev.yml -f docker-compose.nginx.yml up -d
```

## Usage

### Access APIs
- Access RideSharing Customer API through `http://localhost:5000/customer-api/`.
  `GET http://localhost:5000/customer-api` returns <i>`RideSharing.CustomerAPI is running`</i> message.
  
- Access RideSharing Authentication API through `http://localhost:5000/auth-api/`.
  `GET http://localhost:5000/auth-api/` returns <i>`RideSharing.AuthenticationAPI is running`</i> message.

### Access MailHog

- Access Local MailHog Server through `http://localhost:8025`(only for dev env).

## Destroying the Containers

Run the following command to destroy all containers:

```
docker compose
  -f docker-compose.postgres.yml
  -f docker-compose.rabbitmq.yml
  -f docker-compose.redis.yml
  -f docker-compose.dev.yml
  -f docker-compose.nginx.yml down
```

## References

To learn more about each of these topics, head to the following documentations:-

- [Keycloak documentation](https://www.keycloak.org/documentation)
- [Docker documentation](https://docs.docker.com/)
- [Nginx documentation](https://nginx.org/en/docs/)
