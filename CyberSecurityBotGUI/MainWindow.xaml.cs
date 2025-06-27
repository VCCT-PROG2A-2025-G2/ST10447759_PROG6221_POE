// MainWindow.xaml.cs with basic NLP routing and activity logging
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityBot.Core;
using CyberSecurityBotGUI;

namespace CyberSecurityBot
{
    public partial class MainWindow : Window
    {
        private readonly ChatBotEngine _bot = new ChatBotEngine();
        public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>();
        private TaskWindow _taskWindow;
        private readonly ObservableCollection<string> ActivityLog = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            ChatList.ItemsSource = Messages;
            ChatList.ItemTemplateSelector = new ChatTemplateSelector
            {
                UserMessageTemplate = (DataTemplate)Resources["UserMessageTemplate"],
                BotMessageTemplate = (DataTemplate)Resources["BotMessageTemplate"]
            };

            _taskWindow = new TaskWindow();

            if (_bot.ParsedTask != null)
            {
                _taskWindow.AddExternalTask(_bot.ParsedTask);
                _bot.ParsedTask = null;
            }

            Messages.Add(new ChatMessage
            {
                Text = "Welcome to the CyberSecurity Bot!",
                IsUser = false
            });
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputText.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            Messages.Add(new ChatMessage { Text = input, IsUser = true });
            string response = GetBotResponseWithIntent(input.ToLower());

            Messages.Add(new ChatMessage { Text = response, IsUser = false });
            ActivityLog.Add($"[{DateTime.Now:t}] Interacted: '{input}'");

            InputText.Text = string.Empty;
            ChatScroll.ScrollToEnd();
        }

        private string GetBotResponseWithIntent(string input)
        {
            if (input.Contains("task") || input.Contains("remind") || input.Contains("add"))
            {
                EnsureTaskWindowOpen();
                return "Opening Task Assistant...";
            }

            if (input.Contains("quiz") || input.Contains("test") || input.Contains("game"))
            {
                new QuizWindow().Show();
                return "Launching Quiz...";
            }

            if (input.Contains("log") || input.Contains("what have you done"))
            {
                new LogWindow().Show();
                return "Opening Activity Log...";
            }

            if (input.Contains("phishing")) return "Phishing Tip: Never click on unknown email links.";
            if (input.Contains("password")) return "Password Tip: Use a unique password for each account.";
            if (input.Contains("privacy")) return "Privacy Tip: Check your social media privacy settings regularly.";

            return _bot.GetResponse(input);
        }

        private void TaskBtn_Click(object sender, RoutedEventArgs e)
        {
            EnsureTaskWindowOpen();
        }

        private void EnsureTaskWindowOpen()
        {
            if (_taskWindow == null || !_taskWindow.IsLoaded && !_taskWindow.IsVisible)
            {
                _taskWindow = new TaskWindow();
            }

            if (!_taskWindow.IsVisible)
            {
                _taskWindow.Show();
            }

            _taskWindow.Activate();
        }

        private void QuizBtn_Click(object sender, RoutedEventArgs e) => new QuizWindow().Show();
        private void LogBtn_Click(object sender, RoutedEventArgs e) => new LogWindow().Show();
    }

    public class ChatMessage
    {
        public string Text { get; set; }
        public bool IsUser { get; set; }
    }

    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UserMessageTemplate { get; set; }
        public DataTemplate BotMessageTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var msg = item as ChatMessage;
            return msg?.IsUser == true ? UserMessageTemplate : BotMessageTemplate;
        }
    }
}