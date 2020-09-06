using System;
using System.Collections.Generic;
using LikeButton.Domain.DB;

namespace LikeButton.Domain.Model.Entities.PostEntity
{
    public class PostListResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PostLikes { get; set; }
    }
}
