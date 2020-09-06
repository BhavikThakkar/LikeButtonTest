using System.Collections.Generic;

namespace LikeButton.Domain.Model.Generic.Query
{
    public class QueryResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; } = 0;
    }
}
