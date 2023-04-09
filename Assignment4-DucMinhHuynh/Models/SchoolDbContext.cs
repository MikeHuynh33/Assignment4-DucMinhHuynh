using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assignment4_DucMinhHuynh.Models
{
    public class SchoolDbContext
    {
        //properties define an SchoolTeacher Database connection
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        // put things together to create StringConnection
        protected static string connectionDbString
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

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(connectionDbString);
        }
    }
}
