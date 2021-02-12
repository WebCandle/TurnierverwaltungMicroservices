#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      02.02.2021
#endregion

using Common;

namespace IdentityService
{
    public interface IAuthenticateService
    {
        User Authenticate(string username, string password);
    }
}
