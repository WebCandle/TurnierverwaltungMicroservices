using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;

namespace Turnierverwaltung
{
    public class User
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Rolle { get; set; }
        public bool Auth { get; set; }
        public bool Has_Permission(string rolle)
        {
            if (Rolle.ToLower() == rolle.ToLower())
                return true;
            else
                return false;
        }
        public User(HttpSessionState Session)
        {
            if (Session["auth"] != null && (bool)Session["auth"])
            {
                ID = 1;
                Name = Session["name"].ToString();
                Rolle = Session["rolle"].ToString();
                Auth = (bool)Session["auth"];
            }
            else
            {
                ID = 0;
                Name = "Gast";
                Auth = false;
                Rolle = "Gast";
            }
        }
    }
}