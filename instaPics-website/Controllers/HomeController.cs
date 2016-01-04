using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using instaPics_website.Models;
using instaPics_website.Models.POCO;

namespace instaPics_website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SessionUser.Username = "";
            return View();
        }

        //fonction qui va appeler le Model pour rechercher/créer l'utilisateur
        //retour le résultat au controller d'angularjs
        public string userConnect(string username )
        {
            LoginModel testLogin = new LoginModel();
            UserEntity result = testLogin.Connect(username);
            if(result.Username != "error")
            {
                SessionUser.Username = result.Username;
            }

            return result.Username;
        }
    }
}