using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LikeButton.Data.Repositories;
using LikeButton.Domain.IRepositories;
using LikeButton.Domain.IServices;
using LikeButton.Services;
using LikeButton.Services.Services;
using Microsoft.AspNetCore.Http;

namespace LikeButton.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // configure DI for application services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped<IPostLikeService, PostLikeService>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();

            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });


            services.AddAutoMapper(typeof(Startup));

        }
    }
}
