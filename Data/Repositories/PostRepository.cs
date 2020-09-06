using LikeButton.Domain.DB;
using LikeButton.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace LikeButton.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {

        public PostRepository(DataContext context) : base(context) {
        }


    }
}
