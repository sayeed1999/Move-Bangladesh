# Redis Deployment Guide

Follow this guide to learn how to run Redis containers, access Redis management console, and destroy the containers.

## Table of Contents

- [Running the Containers](#running-the-containers)
- [Accessing Redis Management Console](#accessing-redis-management-console)
- [Destroying the Containers](#destroying-the-containers)

## Running the Containers

Follow the following steps to deploy Redis in your local machine: -

- Ensure Docker is running on your system.
- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-redis.yml up -d
  ```

## Accessing Redis Management Console

- Open up your favorite web browser.
- Navigate to [http://localhost:8081](http://localhost:8081).
- Login using default username=`admin` and password=`password`.

## Destroying the Containers

Follow the following steps to destroy the containers: -

- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-redis.yml down
  ```
