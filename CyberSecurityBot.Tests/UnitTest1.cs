using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberSecurityBot.Tests
{
    [TestClass]
    public class CyberBotTests
    {
        [TestMethod]
        public void RespondToKeyword_ShouldReturnPasswordTip_WhenInputContainsPassword()
        {
            // Arrange
            var bot = new CyberBot();
            string input = "Tell me about password safety";

            // Act
            string response = bot.RespondToUser(input);

            // Assert
            Assert.IsTrue(response.Contains("password", StringComparison.OrdinalIgnoreCase));
        }
    }

}
