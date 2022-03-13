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
    public class GroupPostService : IGroupPostService
    {
        private readonly IUserAccessorService _userAccessorService;
        private readonly IPostPhotoService _postPhotoService;
        private readonly DataContext _dataContext;

        public GroupPostService(IUserAccessorService userAccessorService, IPostPhotoService postPhotoService, DataContext dataContext)
        {
            _userAccessorService = userAccessorService;
            _postPhotoService = postPhotoService;
            _dataContext = dataContext;
        }

        public async Task AddGroupPost(List<IFormFile> files, Guid groupId, AddPostDto post)
        {
            var user = Guid.Parse(_userAccessorService.GetCurrentUserId());
            var group = await _dataContext.Groups
                .Include(x => x.Members)
                .FirstOrDefaultAsync(x => x.GroupId == groupId);

            if (group.Members.Any(x => x.UserId != user))
            {
                throw new UnauthorizedAccessException("You are not a participant of this group");
            }

            var newPost = new Post()
            {
                PostId = Guid.NewGuid(),
                Body = post.Body,
                CreatedAt = DateTime.Now,
                UserId = user,
                postType = PostType.Group
            };

            group.Posts = new List<GroupPost>();
            group.Posts.Add(new GroupPost
            {
                GroupId = groupId,
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

        public async Task DeleteGroupPost(Guid postId)
        {
            var userId = Guid.Parse(_userAccessorService.GetCurrentUserId());

            var postToDelete = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == postId);

            var groupPost = await _dataContext.GroupPosts.FirstOrDefaultAsync(x => x.PostId == postId);

            if (postToDelete.UserId != userId)
            {
                throw new UnauthorizedAccessException("User does not own this post");
            }

            foreach (var photo in postToDelete.Photos)
            {
                await _postPhotoService.DeletePostPhoto(photo);
            }

            _dataContext.Posts.Remove(postToDelete);
            _dataContext.GroupPosts.Remove(groupPost);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Unable to remove post");
            }
        }

        public async Task<IList<GetPostDto>> GetAllGroupPosts(Guid groupId)
        {
            var postsDtoList = new List<GetPostDto>();
            var observer = Guid.Parse(_userAccessorService.GetCurrentUserId());
            var group = await _dataContext.Groups.Include(x => x.Members).FirstOrDefaultAsync(x => x.GroupId == groupId);

            if (group.Members.Any(x => x.UserId != observer))
            {
                    throw new UnauthorizedAccessException("You are not a participant of this group");
            }

            var groupPosts = await _dataContext.GroupPosts.Where(x => x.GroupId == groupId).ToListAsync();

            var posts = await _dataContext.Posts
                .Select(x => x)
                .Include(y => y.Photos)
                .ToListAsync();

            foreach (var groupPost in groupPosts)
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
