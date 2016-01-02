using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instaPics_website.Models.POCO
{
    //session de l'utilisateur
    public static class SessionUser
    {
        private static string username = "";

        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

    }
}
