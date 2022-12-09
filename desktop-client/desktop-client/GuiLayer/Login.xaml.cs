using desktop_client.ControlLayer;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace desktop_client.GuiLayer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        LoginController _loginController;
        public Login()
        {
            InitializeComponent();
            _loginController = new LoginController();
        }

        private void loginButton_Click(object sender2, RoutedEventArgs e)
        {
            var email = emailTxt.Text.Trim();
            var password = passwordPassBox.Password.Trim();

            if (email.HasValue() && password.HasValue())
            {
                if (_loginController.Login(email, password))
                {
                    Main mainWindow = new Main();
                    mainWindow.Show();
                    this.Close();
                } 
                else
                {
                    MessageBox.Show("Wrong username or password");
                }
            } 
            else
            {
                MessageBox.Show("Please fill out username & password");
            }
        }
    }
}
