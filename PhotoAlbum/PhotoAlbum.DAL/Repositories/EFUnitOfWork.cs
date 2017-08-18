using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoAlbum.BLL.Contracts.Infrastructure;
using PhotoAlbum.DAL.EF;
using PhotoAlbum.DAL.Entities;
using PhotoAlbum.DAL.Interfaces;

namespace PhotoAlbum.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly PhotoAlbumContext _ctx;

        //private MarkRepository _markRepository;
        //private PhotoRepository _photoRepository;
        //private PostRepository _postRepository;
        //private UserProfileRepository _userProfileRepository;

        private ApplicationUserManager _userManager;

        public int CurrentIndex { get; set; }
        public static int Amount { get; set; }

        public EFUnitOfWork(ConnectionStringProvider connectionStringProvider)
        {
            CurrentIndex = Amount++;
            _ctx = new PhotoAlbumContext(connectionStringProvider.ConnectionString);
        }

        public IDbSet<Post> Posts => _ctx.Posts;

        public IDbSet<UserProfile> UserProfiles => _ctx.UserProfiles;

        public IDbSet<Mark> Marks => _ctx.Marks;

        public IDbSet<Photo> Photos => _ctx.Photos;

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_ctx));
                }
                return _userManager;
            }
        }
        //public IRepository<Mark> Marks
        //{
        //    get
        //    {
        //        if (_markRepository == null)
        //        {
        //            _markRepository = new MarkRepository(_ctx);
        //        }
        //        return _markRepository;
        //    }
        //}

        //public IRepository<Photo> Photos
        //{
        //    get
        //    {
        //        if (_photoRepository == null)
        //        {
        //            _photoRepository = new PhotoRepository(_ctx);
        //        }
        //        return _photoRepository;
        //    }
        //}

        //public IRepository<Post> Posts
        //{
        //    get
        //    {
        //        if (_postRepository == null)
        //        {
        //            _postRepository = new PostRepository(_ctx);
        //        }
        //        return _postRepository;
        //    }
        //}

        //public IRepository<UserProfile> UserProfiles
        //{
        //    get
        //    {
        //        if (_userProfileRepository == null)
        //        {
        //            _userProfileRepository = new UserProfileRepository(_ctx);
        //        }
        //        return _userProfileRepository;
        //    }
        //}

        public void Save()
        {
            _ctx.SaveChanges();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
                _disposed = true;
            }
        }

        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}