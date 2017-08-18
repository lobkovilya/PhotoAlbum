using AutoMapper;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.DAL.Entities;

namespace PhotoAlbum.BLL.Mapping
{
    public class BusinessLogicProfile : Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<UserProfile, UserCreateDto>();
            CreateMap<Post, PostCreateDto>();
            CreateMap<UserCreateDto, UserProfile>();
            CreateMap<UserProfile, UserInfoDto>();
            CreateMap<PhotoDto, Photo>();
            CreateMap<PostCreateDto, Post>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<Post, PostDisplayDto>()
                .ForMember(m => m.Login, cfg => cfg.MapFrom(x => x.UserProfile.ApplicationUser.UserName));
            //    .ForMember(m => m.PhotoUrl,
            //cfg => cfg.MapFrom(x => $"{System.Configuration.ConfigurationManager.AppSettings["ImageHostPrefix"]}/{x.PhotoId}"));

            CreateMap<PostCreateDto, Post>();
            CreateMap<PostCreateDto, Photo>();
            CreateMap<UserEditDto, UserProfile>();
        }
    }
}