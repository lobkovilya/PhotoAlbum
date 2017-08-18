using AutoMapper;
using PhotoAlbum.BLL.Contracts.DTO;

namespace PhotoAlbum.WebAPI.Mapping
{
    public class WebApiProfile : Profile
    {
        public WebApiProfile()
        {
            CreateMap<PostDisplayDto, PostEditDto>();
            CreateMap<PostDisplayDto, PostRatingDto>();
        }
    }
}