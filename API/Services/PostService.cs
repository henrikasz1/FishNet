﻿using API.Dtos;
using API.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;
        private readonly IPostPhotoService _photoService;

        public PostService(
            DataContext dataContext,
            IUserAccessorService userAccessorService,
            IPostPhotoService photoService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _photoService = photoService;
        }

        public async Task AddPost(List<IFormFile> files, AddPostDto post)
        {
            var userId = _userAccessorService.GetCurrentUserId();

            var user = await _dataContext.Users.Include(x => x.Posts)
              .FirstOrDefaultAsync(y => y.UserId == Guid.Parse(userId));

            var newPost = new Post()
            {
                PostId = Guid.NewGuid(),
                Body = post.Body,
                CreatedAt = DateTime.Now,
                UserId = Guid.Parse(userId),
            };

            _dataContext.Posts.Add(newPost);

            var result = await _dataContext.SaveChangesAsync() > 0;

            foreach (var photo in files)
            {
                await _photoService.SavePostPhoto(photo, newPost.PostId);
            }

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }

        public async Task<GetPostDto> GetPostById(Guid postId)
        {
            var post = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == postId);

            //---

            //var postOwner = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserId == post.UserId);
            //var observerId = _userAccessorService.GetCurrentUserId();

            //if (postOwner.IsProfilePrivate == true && )
            //{
            //    //get friendship state and then return null or ok
            //}
            //---

            var postDto = new GetPostDto
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Body = post.Body,
                CreatedAt = post.CreatedAt,
                LikesCount = post.LikesCount,
                Photos = post.Photos
            };

            return postDto;
        }

        public async Task<IList<GetPostDto>> GetPostsByUserId(Guid userId)
        {
            var postsDtoList = new List<GetPostDto>();

            var posts = await _dataContext.Posts.Where(x => x.UserId == userId)
                .Include(y => y.Photos)
                .ToListAsync();

            foreach (var post in posts)
            {
                postsDtoList.Add(
                    new GetPostDto
                    {
                        PostId = post.PostId,
                        UserId = post.UserId,
                        Body = post.Body,
                        CreatedAt = post.CreatedAt,
                        LikesCount = post.LikesCount,
                        Photos = post.Photos
                    });
            }

            return postsDtoList;
        }

        //The purpose of that is to get posts for feed, later will be displayed posts of friends only, 
        //also would be nice to have pagination (if soc net would grow big)
        public async Task<IList<GetPostDto>> GetAllPosts()
        {
            var postsDtoList = new List<GetPostDto>();

            var posts = await _dataContext.Posts
                .Select(x => x)
                .Include(y => y.Photos)
                .ToListAsync();

            foreach (var post in posts)
            {
                postsDtoList.Add(
                    new GetPostDto
                    {
                        PostId = post.PostId,
                        UserId = post.UserId,
                        Body = post.Body,
                        CreatedAt = post.CreatedAt,
                        LikesCount = post.LikesCount,
                        Photos = post.Photos
                    });
            }

            return postsDtoList;
        }

        public async Task DeletePostById(string id)
        {
            var userId = _userAccessorService.GetCurrentUserId();

            var postToDelete = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == Guid.Parse(id));

            if (postToDelete.UserId != Guid.Parse(userId))
            {
                throw new UnauthorizedAccessException("User does not own this post");
            }

            foreach (var photo in postToDelete.Photos)
            {
                await _photoService.DeletePostPhoto(photo);
            }

            _dataContext.Posts.Remove(postToDelete);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to remove post");
            }
        }

        public async Task UpdatePostById(Guid postId, EditPostDto newPost)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

            if (post.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Only post owner can update it");
            }

            post.Body = newPost.Body ?? newPost.Body;

            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success)
            {
                throw new DbUpdateException("Could not update post");
            }
        }
    }
}