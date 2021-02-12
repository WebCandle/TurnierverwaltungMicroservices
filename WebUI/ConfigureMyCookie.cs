#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      09.02.2021
#endregion

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace WebUI
{
    internal class ConfigureMyCookie : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        // You can inject services here
        public ConfigureMyCookie()
        {
        }

        public void Configure(string name, CookieAuthenticationOptions options)
        {
            // Only configure the schemes you want
            if (name == Startup.CookieScheme)
            {
                // options.LoginPath = "/someotherpath";
            }
        }

        public void Configure(CookieAuthenticationOptions options)
            => Configure(Options.DefaultName, options);
    }
}
