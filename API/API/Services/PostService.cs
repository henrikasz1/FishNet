using API.Dtos;
using API.Models;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.LikesDto;
using static API.Models.Enums.PostEnum;

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

        public async Task AddPost(List<IFormFile> file, AddPostDto post)
        {
            var userId = _userAccessorService.GetCurrentUserId();

            var newPost = new Post()
            {
                PostId = Guid.NewGuid(),
                Body = post.Body,
                CreatedAt = DateTime.Now,
                UserId = Guid.Parse(userId),
            };

            _dataContext.Posts.Add(newPost);

            var result = await _dataContext.SaveChangesAsync() > 0;

            foreach (var photo in file)
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

            var observer = _userAccessorService.GetCurrentUserId();

            var postOwner = await _dataContext.Users
                .Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.UserId == post.UserId);
            
            
            var commentsAmount = await _dataContext.Comments
                .CountAsync(comm => comm.Post.PostId == post.PostId);


            if (postOwner.IsProfilePrivate && !postOwner.Friends.Any(x => x.FriendId == Guid.Parse(observer)))
            {
                throw new UnauthorizedAccessException("You are not alowed to view this post");
            }

            var postDto = new GetPostDto
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Body = post.Body,
                CreatedAt = post.CreatedAt,
                LikesCount = post.LikesCount,
                CommentsCount = commentsAmount,
                Photos = post.Photos
            };

            return postDto;
        }

        public async Task<IList<GetLikesDto>> GetPostLikesById(Guid postId)
        {
            var usersDtoList = new List<GetLikesDto>();

            var likes = await _dataContext.PostLikes
                .Where(x => x.ObjectId == postId)
                .ToListAsync();

            foreach (var loverId in likes.Select(x => x.LoverId))
            {
                var user = await _dataContext.Users
                    .Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.UserId == loverId);

                var userMainPhoto = user.Photos != null && user.Photos.Any(x => x.IsMain) ? 
                    user.Photos.First(x => x.IsMain).Url :
                    string.Empty;
                
                usersDtoList.Add(
                    new GetLikesDto()
                    {
                        UserId = loverId,
                        MainPhotoUrl = userMainPhoto,
                        FirstName= user.FirstName,
                        LastName = user.LastName
                    });
            }
            return usersDtoList;
        }

        public async Task<IList<GetPostDto>> GetPostsByUserId(Guid userId, int batchNumber)
        {
            var postsDtoList = new List<GetPostDto>();

            var posts = await _dataContext.Posts.Where(x => x.UserId == userId)
                .Include(y => y.Photos)
                .Skip(5 * batchNumber)
                .Take(5)
                .ToListAsync();

            var observer = _userAccessorService.GetCurrentUserId();

            var postOwner = await _dataContext.Users
                .Include(x => x.Friends)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (postOwner.IsProfilePrivate && !postOwner.Friends.Any(x => x.FriendId == Guid.Parse(observer)))
            {
                throw new UnauthorizedAccessException("You are not alowed to view this user's activity");
            }

            foreach (var post in posts)
            {
                
                var commentsAmount = await _dataContext.Comments
                    .CountAsync(comm => comm.Post.PostId == post.PostId);

                postsDtoList.Add(
                    new GetPostDto
                    {
                        PostId = post.PostId,
                        UserId = post.UserId,
                        Body = post.Body,
                        CreatedAt = post.CreatedAt,
                        LikesCount = post.LikesCount,
                        CommentsCount = commentsAmount,
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
            var observer = _userAccessorService.GetCurrentUserId();

            var posts = await _dataContext.Posts
                .Select(x => x)
                .Include(y => y.Photos)
                .Where(x => x.UserId != Guid.Parse(observer) && x.postType == PostType.Feed)
                .ToListAsync();

            foreach (var post in posts)
            {
                var postOwner = await _dataContext.Users
                    .Include(x => x.Friends)
                    .FirstOrDefaultAsync(x => x.UserId == post.UserId);
                
                var commentsAmount = await _dataContext.Comments
                    .CountAsync(comm => comm.Post.PostId == post.PostId);

                if (postOwner.IsProfilePrivate && postOwner.Friends.Any(x => x.FriendId == Guid.Parse(observer)) 
                    || !postOwner.IsProfilePrivate)
                {
                    postsDtoList.Add(
                    new GetPostDto
                    {
                        PostId = post.PostId,
                        UserId = post.UserId,
                        Body = post.Body,
                        CreatedAt = post.CreatedAt,
                        LikesCount = post.LikesCount,
                        CommentsCount = commentsAmount,
                        Photos = post.Photos
                    });
                }
            }

            return postsDtoList;
        }

 
        public async Task<IList<GetPostDto>> GetRemainingPublicPosts(int batchNumber)
        {
            var postsDtoList = new List<GetPostDto>();
            var observer = _userAccessorService.GetCurrentUserId();

            var posts = await _dataContext.Posts
                .Select(x => x)
                .Include(y => y.Photos)
                .Where(x => x.UserId != Guid.Parse(observer) && x.postType == PostType.Feed)
                .Skip(5*batchNumber)
                .Take(5)
                .ToListAsync();

            foreach (var post in posts)
            {
                var postOwner = await _dataContext.Users
                    .Include(x => x.Friends)
                    .FirstOrDefaultAsync(x => x.UserId == post.UserId);

                var commentsAmount = await _dataContext.Comments
                    .CountAsync(comm => comm.Post.PostId == post.PostId);

                if (!postOwner.IsProfilePrivate && !postOwner.Friends.Any(x => x.FriendId == Guid.Parse(observer)))
                {
                    postsDtoList.Add(
                    new GetPostDto
                    {
                        PostId = post.PostId,
                        UserId = post.UserId,
                        Body = post.Body,
                        CreatedAt = post.CreatedAt,
                        LikesCount = post.LikesCount,
                        CommentsCount = commentsAmount,
                        Photos = post.Photos
                    });
                }
            }

            return postsDtoList;
        }

        public async Task UpdatePostById(Guid postId, EditPostDto newPost)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

            if (post.UserId != Guid.Parse(_userAccessorService.GetCurrentUserId()))
            {
                throw new UnauthorizedAccessException("Only post owner can update it");
            }

            post.Body = newPost.Body ?? post.Body;

            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success)
            {
                throw new DbUpdateException("Could not update post");
            }
        }
    }
}