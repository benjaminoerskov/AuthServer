using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VentureAarhusBackend.API.Controllers;
using VentureAarhusBackend.API.DTOs;
using VentureAarhusBackend.API.Entities;
using VentureAarhusBackend.API.Models;
using VentureAarhusBackend.API.Repositories;

namespace VentureAarhusBackend.UnitTests
{
    [TestClass]
    public class AssociatedOccurrencesControllerTests
    {
        private AssociatedOccurrencesController _controller = null;

        [TestInitialize]
        public void SetUp()
        {
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase();
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var mockRepo = new Mock<Repository<AssociatedOccurrences>>(dbContext);
            var mockMapper = new Mock<IMapper>();

            //Setup methods:

            var testUser = new ApplicationUser()
            {
                Email = "test@test.com",
                Id = "testID"
            };
            
            _controller = new AssociatedOccurrencesController(mockUserManager.Object, mockRepo.Object, mockMapper.Object);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, testUser.Email),
                    }))
                }
            };

            var occurrencesList = new List<AssociatedOccurrences>();

            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = testUser,
                Id = 30,
                ApplicationUserId = testUser.Id,
                Type = "like"
            };
            occurrencesList.Add(associated1);
            mockRepo.Setup(x => x.GetAll()).Returns(occurrencesList.AsQueryable());
            mockUserManager.Setup(x => x.GetUserAsync(_controller.User)).Returns(Task.FromResult<ApplicationUser>(testUser));

            var AssociatedDTO = new AssociatedOccurrencesDTO
            {
                ApplicationUserId = testUser.Id,
                Id = 500,
                OccurrenceId = 20,
                Type = "like"
            };

            mockMapper.Setup(x =>
                x.Map<AssociatedOccurrencesDTO>(It.IsAny<AssociatedOccurrences>())).Returns(AssociatedDTO);
        }

        [TestMethod]
        public void PostAssociatedOccurrence()
        {
            var param = new PostAssociatedOccurrencesParams
            {
                OccurrenceId = 20,
                TypeOfAssociation = "like"
            };

            var response = _controller.PostAssociatedOccurrence(param).Result;
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            var occurrences = okResult.Value.Should().BeAssignableTo<AssociatedOccurrencesDTO>().Subject;
            occurrences.ApplicationUserId.Should().Be("testID");
            occurrences.Type.Should().Be("like");
            occurrences.Id.Should().Be(500);
            occurrences.OccurrenceId.Should().Be(20);
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

        
    }
}
