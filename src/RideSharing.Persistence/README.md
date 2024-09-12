# Persistence Layer

This layer focuses on how your data is stored, including the database context, repositories and migrations.

## Generate Migration Files Guide

-   Navigate to src/RideSharing.Persistence

-   Ensure `dotnet ef` tool is installed globally

-   Run the following command to generate new migration file:

```
dotnet ef migrations add InitialMigration --startup-project ../RideSharing.CustomerAPI
```

-   Run the following command to apply migration to database:

```
dotnet ef database update
```
