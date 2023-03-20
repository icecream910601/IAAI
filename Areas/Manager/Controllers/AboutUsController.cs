using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Filter;
using IAAI.Models;

namespace IAAI.Areas.Manager.Controllers
{
    [PagePermission]
    [PermissionFilters]
    [Authorize]
    
    public class AboutUsController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/AboutUs
        public ActionResult Index()
        {
            var aboutUs = db.About.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.About = aboutUs?.About;

            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AboutUs aboutUs)
        {
            var existingAbout = db.About.FirstOrDefault();

            if (existingAbout != null)
            {
                existingAbout.About = aboutUs.About;
                db.Entry(existingAbout).State = EntityState.Modified;
            }
            else
            {
                db.About.Add(aboutUs);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}