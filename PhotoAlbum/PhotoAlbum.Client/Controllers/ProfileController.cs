using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;
using PhotoAlbum.Client.Filters;
using PhotoAlbum.Client.Models;
using PhotoAlbum.Client.Utils;
using PhotoAlbum.BLL.Contracts.Infrastructure;
using StringResources;

namespace PhotoAlbum.Client.Controllers
{
    [ServerResponseExceptionFilter]
    [AuthorizationExceptionFilter]
    public class ProfileController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProfileController));
        private readonly IPhotoAlbumProxy _server;

        public ProfileController(IPhotoAlbumProxy server)
        {
            _server = server;
        }

        [Authorize]
        public ActionResult CreatePost()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [ValidationExceptionFilter]
        public async Task<ActionResult> CreatePost(HttpPostedFileBase photo, PostCreateModel model)
        {
            if (photo == null || !ModelState.IsValid)
            {
                return Json(new
                {
                    Status = "failure",
                    FormErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
                });
            }

            await _server.CreatePostAsync(photo, model);
            return Json(new
            {
                Status = "success"
            });

        }

        [Authorize]
        public async Task<ActionResult> Manage(string username, int? page)
        {
            if (username != User.Identity.Name)
            {
                return View("ErrorPage", "Permission denied" as object);
            }

            var pageNumber = page ?? 0;
            var posts = await _server.PostsAsync(username, pageNumber, "new");
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ManagePosts", posts);
            }
            return View(posts);

        }

        [Authorize]
        public async Task<ActionResult> EditProfile(string username)
        {
            if (username != User.Identity.Name)
            {
                return View("ErrorPage", "Permission denied" as object);
            }

            var user = await _server.UserAsync(username);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidationExceptionFilter]
        public async Task<ActionResult> EditProfile(UserEditModel model)
        {
            try
            {
                await _server.EditUserAsync(model);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { SuccessMessage = Resources.EditSuccess });
            }
            catch (ValidationException e)
            {
                Log.Warn(e.Message, e);
                ModelState.AddModelErrors(e.ModelErrors);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    FormErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(p => p.ErrorMessage) })
                });
            }
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(UserChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            try
            {
                await _server.ChangePasswordAsync(model);
                return PartialView("Success", Resources.PasswordChangeSuccess);
            }
            catch (ValidationException e)
            {
                Log.Warn(e.Message, e);
                ModelState.AddModelErrors(e.ModelErrors);
            }
            return PartialView(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> RemovePost(int postId)
        {
            await _server.RemovePostAsync(postId);
            return Json(new { Success = true });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> EditPost(int postId)
        {
            var post = await _server.PostEditAsync(postId);
            return PartialView(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditPost(PostEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Status = "failure",
                    FormErrors = ModelState.Select(kvp => new { key = kvp.Key, errors = kvp.Value.Errors.Select(e => e.ErrorMessage) })
                });
            }

            await _server.EditPostAsync(model);
            return Json(new
            {
                Status = "success",
                PostId = model.Id,
                Caption = model.Caption,
                Description = model.Description
            });

        }
    }
}