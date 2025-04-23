// Program.cs
/*
 * Jeron Okkers
 * ST10447759
 * PROG6221
 */
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CyberSecurityBot.Services;

namespace CyberSecurityBot
{
    /// <summary>
    /// Entry point for the CyberSecurityBot application.
    /// Configures dependency injection and starts the bot.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            // 1) Configure DI container (ensure Microsoft.Extensions.DependencyInjection is installed)
            var services = new ServiceCollection()
                .AddSingleton<IResponseService, InMemoryResponseService>()
                .AddSingleton<VoiceGreetingService>()
                .AddSingleton<InteractionService>()
                .AddSingleton<CyberBot>()
                .BuildServiceProvider();

            // 2) Run the bot
            var bot = services.GetRequiredService<CyberBot>();
            await bot.RunAsync();
        }
    }
}
//==================================================================================/
