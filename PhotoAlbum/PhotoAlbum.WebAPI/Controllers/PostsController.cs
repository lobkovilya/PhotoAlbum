using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using AutoMapper;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.WebAPI.Utils;

namespace PhotoAlbum.WebAPI.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        private const int PageSize = 10;

        [HttpGet]
        public IEnumerable<PostDisplayDto> GetPosts(int page, string order)
        {
            return _postService.GetPosts(page, order, PageSize);
        }


        [Route("~/api/user/{username}/posts/{page}/{order}")]
        public IEnumerable<PostDisplayDto> GetPostsForUser(string username, int page, string order)
        {
            return _postService.GetPosts(username, page, order, PageSize);
        }




        [HttpPost]
        [Authorize]
        public HttpResponseMessage CreatePost(PostCreateDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var name = (User.Identity as ClaimsIdentity)?.GetClaim("name");
            if (name != postDto.Login)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden,
                    $"You have no permission, to post photo with username: {postDto.Login}");
            }

            _postService.Create(postDto);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpPost]
        [Route("Rate")]
        [Authorize]
        public PostRatingDto Rate(PostRateDto postDto)
        {
            var name = (User.Identity as ClaimsIdentity)?.GetClaim("name");

            if (name != postDto.Login)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("You are not allowed to rate post with this login"),
                    ReasonPhrase = "Permission denied"
                };
                throw new HttpResponseException(response);
            }

            _postService.Rate(postDto);
            var postDisplayDto = _postService.Get(postDto.PostId);
            return Mapper.Map<PostRatingDto>(postDisplayDto);
        }

        [HttpGet]
        [Route("{postId}/Mark")]
        [Authorize]
        public PostRatingDto GetMarkForUser(int postId)
        {
            var name = (User.Identity as ClaimsIdentity)?.GetClaim("name");

            if (name == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Can't get username from authorization token"),
                    ReasonPhrase = "Unauthorized"
                };
                throw new HttpResponseException(response);
            }

            var mark = _postService.GetMark(name, postId);

            return new PostRatingDto { Rating = mark };
        }


        [HttpPost]
        [Route("{postId}/Remove")]
        [Authorize]
        public IHttpActionResult Remove(int postId)
        {
            var name = (User.Identity as ClaimsIdentity)?.GetClaim("name");
            var post = _postService.Get(postId);

            if (name == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Can't get username from authorization token"),
                    ReasonPhrase = "Wrong type of authorization"
                };
                throw new HttpResponseException(response);
            }

            if (name != post.Login)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("You have no permission to delete this post"),
                    ReasonPhrase = "Permission denied"
                };
                throw new HttpResponseException(response);
            }
            
            _postService.Remove(postId);
            return Ok();
        }

        [HttpGet]
        [Route("{postId}")]
        [Authorize]
        public PostEditDto Post(int postId)
        {
            var post = _postService.Get(postId);
            if (post == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"There is no post with id: {postId}"),
                    ReasonPhrase = "Post not found"
                };
                throw new HttpResponseException(response);
            }
            return Mapper.Map<PostEditDto>(post);
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult Edit(PostEditDto postDto)
        {
            var name = (User.Identity as ClaimsIdentity).GetClaim("name");
            var post = _postService.Get(postDto.Id);

            if (post.Login != name)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("You have no permission to edit this profile"),
                    ReasonPhrase = "Permission denied"
                };
                throw new HttpResponseException(response);
            }

            _postService.Update(postDto);
            return Ok();
        }
    }
}
