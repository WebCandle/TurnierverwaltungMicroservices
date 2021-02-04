using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
