using API.Models;
using API.Services;
using API.Services.Interfaces;
using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class LikesService_Should
    {
        //private var fixture = new Fixture();
        private readonly ILikesService _sut;

        public LikesService_Should()
        {
            //var fixture = new Fixture();

            //fixture.Register<LikesService>(() => null);
            //_sut = fixture.Create<LikesService>();

            //_sut = fixture.Create<LikesService>();
            _sut = new LikesService();
        }

        [Theory, AutoData]
        public async Task ThrowException_When_PhotoIsAlreadyLiked(
            [Frozen] Mock<IUserAccessorService> userAccessorServiceMock,
            Guid postId,
            Guid userId,
            PostLikes postLikes)
        {
            postLikes.LoverId = userId;
            postLikes.ObjectId = postId;

            var postMock = new Mock<DbSet<Post>>();
            var postLikesMock = new Mock<DbSet<PostLikes>>(postLikes);

            var posts = new List<Post>();
            posts.Add(CreatePost());
            var postsLikes = new List<PostLikes>();
            postsLikes.Add(postLikes);

            GetQueryableMockDbSet(posts);
            GetQueryableMockDbSet(postsLikes);
            
            userAccessorServiceMock
                .Setup(r => r.GetCurrentUserId())
                .Returns(userId.ToString());

            var sut = new LikesService(userAccessorServiceMock);

            await Assert.ThrowsAsync<Exception>(() => _sut.LikePost(postId));

            //Assert.Throws<Exception>();
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        private static Post CreatePost()
        {
            return new Post()
            {
                PostId = Guid.NewGuid(),
                Body = "body",
                CreatedAt = DateTime.UtcNow,
                UserId = Guid.NewGuid(),
                Comments = new List<Comment>(),
                Photos = new List<PostPhoto>(),
                LikesCount = 0
            };
        }
    }
}
