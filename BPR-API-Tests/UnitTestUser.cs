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
        // Z - Zero
        // O - One
        // M - Many
        // B - Boundary Behaviors
        // I - Interface Definition
        // E - Exceptional Behavior
        // S - Simple Scenarios, Simple Solutions
        // Z - Zero
        [TestMethod]
        public void TestLogin()
        {
            // Arrange
            var userController = new UserController(null);

            // Act
            userController.Login(new UserDTO { Username = "", Password = "" });

            // Assert
        }
    }
}
