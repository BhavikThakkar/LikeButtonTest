using System;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using LikeButton.Domain.DB;
using LikeButton.Domain.IRepositories;
using LikeButton.Domain.IServices;
using LikeButton.Domain.Model.Entities.PostEntity;
using LikeButton.Domain.Model.Generic.Query;
using Omu.ValueInjecter.Injections;
using System.Linq.Expressions;

namespace LikeButton.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _PostRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository PostRepository, IUnitOfWork unitOfWork)
        {
            _PostRepository = PostRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PostResponse> DeleteAsync(int id)
        {
            var existingPost = _PostRepository.GetByID(id);

            if (existingPost == null)
                return new PostResponse("Post not found.");

            try
            {
                _PostRepository.Delete(existingPost);
                await _unitOfWork.CompleteAsync();

                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PostResponse($"An error occurred when updating the Post: {ex.Message}");
            }
        }
        public Expression<Func<Post, bool>> GetExpressionToFilter(PostQuery query)
        {
            Expression<Func<Post, bool>> expression = null;

            if (query.Id.HasValue)
                expression = x => x.Id == query.Id;

            return expression;
        }
        public Task<QueryResult<Post>> ListAsync(PostQuery query)
        {
            Expression<Func<Post, bool>> filter = GetExpressionToFilter(query);
            return _PostRepository.GetAsync(query, filter, null, nameof(Post.PostLikes));
        }

        public async Task<PostResponse> UpsertAsync(Post Post)
        {
            if (Post.Id == 0)
                return await SaveAsync(Post);
            else
                return await UpdateAsync(Post.Id, Post);
        }

        public async Task<PostResponse> SaveAsync(Post Post)
        {
            _PostRepository.Insert(Post);
            await _unitOfWork.CompleteAsync();

            return new PostResponse(Post);
        }

        public async Task<PostResponse> UpdateAsync(int id, Post Post)
        {
            var existingPost = _PostRepository.GetByID (id);

            if (existingPost == null)
                return new PostResponse("Post not found.");

            try
            {
                existingPost.InjectFrom(new LoopInjection(new[] { "Id" }),Post);

                _PostRepository.Update(existingPost);
                await _unitOfWork.CompleteAsync();

                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PostResponse($"An error occurred when updating the applicant: {ex.Message}");
            }
        }
    }
}
