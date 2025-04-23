// InteractionService.cs
/*
 * Jeron Okkers
 * ST10447759
 * PROG6221
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberSecurityBot.Services;

namespace CyberSecurityBot
{
    /// <summary>
    /// Handles user interaction: name greeting and menu-based Q&A.
    /// </summary>
    public class InteractionService
    {
        private readonly IResponseService _responseService;
        private readonly List<string> _options;
        private string _userName;

        public InteractionService(IResponseService responseService)
        {
            _responseService = responseService;
            _options = responseService.GetAvailableTopics().ToList();
            _options.Add("exit");
        }

        /// <summary>
        /// Prompts the user for their name and stores it locally.
        /// </summary>
        public async Task GreetAndAskNameAsync()
        {
            await ConsoleUI.TypeWriteAsync("Good day! What’s your name?");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                var input = Console.ReadLine()?.Trim();
                Console.ResetColor();

                if (!string.IsNullOrEmpty(input))
                {
                    _userName = input;
                    break;
                }
                await ConsoleUI.TypeWriteAsync("Oops, I missed that. Could you please type your name?");
            }
            await ConsoleUI.TypeWriteAsync($"Nice to meet you, {_userName}! Let's explore how to stay safe online.\n");
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
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    int.TryParse(input, out int sel0) && sel0 == _options.Count)
                {
                    await ConsoleUI.TypeWriteAsync("Goodbye! Stay secure online.");
                    break;
                }

                if (int.TryParse(input, out int sel) && sel >= 1 && sel < _options.Count)
                {
                    var key = _options[sel - 1];
                    var reply = _responseService.GetRandomResponse(key);
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
//================================================================================================================================================================//
