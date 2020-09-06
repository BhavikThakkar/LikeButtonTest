using System;
using System.Collections.Generic;
using LikeButton.Domain.DB;

namespace LikeButton.Domain.Model.Entities.PostLikeEntity
{
    public class PostLikeResource
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserAgent { get; set; }
        public string IPAddress { get; set; }
        public bool UserLike { get; set; }
    }
}
