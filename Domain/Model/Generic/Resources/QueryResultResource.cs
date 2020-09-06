using System.Collections.Generic;

namespace LikeButton.Domain.Model.Generic.Resources
{
    public class QueryResultResource<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; } = 0;
    }
}
