using System;

namespace LikeButton.Domain.Model.Entities.PostLikeEntity
{
    public class SavePostLikeResource
    {
        public int PostId { get; set; }
        public string UserAgent { get; set; }
        public string IPAddress { get; set; }
        public bool UserLike { get; set; }
    }
}
