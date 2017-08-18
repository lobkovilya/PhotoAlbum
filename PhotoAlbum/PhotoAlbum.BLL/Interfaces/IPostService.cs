using System.Collections.Generic;
using PhotoAlbum.BLL.Contracts.DTO;

namespace PhotoAlbum.BLL.Interfaces
{
    public interface IPostService
    {
        void Create(PostCreateDto postDto/*, PhotoDto photoDto*/);
        PostDisplayDto Get(int id);
        IEnumerable<PostDisplayDto> GetPosts();
        IEnumerable<PostDisplayDto> GetPosts(string username, int pageNumber, string order, int pageSize);
        IEnumerable<PostDisplayDto> GetPosts(int pageNumber, string order, int pageSize);
        void Rate(PostRateDto postDto);
        int GetMark(string username, int postId);
        void Remove(int postId);
        void Update(PostEditDto postDto);
    }
}