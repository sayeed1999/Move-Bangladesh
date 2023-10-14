Run "docker compose up -d" to run all containers at once.

Run "docker compose down" to tear down all containers.

The apis are accessible through localhost:5000, 5001, 5002, 5003. https port is not working..

Access the db through these url:-

- localhost:6002 -> In SQL Server, use (localhost, 6002) in server name

Access the apis through these urls:-

- localhost:5000 -> API Gateway
- localhost:5002 -> RideSharing.API
- localhost:5003 -> RideSharing.CustomerAPI


Also db urls are not accessing.