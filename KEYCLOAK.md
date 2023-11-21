# Keycloak Deployment Guide

Follow this guide to learn how to run Keycloak containers, access Keycloak administrator console, and destroy the containers.

## Table of Contents

- [Set Environment Variables](#set-environment-variables)
- [Running the Containers](#running-the-containers)
- [Accessing Keycloak Administrator Console](#accessing-keycloak-administrator-console)
- [Accessing Local Mail Server](#accessing-local-mail-server)
- [Destroying the Containers](#destroying-the-containers)

## Set Environment Variables

Check `.env.example` file and create a similar file named `.env`.
Here is an example file: -
```
# env for keycloak database
KC_POSTGRES_USER=admin
KC_POSTGRES_PASS=admin

# env for keycloak
KC_ADMIN=admin
KC_ADMIN_PASSWORD=admin
KC_DB_USERNAME=admin
KC_DB_PASSWORD=admin

# env for smtp server (not needed for development environment)
KC_SMTP_SERVER_EMAIL=
KC_SMTP_SERVER_HOST=
KC_SMTP_SERVER_PORT=
KC_SMTP_SERVER_USER=
KC_SMTP_SERVER_PASS=
```

## Running the Containers

Follow the following steps to deploy Keycloak in your local machine: -

- Ensure Docker is running on your system.
- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-keycloak.dev.yml up -d
  ```

## Accessing Keycloak Administrator Console

- Open up your favorite web browser.
- Navigate to [http://localhost:9990](http://localhost:9990).
- Login using default admin username=`$KC_ADMIN` and password=`$KC_ADMIN_PASSWORD` taking the values from .env file.

### Accessing Local Mail Server

**Note: This section is only for development environment.**

- Open up your favorite web browser.
- Navigate to [http://localhost:8025](http://localhost:8025).

## Destroying the Containers

Follow the following steps to destroy the containers: -

- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-keycloak.dev.yml down
  ```
