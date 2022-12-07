using desktop_client.ModelLayer;
using desktop_client.repositoryLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows;

namespace desktop_client.ControlLayer
{
    public class LoginController
    {
        public bool Login(string email, string password)
        {

            var client = new RestClient(WebConfigurationManager.AppSettings["WebserviceURI"]);
            var request = new RestRequest("/login", Method.POST);

            request.AddParameter("email", email);
            request.AddParameter("password", password);

            var response = client.Execute(request);

            // get fields from response - id, email, session
            var content = response.Content;
            var statusCode = response.StatusCode;

            // deserialize json
            var admin = JsonConvert.DeserializeObject<Administrator>(content);

            // check if response is valid
            if (statusCode == HttpStatusCode.OK)
            {
                // Show session message box
                AuthRepository.GetInstance().SetAdmin(admin);
                return true;
            }

            return false;
            
        }
    }
}
