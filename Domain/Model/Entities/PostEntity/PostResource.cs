using System;
using System.Collections.Generic;
using LikeButton.Domain.DB;

namespace LikeButton.Domain.Model.Entities.PostEntity
{
    public class PostResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public System.DateTime DatePublished { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }
}
