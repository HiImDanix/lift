using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace desktop_client.ControlLayer
{
    public class LoginController
    {
        public bool Login(string email, string password)
        {
            bool isValid = false;

            var client = new RestClient(WebConfigurationManager.AppSettings["WebserviceURI"]);
            var request = new RestRequest("/login", Method.POST);

            request.AddParameter("email", email);
            request.AddParameter("password", password);

            var response = client.Execute(request);

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
