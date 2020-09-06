using System;
using System.Collections.Generic;
using LikeButton.Domain.DB;

namespace LikeButton.Domain.Model.Entities.PostLikeEntity
{
    public class PostLikeCountResource
    {
        public int PostId { get; set; }
        public int LikeCount { get; set; }
    }
}
