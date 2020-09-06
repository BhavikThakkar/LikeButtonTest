using System.Threading.Tasks;
using LikeButton.Domain.DB;
using LikeButton.Domain.Model.Entities.PostLikeEntity;
using LikeButton.Domain.Model.Generic.Query;

namespace LikeButton.Domain.IServices
{
    public interface IPostLikeService
    {
        Task<QueryResult<PostLike>> ListAsync(PostLikeQuery query);
        Task<PostLikeResponse> SaveAsync(PostLike postLike);
        Task<PostLikeResponse> UpdateAsync(int id, PostLike postLike);
        Task<PostLikeResponse> DeleteAsync(int id);
        Task<PostLikeResponse> SaveAsync(int postId);
        Task<PostLikeResponse> UpsertAsync(PostLike resource);
        public int GetLikeCount(int postId);
    }
}
