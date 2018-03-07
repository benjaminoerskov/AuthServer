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
        private Mock<IMapper> _mockMapper = null;
        private ApplicationUser _testUser = null;

        private DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder = null;
        private ApplicationDbContext _dbContext = null;

        private Mock<Repository<AssociatedOccurrences>> _mockRepo = null;


        [TestInitialize]
        public void SetUp()
        {
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase();
            //_optionsBuilder.UseInMemoryDatabase();
            _dbContext = new ApplicationDbContext(_optionsBuilder.Options);

            _mockRepo = new Mock<Repository<AssociatedOccurrences>>(_dbContext);

            _mockMapper = new Mock<IMapper>();
            _controller = new AssociatedOccurrencesController(mockUserManager.Object, _mockRepo.Object, _mockMapper.Object);
            _testUser = new ApplicationUser()
            {
                Email = "test@test.com",
                Id = "testID"
            };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, _testUser.Email),
                    }))
                }
            };

            //Setup methods:
            mockUserManager.Setup(x => x.GetUserAsync(_controller.User)).Returns(Task.FromResult<ApplicationUser>(_testUser)); 
        }

        [TestMethod]
        public void PostAssociatedOccurrencePositive()
        {
            var param = new PostAssociatedOccurrencesParams
            {
                OccurrenceId = 20,
                TypeOfAssociation = "like"
            };

            var AssociatedDTO = new AssociatedOccurrencesDTO
            {
                ApplicationUserId = _testUser.Id,
                Id = 500,
                OccurrenceId = 20,
                Type = "like"
            };

            var occurrencesList = new List<AssociatedOccurrences>();

            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 30,
                ApplicationUserId = _testUser.Id,
                Type = "like",
                OccurrenceId = 1
            };
            occurrencesList.Add(associated1);
            _mockRepo.Setup(x => x.GetAll()).Returns(occurrencesList.AsQueryable());

            _mockMapper.Setup(x =>
                x.Map<AssociatedOccurrencesDTO>(It.IsAny<AssociatedOccurrences>())).Returns(AssociatedDTO);


            var response = _controller.PostAssociatedOccurrence(param).Result;
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            var occurrences = okResult.Value.Should().BeAssignableTo<AssociatedOccurrencesDTO>().Subject;
            occurrences.ApplicationUserId.Should().Be("testID");
            occurrences.Type.Should().Be("like");
            occurrences.Id.Should().Be(500);
            occurrences.OccurrenceId.Should().Be(20);
        }

        [TestMethod]
        public void PostAssociatedOccurrenceNegative()
        {
            var param = new PostAssociatedOccurrencesParams
            {
                OccurrenceId = 20,
                TypeOfAssociation = "like"
            };

            var AssociatedDTO = new AssociatedOccurrencesDTO
            {
                ApplicationUserId = _testUser.Id,
                Id = 500,
                OccurrenceId = 20,
                Type = "like"
            };

            var occurrencesList = new List<AssociatedOccurrences>();

            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 30,
                ApplicationUserId = _testUser.Id,
                Type = "like",
                OccurrenceId = 20
            };
            occurrencesList.Add(associated1);

            _mockRepo.Setup(x => x.GetAll()).Returns(occurrencesList.AsQueryable());

            _mockMapper.Setup(x =>
                x.Map<AssociatedOccurrencesDTO>(It.IsAny<AssociatedOccurrences>())).Returns(AssociatedDTO);


            var response = _controller.PostAssociatedOccurrence(param).Result;
            var okResult = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

        
    }
}
