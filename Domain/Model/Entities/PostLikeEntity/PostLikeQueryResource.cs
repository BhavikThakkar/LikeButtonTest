using LikeButton.Domain.Model.Generic.Resources;

namespace LikeButton.Domain.Model.Entities.PostLikeEntity
{
    public class PostLikeQueryResource : QueryResource
    {
        public int? PostId { get; set; }
    }

}
