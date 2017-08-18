using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.WebAPI.Filters;

namespace PhotoAlbum.WebAPI.Controllers
{
    public class PhotosController : ApiController
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }


        [HttpGet]
        [EntityTagContentHash]
        public PhotoDto Get(int id)
        {
            var photoDto = _photoService.Get(id);

            if (photoDto == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("No photo with such id"),
                    ReasonPhrase = "Not found"
                };
                throw new HttpResponseException(response);
            }

            return photoDto;
        }
    }
}
