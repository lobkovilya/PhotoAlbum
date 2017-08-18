using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.WebAPI.Filters;
using PhotoAlbum.WebAPI.Utils;

namespace PhotoAlbum.WebAPI.Controllers
{
    [RoutePrefix("api/Account")]
    [ValidationExceptionFilter]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserCreateDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.CreateAsync(userDto);

            return Ok();
        }

        [HttpPut]
        [Route("Edit")]
        [Authorize]
        public async Task<IHttpActionResult> Edit(UserEditDto userDto)
        {
            var authName = (User.Identity as ClaimsIdentity).GetClaim("name");
            if (authName != userDto.Login)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent($"You have no permission to edit user with login: {userDto.Login}"),
                    ReasonPhrase = "Permission denied"
                };
                throw new HttpResponseException(response);
            }

            await _userService.UpdateUserAsync(userDto);
            return Ok();

        }

        [HttpPut]
        [Route("Password")]
        [Authorize]
        public async Task<IHttpActionResult> ChangePassword(UserChangePasswordDto userDto)
        {
            await _userService.ChangePasswordAsync(userDto);
            return Ok();
        }


        [HttpGet]
        [Authorize]
        [Route("{username}")]
        public async Task<UserEditDto> GetUser(string username)
        {
            var authName = (User.Identity as ClaimsIdentity).GetClaim("name");

            if (authName != username)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("You have no permission to this profile"),
                    ReasonPhrase = "Permission denied"
                };
                throw new HttpResponseException(response);
            }
            return await _userService.GetUserAsync(username);
        }

        [HttpGet]
        [Route("{username}/info")]
        public async Task<UserInfoDto> GetUserInfo(string username)
        {
            return await _userService.GetUserInfoAsync(username);
        }
    }
}
