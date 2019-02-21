# Product catalog
A basic implementation of an API for storing, retrieving and exporting products. Implementation took ~11 hours for reference.

## Prerequisites
In order to run this solution you need to have an instance of MSSQL server running.

A quick way to set one up using docker
```
docker run --name mssql -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Admin!01' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

NOTE: The password provided in the docker command is the password hardcoded in `CatalogDbContext.cs` file. If you're rolling with your own MSSQL be sure to update the connection string. Ideally it would be configured in `appsettings.config` fille.

## Running the solution
After you got your prerequisites covered you want to perform the following steps

1. Ensure you're in the root repository directory
2. Run migration `dotnet ef database update -s ./Catalog.API/Catalog.API.Host -p ./Catalog.API/Catalog.API.Data`
3. Build and start the API (on http://localhost:5000) using one of the following options
    - Run `dotnet run -p ./Catalog.API/Catalog.API.Host`
    - Open the solution in `./Catalog.API` using Visual Studio and run start the API from there
4. Change directory to `./product-catalog-ui`
5. Run `npm start`

After these steps http://localhost:3000 should open automatically in your browser loading up the webapp.

API Swagger documentation can be found at http://localhost:5000/swagger.

## Solution notes and shortcommings
### Notes
1. Separated heavier operations into separate endpoints for improved API usability
2. `Catagog` with a `List<Product>` was heavily considered as an aggregate root but opted against due to performance concerns when ensuring `Product.Code` uniqueness with huge amounts of products.

### Shortcomings
1. Lack of unit tests - only `Product` covered reasonably, while almost no application services are. Technology choices and general testing approach/pattern is visible in the project though.
2. Product export should use a persistent messaging mechanism to make the process asynchronous from the API standpoint. In this case requests can easily timeout (or at the very least provide bad UX) when exporting huge prouct catalogs
3. CQRS isn't implemented all that well, especially concerning the data layer.
4. Implementing the service using DDD resulted in an anemic aggregate of `CatalogExports`. Ideally you'd want this in a separate CRUD service since there is little to no domain specific behaviour to warrant use of DDD.
6. EF Core usage extremely not elegant - mostly due to complete lack of experience with the technology.
7. Ideally you'd want to store files in a separate storage (such as Amazon S3 storage or the likes) since now you cannot reliably scale the application beyond one node.
8. Using `Guid` as the primary and indexing key has pretty big performance implications and should be separated into two separate keys or optimized in some other way.
9. UI wasn't focused on and was pretty much hacked together to showcase API functionality. Therefore it lacks proper error handling and most UX features.

