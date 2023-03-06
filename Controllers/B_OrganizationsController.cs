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
    public class B_OrganizationsController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        public void SaveToDb(Organization organization)
        {
            //Write to the database
            //db.Organizations.Add(organization);
            //db.SaveChanges();

            // Query for existing organization
            var existingOrganization = db.Organizations.FirstOrDefault();

            if (existingOrganization != null)
            {
                // Update existing organization with new values
                existingOrganization.Organize = organization.Organize;


                db.Entry(existingOrganization).State = EntityState.Modified;
            }
            else
            {
                // Create new organization with the submitted values
                db.Organizations.Add(organization);
               
            }
            db.SaveChanges();


        }

        public Organization ReadFromDb()
        {
            // Read from the database
            return db.Organizations.FirstOrDefault();
        }


        public ActionResult Index()
        {
            var organization = ReadFromDb();
            ViewBag.Organization = organization?.Organize;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(Organization organization)
        {
            SaveToDb(organization);

            return RedirectToAction("Index");

        }

      
    }
}
