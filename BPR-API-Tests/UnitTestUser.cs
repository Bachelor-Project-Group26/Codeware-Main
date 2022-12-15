using BPR_API.Controllers;
using BPR_API.APIModels;
using BPR_API.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BPR_API.DataAccess;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using System.Security.Claims;

namespace BPR_API_Tests
{
    [TestClass]
    public class UnitTestUser
    {
        private readonly IConfiguration _configuration;
        private readonly UserController controller;
        private readonly Mock<DatabaseContext> _dbContext;
        private readonly Mock<DbSet<UserDetails>> _dbSetDetailsMock;
        private readonly Mock<DbSet<UserPassword>> _dbSetPasswordMock;


        public UnitTestUser()
        {
            _dbContext = new Mock<DatabaseContext>();
            _dbSetDetailsMock = new Mock<DbSet<UserDetails>>();
            _dbSetPasswordMock = new Mock<DbSet<UserPassword>>();
            _configuration = InitConfiguration();

            var userDetails = new UserDetails()
            {
                Id = 1,
                Username = "FirstUser",
                SecurityLevel = 1,
                FirstName = "First",
                LastName = "User",
                Email = "firstuser@viauc.dk",
                Country = "Denmark",
                Bio = "I am the first user in this app!",
                ProfilePicture = null,
                Birthday = new DateTime(2000, 1, 1)
            };
            var userDetailsList = new List<UserDetails> { userDetails }.AsQueryable();

            var userPassword = new UserPassword("", "", "") { Id = 1, Username = "FirstUser", Hash = "", Salt = "" };
            var userPasswordList = new List<UserPassword> { userPassword }.AsQueryable();

            _dbSetDetailsMock.As<IQueryable<UserDetails>>().Setup(m => m.Provider).Returns(userDetailsList.Provider);
            _dbSetDetailsMock.As<IQueryable<UserDetails>>().Setup(m => m.Expression).Returns(userDetailsList.Expression);
            _dbSetDetailsMock.As<IQueryable<UserDetails>>().Setup(m => m.ElementType).Returns(userDetailsList.ElementType);
            _dbSetDetailsMock.As<IQueryable<UserDetails>>().Setup(m => m.GetEnumerator()).Returns(userDetailsList.GetEnumerator());
            _dbContext.Setup(c => c.UserDetails).Returns(_dbSetDetailsMock.Object);

            _dbSetPasswordMock.As<IQueryable<UserPassword>>().Setup(m => m.Provider).Returns(userPasswordList.Provider);
            _dbSetPasswordMock.As<IQueryable<UserPassword>>().Setup(m => m.Expression).Returns(userPasswordList.Expression);
            _dbSetPasswordMock.As<IQueryable<UserPassword>>().Setup(m => m.ElementType).Returns(userPasswordList.ElementType);
            _dbSetPasswordMock.As<IQueryable<UserPassword>>().Setup(m => m.GetEnumerator()).Returns(userPasswordList.GetEnumerator());
            _dbContext.Setup(c => c.UserPasswords).Returns(_dbSetPasswordMock.Object);

            controller = new UserController(_configuration);
            controller.addContext(_dbContext.Object);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();
            return config;
        }

        [TestMethod]
        public void TestRegister()
        {
            var registerResult = controller.Register(new UserDTO()
            {
                Username = "NewUser",
                Password = "Test"
            });
            Assert.AreEqual(200, registerResult.StatusCode);
            
        }
    }
}
