using desktop_client.ControlLayer;
using desktop_client.ModelLayer;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using RestSharp.Extensions;
using System.Threading;
using desktop_client.GuiLayer;

namespace desktop_client.GuiLayer
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        QuestionController _questionController;
        public Main()
        {
            InitializeComponent();
            _questionController = new QuestionController();
        }
        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = ImageTxt.Text;
            string question = QuestionTxt.Text;
            string category = CategoryTxt.Text;
            string correctAnswer = AnswerTxt.Text;
            string answer1 = Answer1Txt.Text;
            string answer2 = Answer2Txt.Text;
            string answer3 = Answer3Txt.Text;

            List<string> answers = new List<string> {
                correctAnswer,
                answer1,
                answer2,
                answer3
            };

            if (IsAddButtonEnabled())
            {
                int response = await _questionController.SaveQuestion(imagePath, question, category, answers);
                if (response == 200)
                {
                    MessageBox.Show("Question added successfully");
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }

            }
            else
            {
                MessageBox.Show("Please fill all fields");
            }
        }

        private void ClearFields()
        {
            ImageTxt.Text = "";
            QuestionTxt.Text = "";
            CategoryTxt.Text = "";
            AnswerTxt.Text = "";
            Answer1Txt.Text = "";
            Answer2Txt.Text = "";
            Answer3Txt.Text = "";
        }

        private void FillFields(Question question)
        {
            ImageTxt.Text = question.ImagePath;
            QuestionTxt.Text = question.QuestionText;
            CategoryTxt.Text = question.Category;
            AnswerTxt.Text = question.Answers[0].AnswerText;
            Answer1Txt.Text = question.Answers[1].AnswerText;
            Answer2Txt.Text = question.Answers[2].AnswerText;
            Answer3Txt.Text = question.Answers[3].AnswerText;
        }

        private bool IsAddButtonEnabled()
        {
            return ImageTxt.Text.HasValue() && QuestionTxt.Text.HasValue() && CategoryTxt.Text.HasValue() &&
                AnswerTxt.Text.HasValue() && Answer1Txt.Text.HasValue() && Answer2Txt.Text.HasValue() && Answer3Txt.Text.HasValue();
        }

        private async void getButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = questionList.SelectedIndex;
            var questions = await _questionController.GetQuestions();
            questionList.ItemsSource = questions;
            questionList.SelectedIndex = selectedIndex;
        }
       

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }

        private async void questionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (questionList.SelectedIndex != -1)
            {
                Question question = (Question)questionList.SelectedItem;
                // Fill fields with question data
                FillFields(question);
                addButton.Visibility = Visibility.Hidden;
                editButton.Visibility = Visibility.Visible;
            }

        }

        private async void editButton_Click(object sender, RoutedEventArgs e)
        {
            string imagePath = ImageTxt.Text;
            string question = QuestionTxt.Text;
            string category = CategoryTxt.Text;
            string correctAnswer = AnswerTxt.Text;
            string answer1 = Answer1Txt.Text;
            string answer2 = Answer2Txt.Text;
            string answer3 = Answer3Txt.Text;

            List<string> answers = new List<string> {
                correctAnswer,
                answer1,
                answer2,
                answer3
            };

            // Get old question object to extract row version
            int selectedIndex = questionList.SelectedIndex;
            Question oldQuestion = (Question)questionList.SelectedItem;
            int response = await _questionController.EditQuestion(oldQuestion.Id, imagePath, question, category, answers, oldQuestion.RowVer);
            if(response == 200)
            {
                MessageBox.Show("Question updated sucessfully");
                ClearFields();
                editButton.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Visible;
                // execute get button click to refresh list
                // TODO: Not optimal. Can select different object if anoter admin adds a question
                getButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                questionList.SelectedIndex = selectedIndex;
            }
            else
            {
                MessageBox.Show("Could not update the question. The question might have been updated by another admin. Please refresh the list and try again");
            }
        }
    }
 }

