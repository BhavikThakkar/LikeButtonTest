using System.Threading.Tasks;
using LikeButton.Domain.DB;
using LikeButton.Domain.Model.Entities.PostEntity;
using LikeButton.Domain.Model.Generic.Query;

namespace LikeButton.Domain.IServices
{
    public interface IPostService
    {
        Task<QueryResult<Post>> ListAsync(PostQuery query);
        Task<PostResponse> SaveAsync(Post Post);
        Task<PostResponse> UpdateAsync(int id, Post Post);
        Task<PostResponse> DeleteAsync(int id);

        Task<PostResponse> UpsertAsync(Post resource);
    }
}
