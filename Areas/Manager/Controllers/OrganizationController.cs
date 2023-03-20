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

    public class OrganizationController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/AboutUs
        public ActionResult Index()
        {
            var organization = db.Organizations.OrderByDescending(x => x.Id).FirstOrDefault();
              ViewBag.Organization = organization?.Organize;

            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(Organization organization)
        {
            var existingOrganization = db.Organizations.FirstOrDefault();

            if (existingOrganization != null)
            {
                existingOrganization.Organize = organization.Organize;
                db.Entry(existingOrganization).State = EntityState.Modified;
            }
            else
            {
                db.Organizations.Add(organization);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}