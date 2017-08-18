using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using PhotoAlbum.Client.Models;
using PhotoAlbum.BLL.Contracts.DTO;

namespace PhotoAlbum.Client.Utils
{
    public interface IPhotoAlbumProxy
    {
        Task AuthorizeAsync(UserLoginModel model);
        Task RegisterAsync(UserCreateModel model);

        Task<UserInfoDto> UserInfoAsync(string username);
        Task<UserEditModel> UserAsync(string username);
        Task<IEnumerable<PostDisplayModel>> PostsAsync(string username, int pageNumber, string order);
        Task<PostEditModel> PostEditAsync(int postId);

        Task CreatePostAsync(HttpPostedFileBase photo, PostCreateModel model);
        Task EditUserAsync(UserEditModel model);
        Task ChangePasswordAsync(UserChangePasswordModel model);
        Task<PostRatingDto> RatePostAsync(PostRateDto rateDto);
        Task<PostRatingDto> GetMark(int postId);
        Task RemovePostAsync(int postId);
        Task EditPostAsync(PostEditModel model);

        Task<PhotoDto> GetPhotoAsync(int id);
    }
}