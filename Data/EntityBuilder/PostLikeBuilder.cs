using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LikeButton.Domain.DB;

namespace LikeButton.Data.EntityBuilder
{
    class PostLikeBuilder
    {
        public PostLikeBuilder(EntityTypeBuilder<PostLike> entity)
        {
            entity.ToTable("PostLike");
        }
    }
}
