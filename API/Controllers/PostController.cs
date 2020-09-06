using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using LikeButton.Domain.DB;
using LikeButton.Domain.IServices;
using LikeButton.Domain.Model.Entities.PostEntity;
using LikeButton.Domain.Model.Generic.Query;
using LikeButton.Domain.Model.Generic.Resources;

namespace LikeButton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OpenApiTags("Post")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _PostService;
        private readonly IMapper _mapper;

        public PostController(IPostService PostService, IMapper mapper)
        {
            _PostService = PostService;
            _mapper = mapper;
        }


        /// <summary>
        /// Lists all existing Post.
        /// </summary>
        /// <returns>List of Post.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(QueryResultResource<PostResource>), 200)]
        public async Task<QueryResultResource<PostResource>> GetAsync([FromQuery] PostQueryResource query)
        {
            var PostQuery = _mapper.Map<PostQueryResource, PostQuery>(query);
            var queryResult = await _PostService.ListAsync(PostQuery);
            var resource = _mapper.Map<QueryResult<Post>, QueryResultResource<PostResource>>(queryResult);
            return resource;
        }

        /// <summary>
        /// Lists all existing Post.
        /// </summary>
        /// <returns>List of Post.</returns>
        [HttpGet("GetList")]
        [ProducesResponseType(typeof(QueryResultResource<PostListResource>), 200)]
        public async Task<QueryResultResource<PostListResource>> GetListAsync([FromQuery] PostQueryResource query)
        {
            var PostQuery = _mapper.Map<PostQueryResource, PostQuery>(query);
            var queryResult = await _PostService.ListAsync(PostQuery);
            var resource = _mapper.Map<QueryResult<Post>, QueryResultResource<PostListResource>>(queryResult);
            return resource;
        }

        /// <summary>
        /// Saves a new Post.
        /// </summary>
        /// <param name="resPost">Post data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PostResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SavePostResource resPost)
        {
            var Post = _mapper.Map<SavePostResource, Post>(resPost);

            var validator = new PostValidator();
            var results = validator.Validate(Post);
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _PostService.SaveAsync(Post);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var PostResource = _mapper.Map<Post, PostResource>(result.Resource);
            return Ok(PostResource);
        }

        /// <summary>
        /// Update a existing Post.
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <param name="resPost">Post data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(PostResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePostResource resPost)
        {
            var Post = _mapper.Map<SavePostResource, Post>(resPost);

            var validator = new PostValidator();
            var results = validator.Validate(Post);
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _PostService.UpdateAsync(id, Post);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var PostResource = _mapper.Map<Post, PostResource>(result.Resource);
            return Ok(PostResource);
        }

        /// <summary>
        /// Deletes a given Post according to an identifier.
        /// </summary>
        /// <param name="id">Post identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PostResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _PostService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var categoryResource = _mapper.Map<Post, PostResource>(result.Resource);
            return Ok(categoryResource);
        }
    }
}
