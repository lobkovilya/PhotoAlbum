using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.DAL.Entities;

namespace PhotoAlbum.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserCreateDto userCreateDto);
        Task<ApplicationUser> FindUserAsync(string login, string password);
        Task UpdateUserAsync(UserEditDto userEditDto);
        Task<UserEditDto> GetUserAsync(string username);
        Task<UserInfoDto> GetUserInfoAsync(string username);
        Task ChangePasswordAsync(UserChangePasswordDto userDto);
    }
}