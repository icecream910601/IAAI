using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Filter;
using IAAI.Models;

namespace IAAI.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class B_AboutUsController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        public void SaveToDb(AboutUs aboutUs)
        {
            //Write to the database
            //db.About.Add(aboutUs);
            //db.SaveChanges();
            // Query for existing About
            var existingAbout = db.About.FirstOrDefault();

            if (existingAbout != null)
            {
                // Update existing About with new values
                existingAbout.About = aboutUs.About;


                db.Entry(existingAbout).State = EntityState.Modified;
            }
            else
            {
                // Create new About with the submitted values
                db.About.Add(aboutUs);

            }
            db.SaveChanges();

        }

        public AboutUs ReadFromDb()
        {
            // Read from the database
            return db.About.OrderByDescending(x => x.Id).FirstOrDefault();
        }


        public ActionResult Index()
        {
            var aboutUs = ReadFromDb();
            ViewBag.About = aboutUs?.About;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(AboutUs aboutUs)
        {
            SaveToDb(aboutUs);
            return RedirectToAction("Index");
        }

        public ActionResult Result()
        {
            var aboutUs = ReadFromDb();
            ViewBag.About = aboutUs.About;
            return View();
        }
    }

}

