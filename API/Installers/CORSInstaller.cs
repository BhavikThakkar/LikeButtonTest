using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LikeButton.Installers
{
    // ReSharper disable once InconsistentNaming
    public class CORSInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                        .SetIsOriginAllowed((host) => true)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }
    }
}
