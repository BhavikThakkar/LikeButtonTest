using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LikeButton.Domain.DB;
using System.Collections.Generic;
using System;

namespace LikeButton.Data.EntityBuilder
{
    class PostBuilder
    {
        public PostBuilder(EntityTypeBuilder<Post> entity)
        {
            entity.ToTable("post");

            var seedData = new List<Post>();

            var post1 = new Post { Id = 1, Title = "EarningType", Abstract = "EarningType", DatePublished = DateTime.Now };
            var post2 = new Post { Id = 2, Title = "AddressType", Abstract = "AddressType", DatePublished = DateTime.Now };
            var post3 = new Post { Id = 3, Title = "ContactType", Abstract = "ContactType", DatePublished = DateTime.Now };

            seedData.AddRange(new List<Post> { post1, post2, post3 });

            entity.HasData(seedData);
        }
    }
}
