using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoAlbum.BLL.Contracts.Infrastructure;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.DAL.Entities;
using PhotoAlbum.DAL.Interfaces;

namespace PhotoAlbum.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _database;

        public UserService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task CreateAsync(UserCreateDto userCreateDto)
        {
            ApplicationUser user = await _database.UserManager.FindByNameAsync(userCreateDto.Login);
            if (user != null)
            {
                throw new ValidationException("This login is not available, please choose another", "Login");
            }

            user = new ApplicationUser
            {
                Email = userCreateDto.Email,
                UserName = userCreateDto.Login
            };

            var result = await _database.UserManager.CreateAsync(user, userCreateDto.Password);

            if (!result.Succeeded)
            {
                throw new ValidationException(string.Join("; ", result.Errors));
            }
            
            UserProfile userProfile = Mapper.Map<UserProfile>(userCreateDto);
            userProfile.ApplicationUser = user;
            _database.UserProfiles.Add(userProfile);
            _database.Save();
            
        }

        public async Task<ApplicationUser> FindUserAsync(string login, string password)
        {
            return await _database.UserManager.FindAsync(login, password);
        }

        public async Task UpdateUserAsync(UserEditDto userEditDto)
        {
            var user = await _database.UserManager.FindByNameAsync(userEditDto.Login);

            if (user == null)
            {
                throw new ValidationException("Wrong login", "Login");
            }
            Mapper.Map(userEditDto, user.UserProfile);
            user.Email = userEditDto.Email;

            _database.UserManager.Update(user);
            _database.Save();
        }

        public async Task<UserEditDto> GetUserAsync(string username)
        {
            var user = await _database.UserManager.FindByNameAsync(username);
            return new UserEditDto
            {
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                Email = user.Email,
                Login = user.UserName
            };
        }

        public async Task<UserInfoDto> GetUserInfoAsync(string username)
        {
            var user = await _database.UserManager.FindByNameAsync(username);
            return Mapper.Map<UserInfoDto>(user.UserProfile);
        }

        public async Task ChangePasswordAsync(UserChangePasswordDto userDto)
        {
            var user = await _database.UserManager.FindByNameAsync(userDto.Login);

            var identityResult = await _database.UserManager
                .ChangePasswordAsync(user.Id, userDto.CurrentPassword, userDto.NewPassword);

            if (!identityResult.Succeeded)
            {
                throw new ValidationException(string.Join("; ", identityResult.Errors));
            }

            _database.Save();
        }
    }
}