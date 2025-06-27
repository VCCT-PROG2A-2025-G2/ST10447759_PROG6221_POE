# 🤖 CyberSecurity Awareness Bot – Part 3

## 🚀 Project Overview

This final version (Part 3) transforms the console-based Cybersecurity Bot into a full **WPF GUI application** with the following enhancements:

- **Graphical Interface (WPF)**:
  - Interactive chat window
  - Dedicated windows for tasks, quizzes, and logs

- **Task Manager with Reminders**:
  - Add tasks with title, description, and a future reminder date
  - Automatically alerts the user when reminders are due
  - Mark tasks as complete or delete them

- **Cybersecurity Quiz (Game)**:
  - Multiple choice and true/false formats
  - Immediate feedback after each question
  - Score tracking and personalized end message

- **NLP Simulation**:
  - Detects keywords in user phrases like “remind me to…”, “quiz me”, or “what have you done?”
  - Recognizes cybersecurity topics like "phishing", "password", or "privacy"

- **Activity Log**:
  - Tracks recent actions like added tasks, completed quiz sessions, and recognized keywords
  - Option to view last 5–10 activities with a “Show More” feature

> 🎥 [Watch Part 3 Demo on YouTube](https://youtu.be/0GYE0yjPHJA)

---

## 📂 Repository Structure

/CyberSecurityBotGUI (WPF GUI Project)  
├── MainWindow.xaml.cs — Chat logic & NLP routing  
├── TaskWindow.xaml.cs — Task management with reminders  
├── QuizWindow.xaml.cs — Cybersecurity quiz  
├── LogWindow.xaml.cs — Recent actions  
├── App.xaml — WPF Application setup  
└── packages.config — NuGet references

/CyberSecurityBot.Core  

.github/  
└── workflows/  
    └── main.yml — GitHub Actions CI workflow

README.md — This file  
CyberSecurityBot.sln — Visual Studio solution file

---

## ⚙️ Prerequisites

* .NET Framework 4.8 Developer Pack  
* Windows OS  
* Visual Studio 2019 or 2022  
* NuGet CLI (optional for CLI builds)

---

## 🛠️ Setup & Run

1. **Clone the Repository**  
```bash
git clone https://github.com/VCCT-PROG2A-2025-G2/ST10447759_PROG6221_POE.git  
cd ST10447759_PROG6221_POE
