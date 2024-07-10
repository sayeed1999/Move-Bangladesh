# Use the official Golang image as the base image for the 'build' stage,
# which includes Go compilers and tools.
FROM golang:latest AS builder

# Set the Current Working Directory inside the container to `/app`.
WORKDIR /app

# Layer 1: Creates a layer with only go.mod and go.sum file
# to take advantage of Docker's caching mechanism.
COPY go.mod go.sum ./

# Layer 2: Download Go dependencies & caches them.
# This layer will be reused if go.mod and go.sum haven’t changed,
# even if the source code changes later.
RUN go mod download

# Layer 3: Copies the entire source code, including go.mod and go.sum.
# Does not invalidate the cache for the `RUN go mod download` step unless 
# go.mod or go.sum are changed. So later code changes & rebuilds of the image are quicker!
COPY . .

# Layer 4: Builds the Go app from dir `./cmd/app` and name the output binary file as `main`.
RUN go build -o main ./cmd/app

# Start a new stage `final stage` from scratch with the minimal Alpine Linux image.
FROM alpine:latest  

# Set the Current Working Directory inside the container to `/root/`.
WORKDIR /root/

# Copy the Pre-built binary file from the previous stage (/app/main) to the current dir
# which is /root/.
COPY --from=builder /app/main .

# Exposes the port to access.
EXPOSE 8080

# Command to run the executable when the container starts.
CMD ["./main"]
