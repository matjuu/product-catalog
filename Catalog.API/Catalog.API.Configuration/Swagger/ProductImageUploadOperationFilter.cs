using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.API.Configuration.Swagger
{
    public class ProductImageUploadOperationFilter : IOperationFilter
    {
        private const string UPDATE_IMAGE_OPERATION = "updateimage";

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == UPDATE_IMAGE_OPERATION)
            {
                operation.Parameters = operation.Parameters.Where(x => x.Name=="id").ToList();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "image",
                    In = "formData",
                    Description = "Product image file",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}
