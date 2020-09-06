using System;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using LikeButton.Domain.DB;
using LikeButton.Domain.IRepositories;
using LikeButton.Domain.IServices;
using LikeButton.Domain.Model.Entities.PostLikeEntity;
using LikeButton.Domain.Model.Generic.Query;
using LikeButton.Services.Helpers;
using System.Linq;
using Omu.ValueInjecter.Injections;
using System.Linq.Expressions;

namespace LikeButton.Services.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _PostLikeRepository;
        private readonly IPostRepository _PostRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostLikeService(IPostRepository PostRepository, IPostLikeRepository PostLikeRepository, IUnitOfWork unitOfWork)
        {
            _PostLikeRepository = PostLikeRepository;
            _unitOfWork = unitOfWork;
            _PostRepository = PostRepository;
        }

        public async Task<PostLikeResponse> DeleteAsync(int id)
        {
            var existingPostLike = _PostLikeRepository.GetByID(id);

            if (existingPostLike == null)
                return new PostLikeResponse("PostLike not found.");

            try
            {
                _PostLikeRepository.Delete(existingPostLike);
                await _unitOfWork.CompleteAsync();

                return new PostLikeResponse(existingPostLike);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PostLikeResponse($"An error occurred when updating the PostLike: {ex.Message}");
            }
        }

        public Expression<Func<PostLike, bool>> GetExpressionToFilter(PostLikeQuery query)
        {
            Expression<Func<PostLike, bool>> expression = null;

            if (query.PostId.HasValue)
                expression = x => x.PostId == query.PostId;

            if (!string.IsNullOrEmpty(query.IpAddress))
                expression = ExpressionHelper.And<PostLike>(expression, x => x.IPAddress == query.IpAddress);

            return expression;
        }

        public Task<QueryResult<PostLike>> ListAsync(PostLikeQuery query)
        {
            Expression<Func<PostLike, bool>> filter = GetExpressionToFilter(query);
            return _PostLikeRepository.GetAsync(query, filter);
        }

        public int GetLikeCount(int postId)
        {
            return _PostLikeRepository.CountByPostId(postId).Result;
        }

        public async Task<PostLikeResponse> UpsertAsync(PostLike PostLike)
        {
            if (PostLike.Id == 0)
                return await SaveAsync(PostLike);
            else
                return await UpdateAsync(PostLike.Id, PostLike);
        }


        public async Task<PostLikeResponse> SaveAsync(int PostId)
        {
            var post = _PostRepository.GetByID(PostId);

            var liked = new PostLike
            {
                IPAddress = WebHelpers.GetRemoteIP,
                PostId = PostId,
                UserAgent = WebHelpers.GetUserAgent,
                UserLike = true
            };

            var postLikes = await ListAsync(new PostLikeQuery(PostId, liked.IPAddress, 1, 1));

            var dupe = postLikes.Items.FirstOrDefault();

            if (dupe == null)
            {
                return await SaveAsync(liked);
            }
            else
            {
                liked.UserLike = !dupe.UserLike;
                return await UpdateAsync(dupe.Id, liked);
            }
        }


        public async Task<PostLikeResponse> SaveAsync(PostLike PostLike)
        {
            _PostLikeRepository.Insert(PostLike);
            await _unitOfWork.CompleteAsync();

            return new PostLikeResponse(PostLike);
        }

        public async Task<PostLikeResponse> UpdateAsync(int id, PostLike PostLike)
        {
            var existingPostLike = _PostLikeRepository.GetByID(id);

            if (existingPostLike == null)
                return new PostLikeResponse("PostLike not found.");

            try
            {
                existingPostLike.InjectFrom(new LoopInjection(new[] { "Id" }), PostLike);

                _PostLikeRepository.Update(existingPostLike);
                await _unitOfWork.CompleteAsync();

                return new PostLikeResponse(existingPostLike);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PostLikeResponse($"An error occurred when updating the applicant: {ex.Message}");
            }
        }
    }
}
