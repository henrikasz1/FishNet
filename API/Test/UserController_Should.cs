using API.Controllers;
using API.Dtos;
using API.Dtos.Responses;
using API.Models;
using API.Services;
using API.Services.Interfaces;
using AutoFixture.Xunit2;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class UserController_Should
    {
        [Theory]
        [AutoData]
        public async Task ReturnBadRequest_When_ModelStateIsInvalid(
            UserLoginDto userReadModel,
            Guid userId)
        {
            var userMock = CreateUser(userId);
            var authServiceMock = new Mock<IAuthManagementService>();
            var userAccessorServiceMock = new Mock<IUserAccessorService>();
            var dataContextMock = new Mock<DataContext>();
            var userServiceMock = new Mock<IUserService>();

            var sut = new UserController(
                authServiceMock.Object,
                userMock.Object,
                userAccessorServiceMock.Object,
                It.IsAny<DataContext>(),
                userServiceMock.Object);

            sut.ModelState.AddModelError("1", "Bad model state");

            var result = await sut.Login(userReadModel);

            var viewResult = Assert.IsType<BadRequestObjectResult>(result);
            var loginResponse = Assert.IsType<LoginResponse>(viewResult.Value);

            Assert.Equal((int)HttpStatusCode.BadRequest, viewResult.StatusCode);
            Assert.Equal("Invalid payload", loginResponse.Errors[0]);
            Assert.False(loginResponse.Success);
        }

        [Theory]
        [AutoData]
        public async Task ReturnBadRequest_When_ModelStateIsValid_But_UserDoesNotExist(
           UserLoginDto userReadModel,
           Guid userId)
        {
            var userMock = CreateUser(userId);
            var authServiceMock = new Mock<IAuthManagementService>();
            var userAccessorServiceMock = new Mock<IUserAccessorService>();
            var dataContextMock = new Mock<DataContext>();
            var userServiceMock = new Mock<IUserService>();

            userMock
                .Setup(x => x.FindByEmailAsync(userReadModel.Email))
                .ReturnsAsync((User)null);

            var sut = new UserController(
                authServiceMock.Object,
                userMock.Object,
                userAccessorServiceMock.Object,
                It.IsAny<DataContext>(),
                userServiceMock.Object);

            var result = await sut.Login(userReadModel);

            var viewResult = Assert.IsType<BadRequestObjectResult>(result);
            var loginResponse = Assert.IsType<LoginResponse>(viewResult.Value);

            Assert.Equal((int)HttpStatusCode.BadRequest, viewResult.StatusCode);
            Assert.Equal("Invalid login request", loginResponse.Errors[0]);
            Assert.False(loginResponse.Success);
        }

        [Theory]
        [AutoData]
        public async Task ReturnBadRequest_When_ModelStateIsValid_But_PasswordIsIncorrect(
          UserLoginDto userReadModel,
          Guid userId)
        {
            var user = GetUser(userId);

            var userMock = CreateUser(userId);
            var authServiceMock = new Mock<IAuthManagementService>();
            var userAccessorServiceMock = new Mock<IUserAccessorService>();
            var dataContextMock = new Mock<DataContext>();
            var userServiceMock = new Mock<IUserService>();

            userMock
                .Setup(x => x.FindByEmailAsync(userReadModel.Email))
                .ReturnsAsync(user);
            userMock
                .Setup(x => x.CheckPasswordAsync(user, userReadModel.Password))
                .ReturnsAsync(false);

            var sut = new UserController(
                authServiceMock.Object,
                userMock.Object,
                userAccessorServiceMock.Object,
                It.IsAny<DataContext>(),
                userServiceMock.Object);

            var result = await sut.Login(userReadModel);

            var viewResult = Assert.IsType<BadRequestObjectResult>(result);
            var loginResponse = Assert.IsType<LoginResponse>(viewResult.Value);

            Assert.Equal((int)HttpStatusCode.BadRequest, viewResult.StatusCode);
            Assert.Equal("Invalid login request", loginResponse.Errors[0]);
            Assert.False(loginResponse.Success);
        }

        [Theory]
        [AutoData]
        public async Task ReturnOk_When_ModelStateIsValid_And_UserExits_And_PasswordIsCorrect(
          UserLoginDto userReadModel,
          Guid userId,
          string jwtToken)
        {
            var user = GetUser(userId);

            var userMock = CreateUser(userId);
            var authServiceMock = new Mock<IAuthManagementService>();
            var userAccessorServiceMock = new Mock<IUserAccessorService>();
            var dataContextMock = new Mock<DataContext>();
            var userServiceMock = new Mock<IUserService>();

            userMock
                .Setup(x => x.FindByEmailAsync(userReadModel.Email))
                .ReturnsAsync(user);
            userMock
                .Setup(x => x.CheckPasswordAsync(user, userReadModel.Password))
                .ReturnsAsync(true);
            authServiceMock
                .Setup(x => x.GenerateJwtToken(user))
                .Returns(jwtToken);

            var sut = new UserController(
                authServiceMock.Object,
                userMock.Object,
                userAccessorServiceMock.Object,
                It.IsAny<DataContext>(),
                userServiceMock.Object);

            var result = await sut.Login(userReadModel);

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var loginResponse = Assert.IsType<LoginResponse>(viewResult.Value);

            Assert.Equal((int)HttpStatusCode.OK, viewResult.StatusCode);
            Assert.Equal(loginResponse.Token, jwtToken);
            Assert.True(loginResponse.Success);
        }

        private static User GetUser(Guid userId)
        {
            return new User
            {
                UserId = userId,
                FirstName = "firstName",
                LastName = "lastName"
            };
        }

        private static Mock<UserManager<User>> CreateUser(Guid userId)
        {
            var users = new List<User>
            {
                new User {
                    UserId = userId,
                    FirstName = "firstName",
                    LastName = "lastName"
                }
            };

            return MockUserManager(users);
        }

        private static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

    }
}