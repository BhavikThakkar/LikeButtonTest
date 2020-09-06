using LikeButton.Domain.Model.Generic.Query;

namespace LikeButton.Domain.Model.Entities.PostLikeEntity
{
    public class PostLikeQuery : Query
    {
        public int? PostId { get; set; }
        public string? IpAddress{ get; set; }

        public PostLikeQuery(int? postId, string? ipAddress, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            PostId = postId;
            IpAddress = ipAddress;
        }
    }

}
