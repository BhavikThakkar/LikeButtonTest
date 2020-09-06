using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LikeButton.Data;

namespace LikeButton.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext, MysqlDataContext>();
        }
    }
}
