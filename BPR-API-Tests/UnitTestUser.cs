using BPR_API.Controllers;
using BPR_API.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPR_API_Tests
{
    [TestClass]
    public class UnitTestUser
    {
        [TestMethod]
        public void TestRegisterLoginDelete()
        {
            // Arrange
            var userController = new UserController(null);
            var username = "TestUser5803";
            var password = "58037493";

            // Act
            var registerResponse = userController.Register(new UserDTO { Username = username, Password = password });
            var loginResponse = userController.Login(new UserDTO { Username = username, Password = password });
            var deleteResponse = userController.DeleteUser(new UserDTO { Username = username });

            // Assert
            Assert.IsTrue(registerResponse.IsCompletedSuccessfully);
            Assert.IsTrue(loginResponse.IsCompletedSuccessfully);
            Assert.IsTrue(deleteResponse.IsCompletedSuccessfully);
        }
    }
}
