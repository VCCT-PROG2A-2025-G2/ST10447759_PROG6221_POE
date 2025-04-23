// InteractionService.cs
/*
 * Jeron Okkers
 * ST10447759
 * PROG6221 - Part 1
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
        private readonly List<string> _topics;
        private string _userName;

        public InteractionService(IResponseService responseService)
        {
            _responseService = responseService;
            _topics = responseService.GetAvailableTopics().OrderBy(t => t).ToList();
            _topics.Add("exit");
        }


        /// <summary>
        /// Prompts the user for their name and stores it locally.
        /// </summary>
        public async Task GreetAndAskNameAsync()
        {
            await ConsoleUI.TypeWriteAsync("Good day! What's your name?");
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
                await ConsoleUI.TypeWriteAsync("Oops, I missed that. Please type your name:");
            }
            await ConsoleUI.TypeWriteAsync($"Nice to meet you, {_userName}! Let's get started.\n");
        }

        /// <summary>
        /// Displays a menu of topics and processes user selections until exit.
        /// </summary>
        public async Task MenuInteractionAsync()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Select a topic by number, or type a keyword (e.g. 'phishing'), or 'exit':");
                for (int i = 0; i < _topics.Count; i++)
                    Console.WriteLine($"  {i + 1}. {_topics[i]}");
                Console.ResetColor();

                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    await ConsoleUI.TypeWriteAsync("Please enter a topic or number.");
                    continue;
                }

                // Exit
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    (int.TryParse(input, out int ex) && ex == _topics.Count))
                {
                    await ConsoleUI.TypeWriteAsync("Goodbye! Stay secure online.");
                    break;
                }

                string reply;
                // Number selection
                if (int.TryParse(input, out int sel) && sel >= 1 && sel < _topics.Count)
                {
                    reply = _responseService.GetRandomResponse(_topics[sel - 1]);
                }
                else
                {
                    // Keyword detection
                    var found = _topics
                        .Where(t => t != "exit")
                        .FirstOrDefault(t => input.IndexOf(t, StringComparison.OrdinalIgnoreCase) >= 0);
                    reply = _responseService.GetRandomResponse(found ?? "");

                }

                await ConsoleUI.TypeWriteAsync(reply);
                Console.WriteLine();
            }
        }
    }
}

//==================================================================================/
