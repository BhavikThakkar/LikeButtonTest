using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace LikeButton.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerDocument(api =>
                {
                    api.DocumentName = "swagger";
                    api.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
                    api.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer",
                        new OpenApiSecurityScheme
                        {
                            Type = OpenApiSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                            In = OpenApiSecurityApiKeyLocation.Header,
                            Description = "JWT Authorization header using the bearer scheme"
                        }));
                    api.PostProcess = document =>
                    {
                        document.Info.Title = "LikeButton API";
                        document.Info.Version = "v1";
                    };
                })
                .AddOpenApiDocument(api =>
                {
                    api.DocumentName = "openapi";
                    api.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
                    api.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer",
                        new OpenApiSecurityScheme
                        {
                            Type = OpenApiSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                            In = OpenApiSecurityApiKeyLocation.Header,
                            Description = "JWT Authorization header using the bearer scheme"
                        }));
                    api.PostProcess = document =>
                    {
                        document.Info.Title = "LikeButton API";
                        document.Info.Version = "v1";
                    };
                });
        }
    }
}
