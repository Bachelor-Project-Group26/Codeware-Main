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
                Username = "SecondUser",
                Password = "Test"
            });
            Assert.AreEqual(200, registerResult.StatusCode);
            
        }
    }
}
