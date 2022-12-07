using RestSharp;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Login()
        {
            InitializeComponent();
        }

        private void emailTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void loginButton_Click(object sender2, RoutedEventArgs e)
        {
            var email = emailTxt.Text.Trim();
            var password = passwordTxt.Text.Trim();
            if (email.HasValue() && password.HasValue())
            {
                // prep RestSharp
                var client = new RestClient(WebConfigurationManager.AppSettings["WebserviceURI"]);
                client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                var request = new RestRequest("/login", Method.POST);

                // add parameters
                request.AddParameter("email", email);
                request.AddParameter("password", password);

                // execute request
                var response = client.Execute(request);

                if ((int)response.StatusCode == 200)
                {
                    // Show main window
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                } else
                {
                    // Show error message
                    MessageBox.Show("Login failed. Please try again.");
                    {
                    }
                }

            

            } else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
    }
}
