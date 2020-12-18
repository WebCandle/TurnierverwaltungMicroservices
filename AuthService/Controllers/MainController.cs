using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using AuthService;
using AuthService.Models;

namespace AuthService.Controllers
{
    public class MainController
    {
        public string DBConnectionString { get; set; }

        public MainController(string dBConnectionString)
        {
            DBConnectionString = dBConnectionString;
        }

        public string MD5(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public bool Login_Authenticate(ref LoginModel model)
        {
            bool auth = false;
            using (MySqlConnection conn = new MySqlConnection(DBConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    string qry = string.Format("SELECT * FROM `users` WHERE `name` = \"{0}\" AND `password` = \"{1}\" LIMIT 1", MySqlHelper.EscapeString(model.UserName), MD5(model.Password));
                    cmd.CommandText = qry;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int active = Convert.ToInt32(reader["active"]);
                            if (active == 1)
                            {
                                model.ID = long.Parse(reader["ID"].ToString());
                                model.Rolle = Convert.ToString(reader["rolle"]);
                                auth = true;
                            }
                        }
                    }
                }
                conn.Close();
            }
            return auth;
        }
    }
}