using desktop_client.ControlLayer;
using desktop_client.ModelLayer;
using System;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            int insertedId = -1;
            string messageText;
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

            insertedId = await _questionController.SaveQuestion(imagePath, question, category, answers);
            messageText = (insertedId > 0) ? $"Saved with ID {insertedId}" : "Couldn't save";
            messageLabel.Content = messageText;

        }

        private void QuestionTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
