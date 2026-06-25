using System.Text;
using System.Text.Json;

namespace AdemideInfoWebsite.Server.Middleware;

public class SwaggerBearerMiddleware
{
    private readonly RequestDelegate _next;

    public SwaggerBearerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Only intercept Swagger JSON requests
        if (context.Request.Path.StartsWithSegments("/swagger") && 
            context.Request.Path.Value?.EndsWith("/swagger.json") == true)
        {
            // Capture the response
            var originalBody = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context);

            // Read the response
            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(memoryStream).ReadToEndAsync();

            // Modify the Swagger JSON to add Bearer authentication
            var modifiedJson = AddBearerSecurityToSwaggerJson(responseText);

            // Write the modified response
            context.Response.Body = originalBody;
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(modifiedJson);
            await context.Response.WriteAsync(modifiedJson);
        }
        else
        {
            await _next(context);
        }
    }

    private string AddBearerSecurityToSwaggerJson(string swaggerJson)
    {
        try
        {
            using var document = JsonDocument.Parse(swaggerJson);
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false });

            writer.WriteStartObject();

            foreach (var property in document.RootElement.EnumerateObject())
            {
                if (property.Name == "components")
                {
                    writer.WritePropertyName("components");
                    writer.WriteStartObject();

                    // Copy existing component properties
                    foreach (var componentProp in property.Value.EnumerateObject())
                    {
                        componentProp.WriteTo(writer);
                    }

                    // Add securitySchemes
                    writer.WritePropertyName("securitySchemes");
                    writer.WriteStartObject();
                    writer.WritePropertyName("Bearer");
                    writer.WriteStartObject();
                    writer.WriteString("type", "http");
                    writer.WriteString("scheme", "bearer");
                    writer.WriteString("bearerFormat", "JWT");
                    writer.WriteString("description", "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.");
                    writer.WriteEndObject();
                    writer.WriteEndObject();

                    writer.WriteEndObject();
                }
                else if (property.Name == "paths")
                {
                    // Write paths as-is
                    property.WriteTo(writer);

                    // Add global security requirement after paths
                    writer.WritePropertyName("security");
                    writer.WriteStartArray();
                    writer.WriteStartObject();
                    writer.WritePropertyName("Bearer");
                    writer.WriteStartArray();
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                    writer.WriteEndArray();
                }
                else
                {
                    property.WriteTo(writer);
                }
            }

            // If no components section existed, add it
            if (!document.RootElement.TryGetProperty("components", out _))
            {
                writer.WritePropertyName("components");
                writer.WriteStartObject();
                writer.WritePropertyName("securitySchemes");
                writer.WriteStartObject();
                writer.WritePropertyName("Bearer");
                writer.WriteStartObject();
                writer.WriteString("type", "http");
                writer.WriteString("scheme", "bearer");
                writer.WriteString("bearerFormat", "JWT");
                writer.WriteString("description", "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.");
                writer.WriteEndObject();
                writer.WriteEndObject();
                writer.WriteEndObject();
            }

            // Add security if not added with paths
            if (!document.RootElement.TryGetProperty("paths", out _))
            {
                writer.WritePropertyName("security");
                writer.WriteStartArray();
                writer.WriteStartObject();
                writer.WritePropertyName("Bearer");
                writer.WriteStartArray();
                writer.WriteEndArray();
                writer.WriteEndObject();
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }
        catch
        {
            // If modification fails, return original
            return swaggerJson;
        }
    }
}
