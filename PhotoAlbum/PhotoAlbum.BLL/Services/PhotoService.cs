using System.Data.Entity;
using AutoMapper;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.DAL.EF;
using PhotoAlbum.DAL.Interfaces;

namespace PhotoAlbum.BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _database;

        public PhotoService(IUnitOfWork database)
        {
            _database = database;
        }

        public PhotoDto Get(int id)
        {
            return Mapper.Map<PhotoDto>(_database.Photos.Find(id));
        }
    }
}