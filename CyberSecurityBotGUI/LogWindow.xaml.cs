// LogWindow.xaml.cs
using System.Linq;
using System.Windows;
using CyberSecurityBot.Core;

namespace CyberSecurityBot
{
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
            LoadLogs();
        }

        private void LoadLogs()
        {
            var displayLogs = ActivityLog.Logs.Select(log =>
            {
                // Extract timestamp and content
                int idx = log.IndexOf(": ");
                string time = log.Substring(0, idx);
                string content = log.Substring(idx + 2);

                // Try highlighting modules or exercises
                string highlight = "";
                if (content.Contains("Completed Module:"))
                    highlight = content.Substring(content.IndexOf("Completed Module:")).Trim();
                else if (content.Contains("Practice Exercise:"))
                    highlight = content.Substring(content.IndexOf("Practice Exercise:")).Trim();

                return new
                {
                    Date = time,
                    Description = string.IsNullOrEmpty(highlight) ? content.Trim() : content.Replace(highlight, "").Trim(),
                    Highlight = highlight
                };
            }).ToList();

            LogList.ItemsSource = displayLogs;
        }
    }
}