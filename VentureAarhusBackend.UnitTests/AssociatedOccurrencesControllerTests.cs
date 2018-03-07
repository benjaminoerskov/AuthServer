using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DetSkerIAarhusBackend.API.Controllers;
using DetSkerIAarhusBackend.API.DTOs;
using DetSkerIAarhusBackend.API.Entities;
using DetSkerIAarhusBackend.API.Models;
using DetSkerIAarhusBackend.API.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DetSkerIAarhusBackend.UnitTests
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
        private List<AssociatedOccurrences> _occurrencesList = null;



        [TestInitialize]
        public void SetUp()
        {
            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase();
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

            _occurrencesList = new List<AssociatedOccurrences>();

            //Setup methods:
            mockUserManager.Setup(x => x.GetUserAsync(_controller.User)).Returns(Task.FromResult<ApplicationUser>(_testUser));
            _mockRepo.Setup(x => x.GetAll()).Returns(_occurrencesList.AsQueryable());

        }

        [TestMethod]
        public void TestPostAssociatedOccurrencePositive()
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

            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 30,
                ApplicationUserId = _testUser.Id,
                Type = "like",
                OccurrenceId = 1
            };
            _occurrencesList.Add(associated1);

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
        public void TestPostAssociatedOccurrenceNegative()
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


            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 30,
                ApplicationUserId = _testUser.Id,
                Type = "like",
                OccurrenceId = 20
            };
            _occurrencesList.Add(associated1);

            _mockMapper.Setup(x =>
                x.Map<AssociatedOccurrencesDTO>(It.IsAny<AssociatedOccurrences>())).Returns(AssociatedDTO);


            var response = _controller.PostAssociatedOccurrence(param).Result;
            var okResult = response.Should().BeOfType<BadRequestObjectResult>().Subject;
        }

        [TestMethod]
        public void TestDeleteAssociatedOccurrencePositive()
        {
            var param = new IdParam
            {
               Id = 20
            };

            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = _testUser.Id,
                Type = "like",
                OccurrenceId = 1
            };
            _occurrencesList.Add(associated1);

            var response = _controller.DeleteAssociatedOccurrence(param).Result;
            var okResult = response.Should().BeOfType<NoContentResult>().Subject;
            
        }

        [TestMethod]
        public void TestDeleteAssociatedOccurrenceNegative()
        {
            var param = new IdParam
            {
                Id = 21
            };

            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = _testUser.Id,
                Type = "like",
                OccurrenceId = 1
            };
            _occurrencesList.Add(associated1);

            var response = _controller.DeleteAssociatedOccurrence(param).Result;
            var okResult = response.Should().BeOfType<NotFoundObjectResult>().Subject;
            var returnString = okResult.Value.Should().BeAssignableTo<string>().Subject;
        }

        [TestMethod]
        public void TestGetUserTypesPositive()
        {
            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = _testUser.Id,
                Type = "like",
                OccurrenceId = 1
            };
            var associated2 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = _testUser.Id,
                Type = "heart",
                OccurrenceId = 1
            };
            var associated3 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = _testUser.Id,
                Type = "follow",
                OccurrenceId = 1
            };
            _occurrencesList.Add(associated1);
            _occurrencesList.Add(associated2);
            _occurrencesList.Add(associated3);

            var response = _controller.GetUserTypes().Result;
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            var returnList = okResult.Value.Should().BeAssignableTo<IEnumerable<string>>().Subject.ToList();
            returnList[0].Should().Be("like");
            returnList[1].Should().Be("heart");
            returnList[2].Should().Be("follow");
        }

        [TestMethod]
        public void TestGetUserTypesNegative()
        {
            var falseId = "falseTestId";
            var associated1 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = falseId,
                Type = "like",
                OccurrenceId = 1
            };
            var associated2 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = falseId,
                Type = "heart",
                OccurrenceId = 1
            };
            var associated3 = new AssociatedOccurrences
            {
                ApplicationUser = _testUser,
                Id = 20,
                ApplicationUserId = falseId,
                Type = "follow",
                OccurrenceId = 1
            };
            _occurrencesList.Add(associated1);
            _occurrencesList.Add(associated2);
            _occurrencesList.Add(associated3);

            var response = _controller.GetUserTypes().Result;
            var okResult = response.Should().BeOfType<NotFoundObjectResult>().Subject;
            var returnString = okResult.Value.Should().BeAssignableTo<string>().Subject;
            returnString.Should().Be("User doesnt have any lists");
        }

        [TestCleanup]
        public void CleanUp()
        {

        }

        
    }
}
