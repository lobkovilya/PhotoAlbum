using System.Collections.Generic;
using PhotoAlbum.BLL.Contracts.DTO;

namespace PhotoAlbum.BLL.Interfaces
{
    public interface IPhotoService
    {
        PhotoDto Get(int id);
    }
}