using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LikeButton.Data.EntityBuilder;
using LikeButton.Domain.DB;

namespace LikeButton.Data
{
    public partial class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Post> Post { get; set; }
  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=127.0.0.1;database=LikeButton;user id=root;password=P5^bC>_Ui<SFG:ub%7)xVq2ut{%{odr4?|uf5bC!P#Gh7B*6{@G+9?NgOQ;treattinyasboolean=false", x => x.ServerVersion("8.0.20-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            new PostBuilder(modelBuilder.Entity<Post>());
           
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
