using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaPics_website.Models.POCO
{
    public static class SessionUser
    {
        //public static string Username { get; set; }
        private static string username = "";

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

    }
}
