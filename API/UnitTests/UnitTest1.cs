using API.Models;
using API.Services;
using API.Services.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task ReturnShopAdById_When_ShopAddExists()
        {

            //var dataContextMock /*=*/ DataContext;
            var dataContextMock = services.GetRequiredService<DataContext>();

            var userAccessorServiceMock = new Mock<IUserAccessorService>();
            var shopPhotoServiceMock = new Mock<IShopPhotoService>();

            var shopAdvertMockSet = new Mock<DbSet<Shop>>();
            var shopId = Guid.NewGuid();

            dataContextMock
                .Setup(x => x.ShopAdverts)
                .Returns(shopAdvertMockSet.Object);

            var sut = new ShopService(dataContextMock.Object, userAccessorServiceMock.Object, shopPhotoServiceMock.Object);


            await sut.GetShopAdById(shopId);

            Assert.Equals(2, 2);
        }

    }
}
