// Program.cs
/*
 * Jeron Okkers
 * ST10447759
 * PROG6221
 */
using CyberSecurityBot.Services;
using System;
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    /// <summary>
    /// Entry point for the CyberSecurityBot application.
    /// Instantiates services manually without dependency injection.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            // Instantiate services
            var responseService = new InMemoryResponseService();
            var voiceService = new VoiceGreetingService();
            var interactionService = new InteractionService(responseService);

            // Display header and run the bot
            ConsoleUI.DisplayAsciiArt();
            await voiceService.PlayVoiceGreetingAsync();
            var bot = new CyberBot(voiceService, interactionService);
            await bot.RunAsync();
        }
    }
}

//==================================================================================/
