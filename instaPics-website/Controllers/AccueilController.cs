using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using instaPics_website.Models;
using instaPics_website.Models.POCO;
using System.Web.Routing;
using System.IO;

namespace instaPics_website.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Accueil
        public ActionResult Index()
        {
            // si la session n'existe pas, on redirige vers la page de login
            if(SessionUser.Username.Length == 0)
            {
                Response.Redirect(new Uri(Request.Url, Url.Action("Index", "Home")).ToString());
            }
            //récupération de la liste des images dans différentes catégories
            AccueilModel accueilmodel = new AccueilModel();
            accueilmodel.listImg();

            ViewBag.listImgUser = accueilmodel.lstImageUserPath;
            ViewBag.listImgUserBN = accueilmodel.lstImageBNUserPath;
            ViewBag.listImgOther = accueilmodel.lstOtherImgPath;

            return View();
        }

        public void uploadImage(HttpPostedFileBase file)
        {
            //si l'image existe, on appelle la fonction d'uplaod
            if (file != null)
            {
                AccueilModel uploadImg = new AccueilModel();
                uploadImg.UploadImage(file);
            }

            Response.Redirect(new Uri(Request.Url, Url.Action("Index", "Accueil")).ToString());
        }

        // controller pour ajouter ou supprimer un favoris
        public void favorite(string gestionfav, string nameimg)
        {
            // si la session n'existe pas, on redirige vers la page de login
            if (SessionUser.Username.Length == 0)
            {
                Response.Redirect(new Uri(Request.Url, Url.Action("Index", "Home")).ToString());
            }

            AccueilModel accueilmodel = new AccueilModel();
            if (gestionfav == "addlike")
            {
                accueilmodel.addFavorite(nameimg);
            }
            else
            {
                accueilmodel.removeFavorite(nameimg);
            }
            Response.Redirect(new Uri(Request.Url, Url.Action("Index", "Accueil")).ToString());
        }
    }
}