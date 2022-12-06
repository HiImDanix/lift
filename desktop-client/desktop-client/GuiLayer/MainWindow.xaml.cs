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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Web.UI.WebControls;
using RestSharp.Extensions;
using System.Threading;

namespace desktop_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QuestionController _questionController;
        public MainWindow()
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

            if (isAddButtonEnabled())
            {
                int response = await _questionController.SaveQuestion(imagePath, question, category, answers);
                if (response == 200)
                {
                    MessageBox.Show("Question added successfully");
                    clearFields();
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
        private bool isAddButtonEnabled()
        {
            return ImageTxt.Text.HasValue() && QuestionTxt.Text.HasValue() && CategoryTxt.Text.HasValue() &&
                AnswerTxt.Text.HasValue() && Answer1Txt.Text.HasValue() && Answer2Txt.Text.HasValue() && Answer3Txt.Text.HasValue();
        }
        private void clearFields()
        {
            ImageTxt.Text = "";
            QuestionTxt.Text = "";
            CategoryTxt.Text = "";
            AnswerTxt.Text = "";
            Answer1Txt.Text = "";
            Answer2Txt.Text = "";
            Answer3Txt.Text = "";
        }
    }
}
