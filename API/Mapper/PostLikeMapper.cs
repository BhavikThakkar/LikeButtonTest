using AutoMapper;
using LikeButton.Domain.DB;
using LikeButton.Domain.Model.Entities.PostLikeEntity;
using LikeButton.Domain.Model.Generic.Query;
using LikeButton.Domain.Model.Generic.Resources;

namespace LikeButton.Mapper
{
    public class PostLikeMapper : Profile
    {
        public PostLikeMapper()
        {
            CreateMap<PostLike, PostLikeResource>();
            CreateMap<QueryResult<PostLike>, QueryResultResource<PostLikeResource>>();

            CreateMap<PostLikeQueryResource, PostLikeQuery>();
            CreateMap<PostLikeResource, PostLike>();
            CreateMap<SavePostLikeResource, PostLike>();
        }
    }
}
