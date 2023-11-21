# PostgreSQL Deployment Guide

Follow this guide to learn how to run PostgreSQL containers, access PostgreSQL management console, and destroy the containers.

## Table of Contents

- [Set Environment Variables](#set-environment-variables)
- [Running the Containers](#running-the-containers)
- [Accessing PgAdmin Console](#accessing-pgadmin-console)
- [Destroying the Containers](#destroying-the-containers)

## Set Environment Variables

Check `.env.example` file and create a similar file named `.env`.
Here is an example file: -
```
POSTGRES_USER=user
POSTGRES_PASSWORD=password
PGADMIN_EMAIL=admin@pg.com
PGADMIN_PASSWORD=password
```

## Running the Containers

Follow the following steps to deploy PostgreSQL in your local machine: -

- Ensure Docker is running on your system.
- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-postgres.yml up -d
  ```

## Accessing PgAdmin Console

- Open up your favorite web browser.
- Navigate to [http://localhost:8082](http://localhost:8082).
- Login using email=`$PGADMIN_EMAIL` and password=`$PGADMIN_PASSWORD` from .env file.

## Destroying the Containers

Follow the following steps to destroy the containers: -

- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-postgres.yml down
  ```
