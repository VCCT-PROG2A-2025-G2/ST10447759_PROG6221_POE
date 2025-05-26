## 🚀 Project Overview

This console application is a **Cybersecurity Awareness Bot** developed in C#. 

Now updated for **Part 2**, it includes:

- Dynamic responses using **keyword recognition** (e.g., "password", "scam", "privacy").
- A **memory feature** that recalls user preferences or inputs.
- Basic **sentiment detection** (e.g., detects "worried" or "curious" tones).
- Support for **natural conversational flow** and follow-up questions.
- **Unit tests** using MSTest to validate core chatbot logic.

This part builds on Part 1 with:

1. Enhanced use of collections (`List`, `Dictionary`)
2. Introduction of **delegates** to handle response strategies
3. Use of basic logic to simulate **natural language processing**
4. More sophisticated input validation and fallback handling


---

## 📂 Repository Structure

```text
/CyberSecurityBot
├── Program.cs
├── CyberBot.cs
├── InteractionService.cs
├── ConsoleUI.cs
├── VoiceGreetingService.cs
├── IResponseService.cs
├── ResponseData.cs
└── InMemoryResponseService.cs
```

**Note:** All chatbot Q&A data is stored **in-memory** in `ResponseData.cs`—no external JSON file required.

---

## ⚙️ Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- Windows with audio support (for voice greeting)
- (Optional) Visual Studio 2022 or [VS Code](https://code.visualstudio.com/) with C# extension

---

## 🛠️ Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/<your-username>/CyberSecurityBot.git
   cd CyberSecurityBot
   ```
2. **Restore NuGet packages** (DI package is required)
   ```bash
   dotnet restore
   ```
3. **Build the solution**
   ```bash
   dotnet build
   ```
4. **Run the application**
   ```bash
   dotnet run --project ./CyberSecurityBot.csproj
   ```

---

## 🎮 How to Use

1. Launch the app – you’ll hear a friendly voice greeting if `Greeting.wav` is present.
2. ASCII art logo appears, followed by a prompt: *“Good day! What’s your name?”*
3. After entering your name, you’ll see a numbered menu of cybersecurity topics.
4. Enter the number corresponding to your topic to get practical tips.
5. Type **exit** or select the last menu item to quit gracefully.

---

## ✅ Continuous Integration (CI)

This repo is configured with **GitHub Actions** to:

- **Build** on every push
- **Run** a simple syntax check (no unit tests yet)

> **Hint:** Add your own tests and update the CI workflow at `.github/workflows/ci.yml`.

---

## 📈 Version Control & Commits

A minimum of **three meaningful commits** is required:

1. **Initial commit:** Project scaffolding & DI setup
2. **Greeting & UI:** Voice greeting, ASCII art, and typing effect
3. **Core logic:** In-memory responses, menu navigation, and input validation

---

## 🙌 Next Steps (Part 2)

- **Dynamic responses** based on keyword detection
- **Memory & recall** (remember user preferences)
- **Sentiment detection** to adjust tone
- **Unit testing** for core services

Stay secure online—and keep coding! 🔒👨‍💻

