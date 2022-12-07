using desktop_client.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desktop_client.repositoryLayer
{
    public class AuthRepository
    {
        private static AuthRepository _instance;
        private Administrator _admin;

        private AuthRepository()
        {
            _admin = new Administrator();
        }

        public static AuthRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AuthRepository();
            }
            return _instance;
        }

        public void SetAdmin(Administrator admin)
        {
            _admin = admin;
        }

        public Administrator GetAdmin()
        {
            return _admin;
        }
    }
}
