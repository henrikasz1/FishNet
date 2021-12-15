using API.Dtos;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static API.Models.Enums.PostEnum;

namespace API.Services
{
    public class OccasionPostService : IOccasionPostService
    {
        private readonly IUserAccessorService _userAccessorService;
        private readonly IPostPhotoService _postPhotoService;
        private readonly DataContext _dataContext;

        public OccasionPostService(IUserAccessorService userAccessorService, IPostPhotoService postPhotoService, DataContext dataContext)
        {
            _userAccessorService = userAccessorService;
            _postPhotoService = postPhotoService;
            _dataContext = dataContext;
        }

        public async Task AddOccasionPost(List<IFormFile> files, Guid occasionId, AddPostDto post)
        {
            var user = Guid.Parse(_userAccessorService.GetCurrentUserId());
            var occasion = await _dataContext.Occasions
                .Include(x => x.Participants)
                .FirstOrDefaultAsync(x => x.OccasionId == occasionId);

            if (occasion.Participants.Any(x => x.UserId != user))
            {
                throw new UnauthorizedAccessException("You are not a participant of this occasion");
            }

            var newPost = new Post()
            {
                PostId = Guid.NewGuid(),
                Body = post.Body,
                CreatedAt = DateTime.Now,
                UserId = user,
                postType = PostType.Occasion
            };

            occasion.Posts = new List<OccasionPost>();
            occasion.Posts.Add(new OccasionPost
            {
                OccasionId = occasionId,
                PostId = newPost.PostId
            });

            _dataContext.Posts.Add(newPost);
            var result = await _dataContext.SaveChangesAsync() > 0;

            foreach (var photo in files)
            {
                await _postPhotoService.SavePostPhoto(photo, newPost.PostId);
            }

            if (!result)
            {
                throw new DbUpdateException("Could not add post to group");
            }
        }

        public async Task DeleteOccasionPost(Guid postId)
        {
            var userId = Guid.Parse(_userAccessorService.GetCurrentUserId());

            var postToDelete = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == postId);

            var occasionPost = await _dataContext.OccasionPosts.FirstOrDefaultAsync(x => x.PostId == postId);

            if (postToDelete.UserId != userId)
            {
                throw new UnauthorizedAccessException("User does not own this post");
            }

            foreach (var photo in postToDelete.Photos)
            {
                await _postPhotoService.DeletePostPhoto(photo);
            }

            _dataContext.Posts.Remove(postToDelete);
            _dataContext.OccasionPosts.Remove(occasionPost);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to remove post");
            }
        }

        public async Task<IList<GetPostDto>> GetAllOccasionPosts(Guid occasionId)
        {
            var postsDtoList = new List<GetPostDto>();
            var observer = Guid.Parse(_userAccessorService.GetCurrentUserId());
            var occasion = await _dataContext.Occasions.Include(x => x.Participants).FirstOrDefaultAsync(x => x.OccasionId == occasionId);

            if (occasion.Participants.Any(x => x.UserId != observer))
            {
                throw new UnauthorizedAccessException("You are not a participant of this group");
            }

            var occasionPosts = await _dataContext.OccasionPosts.Where(x => x.OccasionId == occasionId).ToListAsync();

            var posts = await _dataContext.Posts
                .Select(x => x)
                .Include(y => y.Photos)
                .ToListAsync();

            foreach (var groupPost in occasionPosts)
            {
                var post = await _dataContext.Posts.Include(x => x.Photos).FirstOrDefaultAsync(x => x.PostId == groupPost.PostId);

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
    }
}
