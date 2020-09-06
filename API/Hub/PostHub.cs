using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LikeButton.Domain.IServices;
using AutoMapper;
using LikeButton.Domain.Model.Entities.PostLikeEntity;

namespace LikeButton.API.Hubs
{
    public class PostHub : Hub
    {
        private readonly IPostService _PostService;
        private readonly IPostLikeService _PostLikeService;

        private readonly IMapper _mapper;

        public PostHub(IPostService PostService, IPostLikeService PostLikeService, IMapper mapper)
        {
            _PostService = PostService;
            _PostLikeService = PostLikeService;
            _mapper = mapper;
        }
        public Task Like(string postId)
        {
            var likePost = SaveLike(postId);
            return Clients.All.SendCoreAsync("updateLikeCount", new object[] { likePost });
        }
        private PostLikeCountResource SaveLike(string id)
        {
            int postId = Convert.ToInt32(id);
            
            var x =  _PostLikeService.SaveAsync(postId).Result;

            var postLikes = _PostLikeService.GetLikeCount(postId);

            return new PostLikeCountResource
            {
                PostId = postId,
                LikeCount = postLikes
            };
        }
    }
}
