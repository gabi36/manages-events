using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChef.Tables
{
    public class Users
    {
        public String _username;
        public String _password;
        public String _email;

        public Users(String nume, String parola, String email)
        {
            _username = nume;
            _password = parola;
            _email = email;
        }


        public String username
        {
            get
            {
                return _username;
            }
        }

        public String password
        {
            get
            {
                return _password;
            }
        }

        public String email
        {
            get
            {
                return _email;
            }
        }
    }
}
