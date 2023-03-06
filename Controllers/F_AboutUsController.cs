using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class F_AboutUsController : Controller
    {

        private IAAIDbContext db = new IAAIDbContext();

        // GET: F_AboutUs
        public ActionResult Index()
        {
            var aboutUs = db.About.FirstOrDefault();
            return View(aboutUs);
        }
    }
}