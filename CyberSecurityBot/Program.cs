/*
 * Jeron Okkers
 * ST10447759
 * PROG6221
 */ 
// Program.cs
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    /// <summary>
    /// Entry point for the CyberSecurityBot application.
    /// Sets up the console and starts the bot.
    /// </summary>
    class Program
    {

        /// Asynchronous Main method to support async startup logic.
        static async Task Main(string[] args)
        {

            // Create and run the chatbot
            var bot = new CyberBot();
            await bot.RunAsync();
        }
    }
}