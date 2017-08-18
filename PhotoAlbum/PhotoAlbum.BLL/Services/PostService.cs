using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PhotoAlbum.BLL.Interfaces;
using PhotoAlbum.BLL.Utils;
using PhotoAlbum.BLL.Contracts.DTO;
using PhotoAlbum.DAL.Entities;
using PhotoAlbum.DAL.Interfaces;

namespace PhotoAlbum.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _database;

        public PostService(IUnitOfWork database)
        {
            _database = database;
        }
        

        public void Create(PostCreateDto postDto)
        {
            var photo = Mapper.Map<Photo>(postDto);
            _database.Photos.Add(photo);
            _database.Save();

            var post = Mapper.Map<Post>(postDto);

            post.PhotoId = photo.Id;
            post.UserProfileId = _database.UserManager.FindByName(postDto.Login).UserProfile.Id;
            post.Date = DateTime.Now;
            

            _database.Posts.Add(post);
            _database.Save();
        }

        public PostDisplayDto Get(int id)
        {
            return Mapper.Map<PostDisplayDto>(_database.Posts.Find(id));
        }

        public IEnumerable<PostDisplayDto> GetPosts()
        {
            return Mapper.Map<List<PostDisplayDto>>(_database.Posts.ToList());
        }

        public IEnumerable<PostDisplayDto> GetPosts(string username, int pageNumber, string order, int pageSize)
        {
            var posts = _database.Posts
                .Where(p => p.UserProfile.ApplicationUser.UserName == username)
                .OrderBy(order)
                .Page(pageNumber, pageSize)
                .ToList();
            return Mapper.Map<List<PostDisplayDto>>(posts);
        }

        public IEnumerable<PostDisplayDto> GetPosts(int pageNumber, string order, int pageSize)
        {
            var posts = _database.Posts
                .OrderBy(order)
                .Page(pageNumber, pageSize)
                .ToList();
            return Mapper.Map<List<PostDisplayDto>>(posts);
        }

        private readonly int MinMark = 1;
        private readonly int MaxMark = 5;

        public void Rate(PostRateDto postDto)
        {
            if (postDto.Mark < MinMark || postDto.Mark > MaxMark)
            {
                throw new ArgumentOutOfRangeException(nameof(postDto), postDto.Mark,
                    $"Mark not in range ({MinMark}, {MaxMark}");
            }

            if (GetMark(postDto.Login, postDto.PostId) != 0)
            {
                return;
            }

            var post = _database.Posts.Find(postDto.PostId);
            post.Rating = (post.Rating * post.MarksAmount + postDto.Mark) / (post.MarksAmount + 1);
            post.MarksAmount++;


            var user = _database.UserManager.FindByName(postDto.Login).UserProfile;

            var mark = new Mark
            {
                PostId = post.Id,
                UserProfileId = user.Id,
                Rate = postDto.Mark
            };

            _database.Marks.Add(mark);
            _database.Save();
        }

        public int GetMark(string username, int postId)
        {
            var mark = _database.Marks
                            .Where(m => m.UserProfile.ApplicationUser.UserName == username)
                            .FirstOrDefault(m => m.PostId == postId)?.Rate;

            return mark ?? 0;
        }

        public void Remove(int postId)
        {
            var post = _database.Posts.Find(postId);
            _database.Posts.Remove(post);
            _database.Save();
        }

        public void Update(PostEditDto postDto)
        {
            var post = _database.Posts.Find(postDto.Id);
            post.Caption = postDto.Caption;
            post.Description = postDto.Description;

            _database.Save();
        }
    }
}