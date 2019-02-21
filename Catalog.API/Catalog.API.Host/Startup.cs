using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using Catalog.API.ApplicationServices.Exceptions;
using Catalog.API.Configuration;
using Catalog.API.Configuration.Mapper;
using Catalog.API.Configuration.Swagger;
using Catalog.API.Data;
using Catalog.API.Host.Middlewares;
using Catalog.API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Catalog.API.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DomainExceptionMappingConfiguration.Configure();

            services.AddDbContext<CatalogDbContext>();
            services.ConfigureDIForCommandHandlers();
            services.ConfigureDIForQueryHandlers();
            services.ConfigureDIForApplicationServices();
            services.ConfigureDIForInfrastructureServices(Configuration);

            var mapperConfigurationAssembly = Assembly.GetAssembly(typeof(CatalogProfile));
            services.AddAutoMapper(mapperConfigurationAssembly);


            services
                .AddCors()
                .AddMvc(options => options.AllowEmptyInputInBodyModelBinding = true)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> {new StringEnumConverter()},
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Product Catalog API", Version = "v1" });

                c.DescribeAllEnumsAsStrings();

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Catalog.API.Host.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    "Catalog.API.Contracts.xml"));

                c.OperationFilter<ProductImageUploadOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var staticFileStorePath = Path.Combine(AppContext.BaseDirectory, "files");
            Directory.CreateDirectory(staticFileStorePath);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFileStorePath),
                RequestPath = "/files"
            });
           

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Catalog API");
            });

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseMiddleware<ErrorResponseMiddleware>();
            app.UseMvc();
        }
    }
}
