using System;
using System.Data.Entity;
using PhotoAlbum.DAL.Entities;
using PhotoAlbum.DAL.Repositories;

namespace PhotoAlbum.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<Post> Posts { get; }
        //IRepository<UserProfile> UserProfiles { get; }
        //IRepository<Mark> Marks { get; }
        //IRepository<Photo> Photos { get; }

        //ApplicationUserManager UserManager { get; }

        IDbSet<Post> Posts { get; }
        IDbSet<UserProfile> UserProfiles { get; }
        IDbSet<Mark> Marks { get; }
        IDbSet<Photo> Photos { get; }
        ApplicationUserManager UserManager { get;  }
        void Save();
    }
}