using System.Threading.Tasks;
using LikeButton.Domain.DB;

namespace LikeButton.Domain.IRepositories
{
    public interface IPostLikeRepository : IRepository<PostLike>
    {
        public Task<bool> Exists(int postId, string ipAddress);
        public Task<int> CountByPostId(int id);

    }
}
