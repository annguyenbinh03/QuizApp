using GalaSoft.MvvmLight.Messaging;
using QuizApp.Commands;
using QuizApp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        string filePath;
        List<string> questions;
        List<string> answers;
        List<string> options;
        int QuestionNumber;
        int Score;

        private string question;

        public string Question
        {
            get { return question; }
            set { question = value; OnPropertyChanged(nameof(Question)); }
        }

        private string option1;

        public string Option1
        {
            get { return option1; }
            set { option1 = value; OnPropertyChanged(nameof(Option1)); }
        }

        private string option2;

        public string Option2
        {
            get { return option2; }
            set { option2 = value; OnPropertyChanged(nameof(Option2)); }
        }

        private string option3;

        public string Option3
        {
            get { return option3; }
            set { option3 = value; OnPropertyChanged(nameof(Option3)); }
        }

        private string option4;

        public string Option4
        {
            get { return option4; }
            set { option4 = value; OnPropertyChanged(nameof(Option4)); }
        }

        private bool answerButtonEnable;

        public bool AnswerButtonEnable
        {
            get { return answerButtonEnable; }
            set { answerButtonEnable = value; OnPropertyChanged(nameof(AnswerButtonEnable)); }
        }

        private string button1Selected;

        public string Button1Selected
        {
            get { return button1Selected; }
            set { button1Selected = value; OnPropertyChanged(nameof(Button1Selected)); }
        }

        private string button2Selected;

        public string Button2Selected
        {
            get { return button2Selected; }
            set { button2Selected = value; OnPropertyChanged(nameof(Button2Selected)); }
        }

        private string button3Selected;

        public string Button3Selected
        {
            get { return button3Selected; }
            set { button3Selected = value; OnPropertyChanged(nameof(Button3Selected)); }
        }

        private string button4Selected;

        public string Button4Selected
        {
            get { return button4Selected; }
            set { button4Selected = value; OnPropertyChanged(nameof(Button4Selected)); }
        }

        private string questionNumberString;

        public string QuestionNumberString
        {
            get { return questionNumberString; }
            set { questionNumberString = value; }
        }


        private string scoreString;

        public string ScoreString
        {
            get { return scoreString; }
            set { scoreString = value; OnPropertyChanged(nameof(ScoreString)); }
        }

        private ButtonPressed _buttonPressed;

        public ButtonPressed buttonPressed
        {
            get { return _buttonPressed; }
            set { _buttonPressed = value; }
        }


        public MainViewModel()
        {
            Score = 0;
            ScoreString = Score.ToString();
            QuestionNumber = 0;
            AnswerButtonEnable = false;
            QuestionNumberString = "Question" + (QuestionNumber + 1).ToString();

            Button1Selected = "LightGray";
            Button2Selected = "LightGray";
            Button3Selected = "LightGray";
            Button4Selected = "LightGray";

            filePath = Directory.GetCurrentDirectory() + @"\Questions.txt";
            questions = new List<string>();
            questions = File.ReadAllLines(filePath).ToList();

            filePath = Directory.GetCurrentDirectory() + @"\Answers.txt";
            answers = new List<string>();
            answers = File.ReadAllLines(filePath).ToList();

            filePath = Directory.GetCurrentDirectory() + @"\Options.txt";
            options = new List<string>();
            options = File.ReadAllLines(filePath).ToList();

            buttonPressed = new ButtonPressed(this);
            GetNextQuestion();
            GetNextOptions();

        }

        bool IsCorrectAnswer()
        {
            if (Button1Selected == "Cyan") { 
                 if(option1 != answers[QuestionNumber])
                    return false;
            }
            if (Button2Selected == "Cyan")
            {
                if (option2 != answers[QuestionNumber])
                    return false;
            }
            if (Button3Selected == "Cyan")
            {
                if (option3 != answers[QuestionNumber])
                    return false;
            }
            if (Button3Selected == "Cyan")
            {
                if (option3 != answers[QuestionNumber])
                    return false;
            }
            return true;
        }

        void GameWon()
        {
            MessageBox.Show("Congradulations, you won the game!!!");
            Messenger.Default.Send(new EndOfGame());
        }

        void GameOver()
        {
            MessageBox.Show($"Incorrect answer \n The answer is {answers[QuestionNumber]} \n Your score is {Score}");
            Messenger.Default.Send(new EndOfGame());
        }

        void GetNextQuestion()
        {
            Question = questions[QuestionNumber];
        }

        void GetNextOptions()
        {
            string[] gameOptions = options[QuestionNumber]?.Split(',');
            if (gameOptions != null)
            {
                Option1 = gameOptions[0];
                Option2 = gameOptions[1];
                Option3 = gameOptions[2];
                Option4 = gameOptions[3];
            }
        }

        public void CheckPressedButton(string pressedButton)
        {
            switch (pressedButton)
            {
                case "Button1":
                    Button1Selected = "Cyan";
                    Button2Selected = "LightGray";
                    Button3Selected = "LightGray";
                    Button4Selected = "LightGray";
                    answerButtonEnable = true;
                    break;
                case "Button2":
                    Button1Selected = "LightGray";
                    Button2Selected = "Cyan";
                    Button3Selected = "LightGray";
                    Button4Selected = "LightGray";
                    answerButtonEnable = true;
                    break;
                case "Button3":
                    Button1Selected = "LightGray";
                    Button2Selected = "LightGray";
                    Button3Selected = "Cyan";
                    Button4Selected = "LightGray";
                    answerButtonEnable = true;
                    break;
                case "Button4":
                    Button1Selected = "LightGray";
                    Button2Selected = "LightGray";
                    Button3Selected = "LightGray";
                    Button4Selected = "Cyan";
                    answerButtonEnable = true;
                    break;
                case "AnswerButton":
                    if(!IsCorrectAnswer())
                        GameOver();
                    QuestionNumber++;
                    Score++;
                    ScoreString = Score.ToString();
                    if (QuestionNumber >= questions.Count)
                        GameWon();
                    else
                    {
                        GetNextQuestion();
                        GetNextOptions();
                    }
                    QuestionNumberString = "Question" + (QuestionNumber + 1).ToString();

                    Button1Selected = "LightGray";
                    Button2Selected = "LightGray";
                    Button3Selected = "LightGray";
                    Button4Selected = "LightGray";
                    answerButtonEnable = false;
                    break;
                default:
                    break;
            }
        }
    }
}
