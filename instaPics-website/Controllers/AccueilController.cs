using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using instaPics_website.Models;
using System.Web.Routing;

namespace instaPics_website.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Accueil
        public ActionResult Index()
        {
            return View();
        }

        public void uploadImage(HttpPostedFileBase file)
        {
            AccueilModel uploadImg = new AccueilModel();
            uploadImg.UploadImage(file);

            Response.Redirect(new Uri(Request.Url, Url.Action("Index", "Accueil")).ToString());
        }
    }
}