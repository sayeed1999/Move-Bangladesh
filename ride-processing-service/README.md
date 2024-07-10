Project Deployment Guide

To run the project directly from terminal: -
Open a terminal from this directory and run `go run ./cmd/app/.`

The api will be running on `localhost:8080`.

---

To manually build the Docker image, run from terminal: -
`docker build -t ride-processing-service .`

To manually run a container for this image, run for terminal: -
`docker run --rm -it -p 7000:8080 ride-processing-service`

The api will be running on `localhost:7000`.

---

To run through Docker Compose file, run from terminal: -
`docker-compose up -d`.
