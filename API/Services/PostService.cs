using API.Dtos;
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
        private readonly IPhotoService _photoService;

        public PostService(
            DataContext dataContext,
            IUserAccessorService userAccessorService,
            IPhotoService photoService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
            _photoService = photoService;
        }

        public async Task AddPost(IFormFile file, AddPostDto post)
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

            await _photoService.SavePostPhoto(file, newPost.PostId);

            if (!result)
            {
                throw new DbUpdateException("Failed to add");
            }
        }

        public async Task<GetPostDto> GetPostById(Guid postId)
        {
            var post = await _dataContext.Posts.Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.PostId == postId);

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

        //The purpose of that is to get posts for feed, later will be displayed posts of friends only, also would be nice to have pagination (if soc net would grow big)
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

        //Future methods 

        //put service
        //delete service
    }
}
