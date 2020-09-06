using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LikeButton.Domain.DB
{
    [Table("post")]
    public class Post : BaseEntity
    {
        public Post()
        {
            this.PostLikes = new HashSet<PostLike>();
        }

        public string Title { get; set; }
        public string Abstract { get; set; }
        public System.DateTime DatePublished { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
    }
}
