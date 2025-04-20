// InteractionService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    /// <summary>
    /// Handles user interaction: name greeting and menu-based Q&A.
    /// </summary>
    public class InteractionService
    {
        private readonly CyberBot _bot;
        private readonly List<string> _options = new List<string>()
        {
           "How are you?",
           "What's your purpose?",
           "What can I ask you about?",
           "Password safety",
           "Phishing",
           "Safe browsing",
           "exit"
        };

    public InteractionService(CyberBot bot) => _bot = bot;

    /// <summary>
    /// Prompts the user for their name and stores it in the bot.
    /// </summary>
    public async Task GreetAndAskNameAsync()
    {
        await ConsoleUI.TypeWriteAsync("Hello! I’m your Cybersecurity Defender Bot.");
        await ConsoleUI.TypeWriteAsync("What’s your name?");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var input = Console.ReadLine()?.Trim();
            Console.ResetColor();

            if (!string.IsNullOrEmpty(input))
            {
                _bot.SetUserName(input);
                break;
            }
            await ConsoleUI.TypeWriteAsync("Oops, I missed that. Could you please type your name?");
        }

        await ConsoleUI.TypeWriteAsync($"Nice to meet you, {_bot.UserName}! Let's explore how to stay safe online.\n");
    }

    /// <summary>
    /// Displays a menu of topics and processes user selections until exit.
    /// </summary>
    public async Task MenuInteractionAsync()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Choose a topic by number (or type 'exit' to quit):");
            for (int i = 0; i < _options.Count; i++)
                Console.WriteLine($"  {i + 1}. {_options[i]}");
            Console.ResetColor();

            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
            {
                await ConsoleUI.TypeWriteAsync("Please enter a number.");
                continue;
            }
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) || int.TryParse(input, out int sele) && sele >= 1 && sele <= _options.Count)
            {
                await ConsoleUI.TypeWriteAsync("Goodbye! Stay secure online.");
                break;
            }

            if (int.TryParse(input, out int sel) && sel >= 1 && sel <= _options.Count)
            {
                var key = _options[sel - 1]
                    .ToLowerInvariant()
                    .Replace("?", "")
                    .Trim();

                // Fetch and display a response
                var reply = _bot.GetRandomResponse(key);
                await ConsoleUI.TypeWriteAsync(reply);
            }
            else
            {
                await ConsoleUI.TypeWriteAsync("Invalid selection. Please choose a valid number.");
            }
            Console.WriteLine();
        }
    }
}
}
