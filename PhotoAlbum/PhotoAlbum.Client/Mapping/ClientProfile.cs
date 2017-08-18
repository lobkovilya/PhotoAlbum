using AutoMapper;
using PhotoAlbum.Client.Models;
using PhotoAlbum.BLL.Contracts.DTO;

namespace PhotoAlbum.Client.Mapping
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<UserCreateModel, UserCreateDto>();
            CreateMap<PostDisplayDto, PostDisplayModel>();
            CreateMap<UserChangePasswordModel, UserChangePasswordDto>();
            CreateMap<PostCreateModel, PostCreateDto>();

            CreateMap<PostEditDto, PostEditModel>();
            CreateMap<PostEditModel, PostEditDto>();

            CreateMap<UserEditDto, UserEditModel>();
            CreateMap<UserEditModel, UserEditDto>();

            CreateMap<PostRateModel, PostRateDto>();

            CreateMap<UserCreateModel, UserLoginModel>();
            CreateMap<PostCreateModel, PostCreateDto>();
        }
    }
}