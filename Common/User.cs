#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      09.02.2021
#endregion

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public User()
        {
            UserId = 0; UserName = string.Empty;Password = string.Empty; Role = string.Empty; Token = string.Empty;
        }
        public User(string userName, string password, string role)
        {
            UserName = userName;
            Password = password;
            Role = role;
        }

        public void Asign(User user)
        {
            Password = user.Password;
            Role = user.Role;
        }
    }
}
