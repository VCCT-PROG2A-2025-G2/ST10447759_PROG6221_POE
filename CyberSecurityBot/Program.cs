// Program.cs
/*
 * Jeron Okkers
 * ST10447759
 * PROG6221
 */
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
            // 1) Create your services directly
            var responseService = new Services.InMemoryResponseService();
            var voiceService = new VoiceGreetingService();
            var interactionService = new InteractionService(responseService);

            // 2) Wire up the bot
            var bot = new CyberBot(voiceService, interactionService);

            // 3) Run it
            await bot.RunAsync();
        }
    }
}

//==================================================================================/
