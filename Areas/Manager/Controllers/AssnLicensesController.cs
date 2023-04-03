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
    public class AssnLicensesController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/AssnLicenses
        public ActionResult Index()
        {
            var licenses = db.AssnLicenses.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.Licenses = licenses?.Licenses;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AssnLicenses assnLicenses)
        {
            var existingLicenses = db.AssnLicenses.FirstOrDefault();

            if (existingLicenses != null)
            {
                existingLicenses.Licenses = assnLicenses.Licenses;
                db.Entry(existingLicenses).State = EntityState.Modified;
            }
            else
            {
                db.AssnLicenses.Add(assnLicenses);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}