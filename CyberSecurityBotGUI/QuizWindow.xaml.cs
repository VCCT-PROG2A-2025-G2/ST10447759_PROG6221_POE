// Enhanced QuizWindow.xaml.cs with Score Tracking and Feedback
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CyberSecurityBot.Core;

namespace CyberSecurityBotGUI
{
    public partial class QuizWindow : Window
    {
        private readonly List<Question> questions;
        private int currentQuestionIndex = 0;
        private int selectedOptionIndex = -1;
        private int score = 0;

        public QuizWindow()
        {
            InitializeComponent();
            questions = QuizBank.Questions;
            DisplayQuestion();
        }

        private void DisplayQuestion()
        {
            if (currentQuestionIndex >= questions.Count)
            {
                ShowFinalScore();
                return;
            }

            var question = questions[currentQuestionIndex];
            QuestionText.Text = question.Text;
            QuestionNumberText.Text = $"Question {currentQuestionIndex + 1} of {questions.Count}";
            QuizProgress.Value = currentQuestionIndex + 1;

            OptionsPanel.Children.Clear();
            selectedOptionIndex = -1;
            FeedbackText.Text = "";

            for (int i = 0; i < question.Options.Count; i++)
            {
                var optionButton = new RadioButton
                {
                    Content = question.Options[i],
                    GroupName = "OptionsGroup",
                    Margin = new Thickness(0, 10, 0, 0),
                    FontWeight = FontWeights.SemiBold,
                    Tag = i
                };

                optionButton.Checked += (s, e) =>
                {
                    selectedOptionIndex = (int)((RadioButton)s).Tag;
                };

                OptionsPanel.Children.Add(optionButton);
            }
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOptionIndex == -1)
            {
                FeedbackText.Text = "Please select an option.";
                return;
            }

            var question = questions[currentQuestionIndex];
            if (selectedOptionIndex == question.CorrectIndex)
            {
                FeedbackText.Text = "Correct!";
                score++;
            }
            else
            {
                FeedbackText.Text = $"Incorrect. Correct answer: {question.Options[question.CorrectIndex]}";
            }

            currentQuestionIndex++;
            await Task.Delay(1500);
            DisplayQuestion();
        }

        private void ShowFinalScore()
        {
            string message = $"You scored {score} out of {questions.Count}.\n";

            if (score >= questions.Count * 0.8)
                message += "Excellent! You're a cybersecurity pro!";
            else if (score >= questions.Count * 0.5)
                message += "Good effort! Keep learning to stay safe online.";
            else
                message += "Keep practicing to improve your cybersecurity knowledge.";

            MessageBox.Show(message, "Quiz Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}