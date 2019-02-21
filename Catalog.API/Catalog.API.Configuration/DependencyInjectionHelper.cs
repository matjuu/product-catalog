using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Catalog.API.ApplicationServices;
using Catalog.API.ApplicationServices.CommandHandlers;
using Catalog.API.ApplicationServices.Excel;
using Catalog.API.ApplicationServices.QueryHandlers;
using Catalog.API.Contracts.Commands;
using Catalog.API.Contracts.Queries;
using Catalog.API.Contracts.Views;
using Catalog.API.Data;
using Catalog.API.Infrastructure;
using Catalog.API.Infrastructure.Repositories;
using Catalog.API.Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Configuration
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class DependencyInjectionHelper
    {
        public static void ConfigureDIForCommandHandlers(this IServiceCollection services)
        {
            services.AddScoped<IHandler<CreateProduct, Product>, WhenCreateProduct>();
            services.AddScoped<IHandler<UpdateProduct, Product>, WhenUpdateProduct>();
            services.AddScoped<IHandler<UpdateProductImage, Product>, WhenUpdateProductImage>();
            services.AddScoped<IHandler<ApproveProductPrice, Product>, WhenApproveProductPrice>();
            services.AddScoped<IHandler<DeleteProduct>, WhenDeleteProduct>();

            services.AddScoped<IHandler<CreateCatalogExport, CatalogExport>, WhenCreateCatalogExport>();
            services.AddScoped<IHandler<DeleteCatalogExport>, WhenDeleteCatalogExport>();
        }

        public static void ConfigureDIForApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICatalogExportFileProducer, CatalogExportFileProducer>();
        }

        public static void ConfigureDIForQueryHandlers(this IServiceCollection services, IConfiguration Configuration = null)
        {
            services.AddTransient<IHandler<ProductsByFilter, IEnumerable<Product>>, QueryProductsByFilter>();
            services.AddTransient<IHandler<ProductById, Product>, QueryProductById>();

            services.AddTransient<IHandler<CatalogExportsByFilter, IEnumerable<CatalogExport>>, QueryCatalogExportsByFilter>();
        }

        public static void ConfigureDIForInfrastructureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<CatalogDbContext>();
            services.AddScoped<IRepository<Domain.Aggregates.Product>, ProductsRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IRepository<Domain.Aggregates.CatalogExport>, CatalogExportsRepository>();


            services.AddScoped<ICatalogExportStorage, CatalogExportStorage>(provider =>
                new CatalogExportStorage(Path.Combine(AppContext.BaseDirectory, "files",
                    Configuration["CatalogExportStorage:Path"])));

            services.AddScoped<IProductImageStorage, ProductImageStorage>(provider =>
                new ProductImageStorage(Path.Combine(AppContext.BaseDirectory, "files",
                    Configuration["ProductImageStorage:Path"])));

        }
    }
}