using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PhotoAlbum.Client.Filters;
using PhotoAlbum.Client.Models;
using PhotoAlbum.Client.Utils;
using PhotoAlbum.BLL.Contracts.DTO;
using WebGrease.Css.Extensions;

namespace PhotoAlbum.Client.Controllers
{
    [ServerResponseExceptionFilter]
    [AuthorizationExceptionFilter]
    public class FeedController : Controller
    {
        private readonly IPhotoAlbumProxy _server;

        public FeedController(IPhotoAlbumProxy server)
        {
            _server = server;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Posts(int? page, string userFilter, string order = "new")
        {
            var pageNumber = page ?? 0;
            var posts = (await _server.PostsAsync(userFilter, pageNumber, order)).ToList();

            if (User.Identity.IsAuthenticated)
            {
                await LoadMarks(posts);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Posts", posts);
            }

            if (userFilter == null)
            {
                return View(new FeedModel { Posts = posts });
            }

            var userInfo = await _server.UserInfoAsync(userFilter);
            var userName = $"{userInfo.LastName} {userInfo.FirstName}";

            var model = new FeedModel
            {
                Posts = posts,
                UserFilter = userFilter,
                UserName = userName
            };

            return View(model);
        }

        
        [NotModifiedExceptionFilter]
        public async Task<ActionResult> Photo(int id)
        {
            var photo = await _server.GetPhotoAsync(id);
            return File(photo.Content, photo.Type);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> RatePost(PostRateModel model)
        {
            var rateDto = Mapper.Map<PostRateDto>(model);
            rateDto.Login = User.Identity.Name;
            var ratingModel = await _server.RatePostAsync(rateDto);
            return Json(new { Rating = ratingModel.Rating, MarksAmount = ratingModel.MarksAmount });
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetMark(int postId)
        {
            var ratingModel = await _server.GetMark(postId);
            return Json(new { Rating = ratingModel.Rating }, JsonRequestBehavior.AllowGet);
        }


        private async Task LoadMarks(IEnumerable<PostDisplayModel> posts)
        {
            foreach (var post in posts)
            {
                var dto = await _server.GetMark(post.Id);
                post.UserMark = dto.Rating;
            }
        }
    }
}