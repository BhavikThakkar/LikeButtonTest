using System.Linq;
using System.Threading.Tasks;
using LikeButton.Domain.DB;
using LikeButton.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LikeButton.Data.Repositories
{
    public class PostLikeRepository : BaseRepository<PostLike>, IPostLikeRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<PostLike> _dbSet;


        public PostLikeRepository(DataContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<PostLike>();
        }

        public async Task<bool> Exists(int postId, string ipAddress)
        {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.PostId == postId && e.IPAddress == ipAddress);
            return result != null;
        }

        public async Task<int> CountByPostId(int id)
        {
            return await _dbSet.Where(e => e.PostId == id).CountAsync(x => x.UserLike);
        }

    }
}
