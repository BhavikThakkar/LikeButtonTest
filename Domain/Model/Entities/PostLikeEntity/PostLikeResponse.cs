using LikeButton.Domain.DB;
using LikeButton.Domain.Model.Generic.Response;

namespace LikeButton.Domain.Model.Entities.PostLikeEntity
{

    public class PostLikeResponse : BaseResponse<PostLike>
    {
        public PostLikeResponse(PostLike PostLike) : base(PostLike) { }

        public PostLikeResponse(string message) : base(message) { }
    }
}
