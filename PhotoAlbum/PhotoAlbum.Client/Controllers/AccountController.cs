using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using log4net;
using PhotoAlbum.Client.Filters;
using PhotoAlbum.Client.Models;
using PhotoAlbum.Client.Utils;
using PhotoAlbum.BLL.Contracts.Infrastructure;

namespace PhotoAlbum.Client.Controllers
{
    [ServerResponseExceptionFilter]
    [AuthorizationExceptionFilter]
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountController));
        private readonly IPhotoAlbumProxy _server;

        public AccountController(IPhotoAlbumProxy server)
        {
            _server = server;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidationExceptionFilter]
        public async Task<ActionResult> Login(UserLoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            await _server.AuthorizeAsync(model);
            FormsAuthentication.SetAuthCookie(model.Login, false);
            RenewCurrentUser();

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Posts", "Feed");
            
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidationExceptionFilter]
        public async Task<ActionResult> Register(UserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            await _server.RegisterAsync(model);

            var loginModel = Mapper.Map<UserLoginModel>(model);
            await _server.AuthorizeAsync(loginModel);
            FormsAuthentication.SetAuthCookie(model.Login, false);
            RenewCurrentUser();

            return RedirectToAction("Posts", "Feed");

        }


        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Posts", "Feed");
        }

        private static void RenewCurrentUser()
        {
            HttpCookie authCookie =
                System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = null;
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket != null && !authTicket.Expired)
                {
                    FormsAuthenticationTicket newAuthTicket = authTicket;

                    if (FormsAuthentication.SlidingExpiration)
                    {
                        newAuthTicket = FormsAuthentication.RenewTicketIfOld(authTicket);
                    }

                    string userData = newAuthTicket.UserData;
                    string[] roles = userData.Split(',');

                    System.Web.HttpContext.Current.User =
                        new System.Security.Principal.GenericPrincipal(new FormsIdentity(newAuthTicket), roles);
                }
            }
        }

    }
}