/*
 * CyberSecurityBot WPF GUI Implementation
 * Features:
 * - Task Assistant with reminders
 * - NLP Simulation with keyword detection
 * - Quiz Game (10 cybersecurity questions)
 * - Activity Log
 * - Full integration of Part 1 & 2 logic
 */

// ChatBotEngine.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CyberSecurityBot.Core
{
    public class ChatBotEngine
    {
        public string UserName { get; set; }
        public string FavoriteTopic { get; set; }
        public TaskItem ParsedTask { get; set; }


        // Updated fields to be readonly as they are only initialized and not modified elsewhere in the code.
        private readonly List<string> phishingTips = new List<string>() {
            "Don’t click on links in unsolicited emails.",
            "Verify sender addresses before responding.",
            "Use spam filters and report phishing attempts."
        };

        // Updated the initialization of keywordResponses to use explicit type declaration to ensure compatibility with C# 7.3.
        private readonly Dictionary<string, string> keywordResponses = new Dictionary<string, string>() {
            { "password", "Use complex passwords and never reuse them." },
            { "scam", "Always verify requests for money or sensitive info." },
            { "privacy", "Regularly review your app permissions and privacy settings." }
        };



        public string GetResponse(string input)
        {
            input = input.ToLower();
            if (input.Contains("how are you"))
                return "I'm here to help you stay safe online!";

            if (input.Contains("purpose"))
                return "I'm your CyberSecurity Assistant, here to teach you about online safety.";

            if (input.Contains("phishing"))
            {
                var rand = new Random();
                return phishingTips[rand.Next(phishingTips.Count)];
            }

            foreach (var key in keywordResponses.Keys)
            {
                if (input.Contains(key))
                    return keywordResponses[key];
            }

            if (input.Contains("worried"))
                return "It’s okay to feel worried. Let's learn how to stay secure together.";

            if (input.Contains("task") || input.Contains("remind"))
                return "Would you like me to open the Task Assistant?";

            if (input.Contains("quiz"))
                return "Launching the Cybersecurity Quiz now!";

            if (input.Contains("log") || input.Contains("done"))
                return "Opening the Activity Log for your recent actions.";

            if (input.StartsWith("add task"))
            {
                var parts = input.Split('-', (char)2);
                if (parts.Length == 2)
                {
                    ParsedTask = new TaskItem
                    {
                        Title = parts[1].Trim(),
                        Description = "Cybersecurity-related task" // Optional improvement: parse better
                    };
                    return $"Task added: \"{ParsedTask.Title}\". Would you like a reminder?";
                }
            }


            return "I didn’t quite understand that. Could you rephrase?";
        }
    }

    public static class ActivityLog
    {
        // Updated the initialization of Logs to use explicit type declaration to ensure compatibility with C# 7.3.
        public static List<string> Logs { get; } = new List<string>();
        public static void Add(string entry)
        {
            Logs.Insert(0, $"{DateTime.Now}: {entry}");
            if (Logs.Count > 10) Logs.RemoveAt(Logs.Count - 1);
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; } // <--- ADD THIS LINE

        public string ReminderDisplay => ReminderDate.HasValue ? $"Reminder: {ReminderDate.Value:MMM dd, yyyy}" : string.Empty;

        // Update ToString() to reflect completion status
        public override string ToString()
        {
            string completionStatus = IsCompleted ? "[COMPLETED] " : "";
            return $"{completionStatus}{Title} - {Description} (Reminder: {ReminderDate?.ToShortDateString() ?? "None"})";
        }
    }


    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
    }

    public static class QuizBank
    {
        public static List<Question> Questions = new List<Question> {
            new Question {
                Text = "What should you do if you receive an email asking for your password?",
                Options = new List<string> {"Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it"},
                CorrectIndex = 2
            },
            new Question {
                Text = "Which of the following is a strong password?",
                Options = new List<string> {"12345678", "qwerty", "P@55w0rd$123", "yourname1990"},
                CorrectIndex = 2
            },
            new Question {
                Text = "True or False: It's safe to click on links from unknown sources.",
                Options = new List<string> {"True", "False"},
                CorrectIndex = 1
            },
            new Question {
                Text = "What is two-factor authentication?",
                Options = new List<string> {
                    "A method requiring two passwords",
                    "A security method using two layers of verification",
                    "A fingerprint scanner only",
                    "An email check only"
                },
                CorrectIndex = 1
            },
            new Question {
                Text = "Which one is a phishing sign?",
                Options = new List<string> {
                    "Perfect grammar and spelling",
                    "Personalized message",
                    "Urgency to click a link or open an attachment",
                    "From a known sender"
                },
                CorrectIndex = 2
            },
            new Question {
                Text = "What should you regularly update to stay secure?",
                Options = new List<string> {
                    "Your browser theme",
                    "Your contact list",
                    "Your social media bios",
                    "Your software and operating system"
                },
                CorrectIndex = 3
            },
            new Question {
                Text = "What is malware?",
                Options = new List<string> {
                    "A secure program",
                    "A friendly app",
                    "Malicious software intended to harm your device",
                    "A browser extension"
                },
                CorrectIndex = 2
            },
            new Question {
                Text = "What does HTTPS indicate?",
                Options = new List<string> {
                    "A secure website",
                    "A hacked website",
                    "A fast website",
                    "A hidden website"
                },
                CorrectIndex = 0
            },
            new Question {
                Text = "True or False: It's okay to reuse your password across sites.",
                Options = new List<string> {"True", "False"},
                CorrectIndex = 1
            },
            new Question {
                Text = "What's the best action if you're unsure about a suspicious email?",
                Options = new List<string> {
                    "Click it to see what happens",
                    "Forward it to your friends",
                    "Report it to IT/security",
                    "Ignore it completely"
                },
                CorrectIndex = 2
            }
        };
    }
}
