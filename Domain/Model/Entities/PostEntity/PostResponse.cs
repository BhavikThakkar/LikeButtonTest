using LikeButton.Domain.DB;
using LikeButton.Domain.Model.Generic.Response;

namespace LikeButton.Domain.Model.Entities.PostEntity
{

    public class PostResponse : BaseResponse<Post>
    {
        public PostResponse(Post Post) : base(Post) { }

        public PostResponse(string message) : base(message) { }
    }
}
