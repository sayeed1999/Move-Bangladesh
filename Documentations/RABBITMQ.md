# RabbitMQ Deployment Guide

Follow this guide to learn how to run RabbitMQ containers, access RabbitMQ management console, and destroy the containers.

## Table of Contents

- [Running the Containers](#running-the-containers)
- [Accessing RabbitMQ Management Console](#accessing-rabbitmq-management-console)
- [Destroying the Containers](#destroying-the-containers)

## Running the Containers

Follow the following steps to deploy rabbitmq in your local machine: -

- Ensure Docker is running on your system.
- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-rabbitmq.yml up -d
  ```

## Accessing RabbitMQ Management Console

- Open up your favorite web browser.
- Navigate to [http://localhost:15672](http://localhost:15672).
- Login using default username=`guest` and password=`guest`.

## Destroying the Containers

Follow the following steps to destroy the containers: -

- Open up a terminal from the project root directory.
- Run the following command:
  ```
  docker-compose -f docker-compose-rabbitmq.yml down
  ```
