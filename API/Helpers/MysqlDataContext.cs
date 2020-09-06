using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LikeButton.Data;

namespace LikeButton.Helpers
{
    public class MysqlDataContext : DataContext
    {
        public MysqlDataContext(IConfiguration configuration) : base(configuration)
        {
        }

        private static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(MyLoggerFactory);
            // connect to mysql database
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
