using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Server.Security
{
    public class SecurityHelper
    {

        private readonly IConfiguration _configuration;

        // Fetches configuration from more sources
        public SecurityHelper(IConfiguration inConfiguration)
        {
            _configuration = inConfiguration;
        }

        // Create key for signing
        public SymmetricSecurityKey GetSecurityKey()
        {
            SymmetricSecurityKey SIGNING_KEY = null;
            if (_configuration != null)
            {
                string SECRET_KEY = _configuration["SECRET_KEY"];
                SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            }
            return SIGNING_KEY;
        }
        public bool IsValidUsernameAndPassword(string username, string password)
        {
            string allowedUsername = _configuration["AllowDesktopApp:Username"];
            string allowedPassword = _configuration["AllowDesktopApp:Password"];
            bool credentialsOk = (username.Equals(allowedUsername)) && (password.Equals(allowedPassword));
            return credentialsOk;
        }
    }
}
