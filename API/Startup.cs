using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using LikeButton.Data;
using LikeButton.Installers;
using LikeButton.Services.Helpers;
using Microsoft.AspNetCore.Http;
using LikeButton.API.Hubs;

namespace LikeButton
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
                option.JsonSerializerOptions.MaxDepth = 256000;
            });
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            var assemblies = Assembly
                .GetEntryAssembly()
                ?.GetReferencedAssemblies()
                .Select(Assembly.Load)
                .ToList();

            assemblies?.Add(typeof(Startup).Assembly);
            services.AddAutoMapper(assemblies);

            services.InstallServicesInAssembly(_configuration);

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new ConsumesAttribute("application/json"));
                foreach (var outputFormatter in options.OutputFormatters.OfType<OutputFormatter>()
                    .Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }

                foreach (var inputFormatter in options.InputFormatters.OfType<InputFormatter>()
                    .Where(x => x.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            //  dataContext.Database.EnsureDeleted();
            // migrate any database changes on startup (includes initial db creation)
            //dataContext.Database.EnsureCreated();
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(options =>
            {
                options.Path = "/redoc";
                options.DocumentPath = "/swagger/swagger/swagger.json";
            });
            app.UseReDoc(options =>
            {
                options.Path = "/openapi_redoc";
                options.DocumentPath = "/swagger/openapi/swagger.json";
            });

            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); endpoints.MapHub<PostHub>("/PostHub");
            });

            app.UseMvc();

            //app.UseMvc(routeBuilder =>
            //{
            //    routeBuilder.EnableDependencyInjection();
            //    routeBuilder.Expand().Select().Filter().Count().OrderBy();
            //});
            WebHelpers.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

        }
    }
}
