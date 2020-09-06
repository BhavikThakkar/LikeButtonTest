using LikeButton.Domain.Model.Generic.Query;

namespace LikeButton.Domain.Model.Entities.PostEntity
{
    public class PostQuery : Query
    {
        public int? Id { get; set; }

        public PostQuery(int? id,  int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            Id = id;
        }
    }

}
