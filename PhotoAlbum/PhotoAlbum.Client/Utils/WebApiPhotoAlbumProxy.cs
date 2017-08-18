using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using PhotoAlbum.Client.Infrastructure;
using PhotoAlbum.Client.Models;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.BLL.Contracts.Infrastructure;

namespace PhotoAlbum.Client.Utils
{
    public class WebApiPhotoAlbumProxy : IPhotoAlbumProxy, IDisposable
    {
        private readonly HttpClient _client;

        public WebApiPhotoAlbumProxy(string baseAddress)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        public async Task AuthorizeAsync(UserLoginModel model)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", model.Login),
                new KeyValuePair<string, string>("password", model.Password)
            });
            var response = await _client.PostAsync("/token", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new ValidationException("Wrong login or password");
            }

            var responseStr = await response.Content.ReadAsStringAsync();
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseStr);

            var cookie = new HttpCookie("auth") { ["access_token"] = dict["access_token"] };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public async Task RegisterAsync(UserCreateModel model)
        {
            var userCreateDto = Mapper.Map<UserCreateDto>(model);
            var response = await _client.PostAsJsonAsync("/api/account/register", userCreateDto);

            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
        }

        public async Task<UserInfoDto> UserInfoAsync(string username)
        {
            var response = await _client.GetAsync($"api/account/{username}/info");
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
            return await response.Content.ReadAsAsync<UserInfoDto>();
        }

        public async Task<UserEditModel> UserAsync(string username)
        {
            AddAuthorizationToken();

            var response = await _client.GetAsync($"api/account/{username}");
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
            var userDto = await response.Content.ReadAsAsync<UserEditDto>();
            return Mapper.Map<UserEditModel>(userDto);
        }
        

        public async Task<IEnumerable<PostDisplayModel>> PostsAsync(string username, int pageNumber, string order)
        {
            var query = $"api/user/{username}/posts/{pageNumber}/{order}";

            if (username == null)
            {
                query = $"api/posts?page={pageNumber}&order={order}";
            }

            var response = await _client.GetAsync(query);
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }

            var postDtos = await response.Content.ReadAsAsync<List<PostDisplayDto>>();
            var modelList = new List<PostDisplayModel>();
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            foreach (var post in postDtos)
            {
                var postModel = Mapper.Map<PostDisplayModel>(post);
                postModel.PhotoUrl = urlHelper.Action("Photo", "Feed", new { id = post.PhotoId });
                modelList.Add(postModel);
            }
            return modelList;
        }

        
        public async Task<PostEditModel> PostEditAsync(int postId)
        {
            AddAuthorizationToken();
            var response = await _client.GetAsync($"api/posts/{postId}");
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }

            var postDto = await response.Content.ReadAsAsync<PostEditDto>();
            return Mapper.Map<PostEditModel>(postDto);
        }

        public async Task CreatePostAsync(HttpPostedFileBase photo, PostCreateModel model)
        {
            AddAuthorizationToken();

            var postDto = Mapper.Map<PostCreateDto>(model);
            using (var ms = new MemoryStream())
            {
                photo.InputStream.CopyTo(ms);
                postDto.Content = ms.ToArray();
            }
            postDto.Type = photo.ContentType;

            var response = await _client.PostAsJsonAsync("api/posts", postDto);

            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
        }

        public async Task EditUserAsync(UserEditModel model)
        {
            AddAuthorizationToken();

            var userDto = Mapper.Map<UserEditDto>(model);
            var response = await _client.PutAsJsonAsync("api/account/edit", userDto);
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
        }

        public async Task ChangePasswordAsync(UserChangePasswordModel model)
        {
            AddAuthorizationToken();

            var changePasswordDto = Mapper.Map<UserChangePasswordDto>(model);
            var response = await _client.PutAsJsonAsync("api/account/password", changePasswordDto);
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
        }

        public async Task<PostRatingDto> RatePostAsync(PostRateDto rateDto)
        {
            AddAuthorizationToken();
            var response = await _client.PostAsJsonAsync("api/posts/rate", rateDto);
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
            return await response.Content.ReadAsAsync<PostRatingDto>();
        }


        public async Task<PostRatingDto> GetMark(int postId)
        {
            AddAuthorizationToken();
            var response = await _client.GetAsync($"api/posts/{postId}/mark");
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
            return await response.Content.ReadAsAsync<PostRatingDto>();
        }


        public async Task RemovePostAsync(int postId)
        {
            AddAuthorizationToken();
            var response = await _client.PostAsync($"api/posts/{postId}/remove", null);
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
        }

        public async Task EditPostAsync(PostEditModel model)
        {
            AddAuthorizationToken();
            var postDto = Mapper.Map<PostEditDto>(model);
            var response = await _client.PutAsJsonAsync("api/posts", postDto);
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }
        }

        public async Task<PhotoDto> GetPhotoAsync(int id)
        {
            var header = HttpContext.Current.Request.Headers["If-None-Match"];
            if (header != null)
            { 
                _client.DefaultRequestHeaders.Add("If-None-Match", header);
            }
               
            var response = await _client.GetAsync($"api/photos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                await HandleResponseErrorAsync(response);
            }

            var etag = response.Headers.ETag.Tag;
            if (etag != null)
            {
                HttpContext.Current.Response.Headers.Add("ETag", etag);
            }
            return await response.Content.ReadAsAsync<PhotoDto>();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private void AddAuthorizationToken()
        {
            var auth = HttpContext.Current.Request.Cookies["auth"];
            var token = auth?["access_token"];

            if (token == null)
            {
                throw new AuthorizationException("Unauthorize");
            }

            if (_client.DefaultRequestHeaders.Authorization == null)
            {
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

        private async Task HandleResponseErrorAsync(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    var obj = new { Message = "", ModelState = new Dictionary<string, string[]>() };
                    var contentStr = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeAnonymousType(contentStr, obj);
                    throw new ValidationException(content.ModelState);

                case HttpStatusCode.Unauthorized:
                    throw new AuthorizationException("Unauthorize");

                case HttpStatusCode.NotModified:
                    throw new NotModifiedException();

                default:
                    throw new ServerResponseException(
                        response.ReasonPhrase,
                        await response.Content.ReadAsStringAsync(),
                        response.StatusCode
                    );
            }
        }
    }
}