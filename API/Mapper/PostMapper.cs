using System.Linq;
using AutoMapper;
using LikeButton.Domain.DB;
using LikeButton.Domain.Model.Entities.PostEntity;
using LikeButton.Domain.Model.Generic.Query;
using LikeButton.Domain.Model.Generic.Resources;

namespace LikeButton.Mapper
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<Post, PostResource>();
            CreateMap<Post, PostListResource>().ForMember(x => x.PostLikes, y => y.MapFrom(z => z.PostLikes.Count(e => e.UserLike)));
            CreateMap<QueryResult<Post>, QueryResultResource<PostResource>>();
            CreateMap<QueryResult<Post>, QueryResultResource<PostListResource>>();

            CreateMap<PostQueryResource, PostQuery>();
            CreateMap<PostResource, Post>();
            CreateMap<SavePostResource, Post>();
        }
    }
}
