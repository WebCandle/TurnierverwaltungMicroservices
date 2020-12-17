using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Turnierverwaltung
{
    public class Global : HttpApplication
    {
        public static string mySqlConnectionString;
        void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            mySqlConnectionString = "server=localhost;database=Turnierverwaltung_db;uid=root;password=";

        }
        
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            
        }
    }
}