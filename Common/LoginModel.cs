#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      04.02.2021
#endregion

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
