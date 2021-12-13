using API.Dtos.CommentsDtos;
using API.Dtos.Responses;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserAccessorService _userAccessorService;
        private readonly DataContext _dataContext;

        public CommentService(IUserAccessorService userAccessorService, DataContext dataContext)
        {
            _userAccessorService = userAccessorService;
            _dataContext = dataContext;
        }

        public async Task<CommentResponse> AddComment(Guid postId, CommentDto body)
        {
            var response = new CommentResponse();
            var post = await _dataContext.Posts
              .Include(x => x.Comments)
              .FirstOrDefaultAsync(x => x.PostId == postId);

            if (post == null)
            {
                response.Status = "Post not found";
                return response;
            }

            var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserId == Guid.Parse(_userAccessorService.GetCurrentUserId()));

            var comment = new Comment()
            {
                CommentId = Guid.NewGuid(),
                Post = post,
                User = user,
                Body = body.Body
            };

            _dataContext.Comments.Add(comment);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                response.Status = "Could not add comment";
            }
            else
            {
                response.Status = "Commented successfully";
            }

            return response;
        }

        public async Task<IList<Comment>> GetAllPostComments(Guid postId)
        {
            var post = await _dataContext.Posts
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.PostId == postId);

            var result = new List<Comment>();

            foreach (var comment in post.Comments)
            {
                result.Add(comment);
            }
            //result dto, not comment itself
            return result;
        }
    }
}
