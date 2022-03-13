using API.Models;
using API.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class LikesService : ILikesService
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessorService _userAccessorService;

        public LikesService(DataContext dataContext, IUserAccessorService userAccessorService)
        {
            _dataContext = dataContext;
            _userAccessorService = userAccessorService;
        }

        public async Task LikePost(Guid postId)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
            var userId = _userAccessorService.GetCurrentUserId();
            post.LikesCount++;

            var newRecord = new PostLikes
            {
                ObjectId = postId,
                LoverId = Guid.Parse(userId)
            };

            if (await _dataContext.PostLikes.AnyAsync(x => x.LoverId == newRecord.LoverId && x.ObjectId == newRecord.ObjectId))
            {
                throw new Exception("You have already liked this post");
            }

            _dataContext.PostLikes.Add(newRecord);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Could not update likes count");
            }
        }

        public async Task UnlikePost(Guid postId, Guid loverId)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

            post.LikesCount--;

            var newRecord = new PostLikes
            {
                ObjectId = postId,
                LoverId = loverId
            };

            if (await _dataContext.PostLikes.AnyAsync(x => x.LoverId == newRecord.LoverId && x.ObjectId == newRecord.ObjectId))
            {
                _dataContext.PostLikes.Remove(newRecord);

                var result = await _dataContext.SaveChangesAsync() > 0;

                if (!result)
                {
                    throw new DbUpdateException("Could not update likes count");
                }

            }
            else
            {
                throw new Exception("You have already unliked this post");
            }
        }

        public async Task LikeUserPhoto(string photoId, Guid loverId)
        {
            var photo = await _dataContext.UserPhotos.FirstOrDefaultAsync(x => x.Id == photoId);

            photo.LikesCount++;

            var newRecord = new PhotoLikes
            {
                ObjectId = photoId,
                LoverId = loverId
            };

            if (await _dataContext.PhotoLikes.AnyAsync(x => x.LoverId == newRecord.LoverId && x.ObjectId == newRecord.ObjectId))
            {
                throw new Exception("You have already liked this photo");
            }

            _dataContext.PhotoLikes.Add(newRecord);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Could not update likes count");
            }
        }

        public async Task UnlikePhoto(string photoId, Guid loverId)
        {
            var photo = await _dataContext.UserPhotos.FirstOrDefaultAsync(x => x.Id == photoId);

            photo.LikesCount--;

            var newRecord = new PhotoLikes
            {
                ObjectId = photoId,
                LoverId = loverId
            };

            if (await _dataContext.PhotoLikes.AnyAsync(x => x.LoverId == newRecord.LoverId && x.ObjectId == newRecord.ObjectId))
            {
                _dataContext.PhotoLikes.Remove(newRecord);

                var result = await _dataContext.SaveChangesAsync() > 0;

                if (!result)
                {
                    throw new DbUpdateException("Could not update likes count");
                }

            }
            else
            {
                throw new Exception("You have already unliked this post");
            }
        }

        public async Task LikeComment(Guid commentId)
        {
            var user = Guid.Parse(_userAccessorService.GetCurrentUserId());
            var comment = await _dataContext.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);

            comment.LikesCount++;

            var newRecord = new CommentLikes
            {
                ObjectId = commentId,
                LoverId = user
            };

            if (await _dataContext.CommentLikes.AnyAsync(x => x.LoverId == newRecord.LoverId && x.ObjectId == newRecord.ObjectId))
            {
                throw new Exception("You have already liked this comment");
            }

            _dataContext.CommentLikes.Add(newRecord);

            var result = await _dataContext.SaveChangesAsync() > 0;

            if (!result)
            {
                throw new DbUpdateException("Could not update likes count");
            }
        }

        public async Task UnlikeComment(Guid commentId)
        {
            var user = Guid.Parse(_userAccessorService.GetCurrentUserId());
            var comment = await _dataContext.Comments.FirstOrDefaultAsync(x => x.CommentId == commentId);

            comment.LikesCount--;

            var newRecord = new CommentLikes
            {
                ObjectId = commentId,
                LoverId = user
            };

            if (await _dataContext.CommentLikes.AnyAsync(x => x.LoverId == newRecord.LoverId && x.ObjectId == newRecord.ObjectId))
            {
                _dataContext.CommentLikes.Remove(newRecord);

                var result = await _dataContext.SaveChangesAsync() > 0;

                if (!result)
                {
                    throw new DbUpdateException("Could not update likes count");
                }
            }
            else
            {
                throw new Exception("You have already unliked this comment");
            }
        }
    }
}
