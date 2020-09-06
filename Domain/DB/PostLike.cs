using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LikeButton.Domain.DB
{
    [Table("postlike")]
    public class PostLike : BaseEntity
    {
        public int PostId { get; set; }
        public string UserAgent { get; set; }
        public string IPAddress { get; set; }
        public bool UserLike { get; set; }
        public virtual Post Post { get; set; }
    }
}
