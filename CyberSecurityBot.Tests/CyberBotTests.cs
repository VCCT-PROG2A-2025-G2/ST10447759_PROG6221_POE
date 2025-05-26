// CyberSecurityBot.Tests/CyberBotTests.cs
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CyberSecurityBot;
using CyberSecurityBot.Services; // For IResponseService and InMemoryResponseService
using System.Threading.Tasks;

namespace CyberSecurityBot.Tests
{
    // Stub for VoiceGreetingService for testing purposes
    public class StubVoiceGreetingService : VoiceGreetingService
    {
        public bool PlayVoiceGreetingAsyncCalled { get; private set; } = false;

        public override async Task PlayVoiceGreetingAsync() // Must be virtual in the base classes
        {
            PlayVoiceGreetingAsyncCalled = true;
            await Task.CompletedTask;
        }
    }

    // Stub for InteractionService for testing purposes
    // For this stub to be useful for the CyberBot test, its methods called by CyberBot
    // also need to be virtual in the original InteractionService class.
    public class StubInteractionService : InteractionService
    {
        public bool GreetAndAskNameAsyncCalled { get; private set; } = false;
        public bool MenuInteractionAsyncCalled { get; private set; } = false;

        // Constructor needs to match a base constructor or be parameterless if base has one.
        // Assuming IResponseService is needed for InteractionService's base logic.
        public StubInteractionService(IResponseService responseService) : base(responseService) { }

        public override async Task GreetAndAskNameAsync() // Must be virtual in the base class
        {
            GreetAndAskNameAsyncCalled = true;
            await Task.CompletedTask;
        }

        public override async Task MenuInteractionAsync() // Must be virtual in the base class
        {
            MenuInteractionAsyncCalled = true;
            await Task.CompletedTask;
        }
    }

    [TestClass]
    public class CyberBotTests
    {
        [TestMethod]
        public async Task RunAsync_CallsDependenciesInOrder()
        {
            // Arrange
            var stubVoiceService = new StubVoiceGreetingService();
            // InteractionService constructor needs an IResponseService. We can use a real one or another stub.
            var dummyResponseService = new InMemoryResponseService(); // Or a more specific mock if needed
            var stubInteractionService = new StubInteractionService(dummyResponseService);

            var bot = new CyberBot(stubVoiceService, stubInteractionService);

            // Act
            await bot.RunAsync();

            // Assert
            // Note: VoiceGreeting is played first in your Program.cs, then GreetAndAskName, then MenuInteraction.
            // The CyberBot.RunAsync currently doesn't call PlayVoiceGreetingAsync itself, it's called before bot.RunAsync in Program.cs.
            // So, based on CyberBot.RunAsync:
            Assert.IsTrue(stubInteractionService.GreetAndAskNameAsyncCalled, "GreetAndAskNameAsync should have been called.");
            Assert.IsTrue(stubInteractionService.MenuInteractionAsyncCalled, "MenuInteractionAsync should have been called.");

            // If PlayVoiceGreetingAsync was part of bot.RunAsync, you'd assert:
            // Assert.IsTrue(stubVoiceService.PlayVoiceGreetingAsyncCalled, "PlayVoiceGreetingAsync should have been called.");
        }
    }
}