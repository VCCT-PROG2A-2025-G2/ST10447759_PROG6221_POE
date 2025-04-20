// CyberBot.cs
/*
 * Jeron Okkers
 * ST10447759
 * PROG6221
 */ 

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    /// <summary>
    // Manages chatbot logic, responses, and services
    /// </summary>
    public class CyberBot
    {
        public string UserName { get; private set; }
        private readonly Dictionary<string, List<string>> _responses;
        private readonly VoiceGreetingService _voiceService;
        private readonly InteractionService _interactionService;

        public CyberBot()
        {

            // Reference: GeeksforGeeks. “C# Dictionary With Examples.” [Online]. Available: https://www.geeksforgeeks.org/c-sharp-dictionary-with-examples/ [Accessed: 19-Apr-2025].
            // Initialize predefined responses
            _responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "how are you", new List<string> { "I’m doing great, thanks for asking!", "Feeling secure and ready to help!" } },
                { "what's your purpose", new List<string> { "I’m here to help you stay safe online.", "My purpose is to guide you on cybersecurity best practices." } },
                { "what can i ask you about", new List<string> { "You can ask about password safety, phishing, safe browsing, and more.", "Feel free to ask me about password safety, detecting scams, and securing your privacy." } },
                { "password safety", new List<string> { "Use strong, unique passwords for each account and avoid personal info.", "Try using a password manager to store complex passwords securely." } },
                { "phishing", new List<string> { "Don’t click on suspicious links or provide personal info over email.", "Always verify the sender's identity in emails asking for sensitive data." } },
                { "safe browsing", new List<string> { "Use updated browsers and HTTPS websites only.", "Avoid downloading from unknown sources and use ad blockers." } }
            };

            _voiceService = new VoiceGreetingService();
            _interactionService = new InteractionService(this);
        }

        /// Runs the chatbot by invoking each service in order.
        public async Task RunAsync()
        {
            ConsoleUI.DisplayAsciiArt();
            await _voiceService.PlayVoiceGreetingAsync();
            await _interactionService.GreetAndAskNameAsync();
            await _interactionService.MenuInteractionAsync();
        }

        /// Retrieves a random response for a given key.
        public string GetRandomResponse(string key)
        {
            if (_responses.TryGetValue(key, out var replies))
            {
                var rnd = new Random();
                return replies[rnd.Next(replies.Count)];
            }
            return "Sorry, I don't have a response for that.";
        }

        /// Sets the user's name for personalized responses.
        public void SetUserName(string name) => UserName = name;
    }
}