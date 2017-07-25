using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructoApp.Models
{
    public class Admin
    {
        private string username = "Ali";
        private string password = "123";
        public string user { get; set; }
        public string pass { get; set; }
        public string getUsername()
        {
            return username;
        }
        public string getpassword()
        {
            return password;
        }
    }
}