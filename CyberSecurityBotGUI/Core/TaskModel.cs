// CyberSecurityBotGUI/Core/TaskModel.cs
using System;

public class TaskModel // This seems to be an old TaskModel, use TaskItem as per ChatBotEngine
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ReminderDate { get; set; }

    public override string ToString()
    {
        return $"{Title} - {(ReminderDate.HasValue ? ReminderDate.Value.ToShortDateString() : "No reminder")}";
    }
}