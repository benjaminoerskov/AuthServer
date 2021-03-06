﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DetSkerIAarhusBackend.API.Controllers;
using DetSkerIAarhusBackend.API.DTOs;
using DetSkerIAarhusBackend.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;


namespace DetSkerIAarhusBackend.UnitTests
{
    [TestClass]
    public class AccountControllerUnitTests
    {
        public AccountController _accountController = null;

        [TestInitialize]
        public void SetUp()
        {
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<ApplicationUser>>(userManager, null, null, null, null, null);



            ApplicationUser testUser = new ApplicationUser
            {
                Email = "TestEmail",
                UserName = "TestUserName"
            };
            signInManager.Setup(x => x.PasswordSignInAsync(testUser.UserName, "TestPassword", false, false)).Returns(Task.FromResult<SignInResult>(SignInResult.Success));

            var testList = new List<ApplicationUser>();
            testList.Add(testUser);

            userManager.Setup(x =>x.Users).Returns(testList.AsQueryable());

            //var foo = userManager.Object.Users.SingleOrDefault();
            
            //userManager.Setup(x => x.Users.SingleOrDefault(It.IsAny<Func<ApplicationUser, bool>>()))
            //    .Returns(new Func<ApplicationUser, bool>, IQueryable<ApplicationUser>())


            _accountController = new AccountController(userManager.Object, signInManager.Object, null);

            //var result = _accountController.Register(
            //new RegisterDto
            //{
            //    Email = "test@email",
            //    UserName = "TestUserName",
            //    Password = "TestPassword",
            //    ConfirmPassword = "TestPassword"
            //}
            //);


        }

        [TestCleanup]
        public void CleanUp()
        {

        }

        //[TestMethod]
        public void TestLogin()
        {
            var result = _accountController.Login(
                new LoginDto { Email = "TestUserName", Password = "TestPassword" });

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var token = okResult.Value.Should().BeAssignableTo<string>().Subject;

            token.Length.Should().BeGreaterThan(200);
        }
    }
}
