using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using instaPics_website.Models;

namespace instaPics_website.Controllers
{
    public class AccueilController : Controller
    {
        // GET: Accueil
        public ActionResult Index()
        {
            return View();
        }

        public string uploadImage(string file)
        {
            AccueilModel uploadImg = new AccueilModel();
            string result = uploadImg.UploadImage(file);

            return result;
        }
    }
}