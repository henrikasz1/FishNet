﻿using API.Dtos.CommentsDtos;
using API.Dtos.Responses;
using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.LikesDto;

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
                Body = body.Body,
                CreatedAt = DateTime.Now
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
        
        

        public async Task<CommentResponse> DeleteComment(Guid commentId)
        {
            var response = new CommentResponse();
            var comment = await _dataContext.Comments.Include(x => x.User).FirstOrDefaultAsync(x => x.CommentId == commentId);

            if (comment == null) { response.Status = "Could not find comment"; return response; }

            var user = Guid.Parse(_userAccessorService.GetCurrentUserId());

            if (comment.User.UserId != user)
            {
                throw new UnauthorizedAccessException("You are not aloud to delete this comment");
            }

            _dataContext.Comments.Remove(comment);
            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result) response.Status = "Failed to delete comment";
            else response.Status = "Comment delete successfully";

            return response;
        }

        
        
        public async Task<IList<GetLikesDto>> GetCommentLikesById(Guid commentId)
        {
            var usersDtoList = new List<GetLikesDto>();

            var likes = await _dataContext.CommentLikes
                .Where(x => x.ObjectId == commentId)
                .ToListAsync();

            foreach (var like in likes)
            {
                var user = await _dataContext.Users
                    .Include(x => x.Photos)
                    .FirstOrDefaultAsync(x => x.UserId == like.LoverId);
                var userMainPhoto = user.Photos.Where(x => x.IsMain == true).Any() ?
                    user.Photos.FirstOrDefault(x => x.IsMain == true).Url : string.Empty;


                usersDtoList.Add(
                    new GetLikesDto()
                    {
                        UserId = like.LoverId,
                        MainPhotoUrl = userMainPhoto,
                        FirstName= user.FirstName,
                        LastName = user.LastName
                    });
            }
            return usersDtoList;
        }
    }
}
