using API.Dtos;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.GroupId == groupId);

            if (user != group.User.UserId)
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

            foreach (var photo in files)
            {
                await _postPhotoService.SavePostPhoto(photo, newPost.PostId);
            }

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Could not add post to group");
            }
        }

        //Add group post
        //delete group post
        //get all group posts
    }
}
