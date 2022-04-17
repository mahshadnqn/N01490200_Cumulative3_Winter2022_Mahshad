using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace N01490200_Cumulative3_Winter2022_Mahshad.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }



        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }

        //This is the method we actually use to get the database!
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }


    }
}