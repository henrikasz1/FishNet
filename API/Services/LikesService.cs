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

        public LikesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task LikePost(Guid postId, Guid loverId)
        {
            var post = await _dataContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

            post.LikesCount++;

            var newRecord = new PostLikes
            {
                ObjectId = postId,
                LoverId = loverId
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
            var post = await _dataContext.UserPhotos.FirstOrDefaultAsync(x => x.Id == photoId);

            post.LikesCount++;

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
            var post = await _dataContext.UserPhotos.FirstOrDefaultAsync(x => x.Id == photoId);

            post.LikesCount--;

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
    }
}
