using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AdemideInfoWebsite.Server.Configuration;

public static class SwaggerConfiguration
{
    public static void ConfigureJwtBearer(this SwaggerGenOptions options)
    {
        try
        {
            // Load the OpenApi assembly dynamically
            var openApiAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "Microsoft.OpenApi");

            if (openApiAssembly == null)
            {
                Console.WriteLine("[SWAGGER] Warning: Microsoft.OpenApi assembly not found. JWT configuration skipped.");
                return;
            }

            // Get the types we need
            var openApiSecuritySchemeType = openApiAssembly.GetType("Microsoft.OpenApi.Models.OpenApiSecurityScheme");
            var securitySchemeTypeEnum = openApiAssembly.GetType("Microsoft.OpenApi.Models.SecuritySchemeType");
            var parameterLocationEnum = openApiAssembly.GetType("Microsoft.OpenApi.Models.ParameterLocation");
            var openApiSecurityRequirementType = openApiAssembly.GetType("Microsoft.OpenApi.Models.OpenApiSecurityRequirement");
            var openApiReferenceType = openApiAssembly.GetType("Microsoft.OpenApi.Models.OpenApiReference");
            var referenceTypeEnum = openApiAssembly.GetType("Microsoft.OpenApi.Models.ReferenceType");

            if (openApiSecuritySchemeType == null || securitySchemeTypeEnum == null || parameterLocationEnum == null)
            {
                Console.WriteLine("[SWAGGER] Warning: Required OpenApi types not found. JWT configuration skipped.");
                return;
            }

            // Create the security scheme instance
            var securityScheme = Activator.CreateInstance(openApiSecuritySchemeType);

            // Set properties using reflection
            openApiSecuritySchemeType.GetProperty("Name")?.SetValue(securityScheme, "Authorization");
            openApiSecuritySchemeType.GetProperty("Type")?.SetValue(securityScheme, Enum.Parse(securitySchemeTypeEnum, "Http"));
            openApiSecuritySchemeType.GetProperty("Scheme")?.SetValue(securityScheme, "bearer");
            openApiSecuritySchemeType.GetProperty("BearerFormat")?.SetValue(securityScheme, "JWT");
            openApiSecuritySchemeType.GetProperty("In")?.SetValue(securityScheme, Enum.Parse(parameterLocationEnum, "Header"));
            openApiSecuritySchemeType.GetProperty("Description")?.SetValue(securityScheme, 
                "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.\n\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'");

            // Add security definition
            var addSecurityDefMethod = typeof(SwaggerGenOptions).GetMethod("AddSecurityDefinition");
            addSecurityDefMethod?.Invoke(options, new object[] { "Bearer", securityScheme });

            // Create OpenApiReference for the security requirement
            var reference = Activator.CreateInstance(openApiReferenceType);
            openApiReferenceType.GetProperty("Type")?.SetValue(reference, Enum.Parse(referenceTypeEnum, "SecurityScheme"));
            openApiReferenceType.GetProperty("Id")?.SetValue(reference, "Bearer");

            // Create the security scheme for the requirement
            var securitySchemeForReq = Activator.CreateInstance(openApiSecuritySchemeType);
            openApiSecuritySchemeType.GetProperty("Reference")?.SetValue(securitySchemeForReq, reference);

            // Create security requirement
            var securityRequirement = Activator.CreateInstance(openApiSecurityRequirementType);
            var addMethod = openApiSecurityRequirementType.GetMethod("Add", new[] { openApiSecuritySchemeType, typeof(IList<string>) });
            addMethod?.Invoke(securityRequirement, new object[] { securitySchemeForReq, Array.Empty<string>() });

            // Add security requirement
            var addSecurityReqMethod = typeof(SwaggerGenOptions).GetMethod("AddSecurityRequirement");
            addSecurityReqMethod?.Invoke(options, new object[] { securityRequirement });

            Console.WriteLine("[SWAGGER] JWT Bearer authentication configured successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SWAGGER] Error configuring JWT Bearer: {ex.Message}");
            Console.WriteLine("[SWAGGER] Swagger will work but without JWT authentication UI.");
        }
    }
}
