// CyberSecurityBotGUI/TaskWindow.xaml.cs
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq; // Add this for .ToList() and other LINQ operations
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityBot.Core; // Ensure this is correct for TaskItem

namespace CyberSecurityBotGUI
{
    public partial class TaskWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();

        public TaskWindow()
        {
            InitializeComponent();
            TaskList.ItemsSource = Tasks;
            LoadTasks();
            this.Closing += TaskWindow_Closing;

        }

        public void AddExternalTask(TaskItem task)
        {
            Tasks.Add(task);
            ActivityLog.Add($"Task added via chatbot: '{task.Title}'");
            SaveTasks(); // Save after adding
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string description = TaskDescBox.Text.Trim();
            DateTime? reminderDate = ReminderDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newTask = new TaskItem
            {
                Title = title,
                Description = string.IsNullOrWhiteSpace(description) ? "Cybersecurity-related task" : description,
                ReminderDate = reminderDate,
                IsCompleted = false // New tasks are not completed by default
            };

            Tasks.Add(newTask);
            SaveTasks(); // Save after adding

            TaskTitleBox.Text = "";
            TaskDescBox.Text = "";
            ReminderDatePicker.SelectedDate = null;
            ActivityLog.Add($"Task added manually: '{newTask.Title}'");
        }

        private void TaskCompleted_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsCompleted = true; // Mark as completed
                SaveTasks(); // Save changes
                ActivityLog.Add($"Task marked as completed: '{task.Title}'");
            }
        }

        private void TaskCompleted_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TaskItem task)
            {
                task.IsCompleted = false; // Mark as not completed
                SaveTasks(); // Save changes
                ActivityLog.Add($"Task marked as incomplete: '{task.Title}'");
            }
        }

        private void TaskWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveTasks();
        }

        public void SaveTasks()
        {
            try
            {
                string json = JsonSerializer.Serialize(Tasks.ToList()); // Serialize the ObservableCollection to a list
                File.WriteAllText("tasks.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tasks: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadTasks()
        {
            if (!File.Exists("tasks.json"))
            {
                Tasks.Clear();
                return;
            }

            try
            {
                string json = File.ReadAllText("tasks.json");
                var loadedTasks = JsonSerializer.Deserialize<List<TaskItem>>(json);
                Tasks.Clear();
                foreach (var task in loadedTasks)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Tasks.Clear();
            }
        }

        private void DeleteSelectedTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList is ListBox listBox && listBox.SelectedItem is TaskItem selected)
            {
                Tasks.Remove(selected);
                SaveTasks();
                ActivityLog.Add($"Task deleted: '{selected.Title}'");
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "No Task Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}