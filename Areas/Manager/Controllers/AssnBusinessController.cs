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

    public class AssnBusinessController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/AssnBusiness
        public ActionResult Index()
        {
            var business = db.AssnBusinesses.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.Business = business?.Business;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AssnBusiness assnBusiness)
        {
            var existingBusiness = db.AssnBusinesses.FirstOrDefault();

            if (existingBusiness != null)
            {
                existingBusiness.Business = assnBusiness.Business;
                db.Entry(existingBusiness).State = EntityState.Modified;
            }
            else
            {
                db.AssnBusinesses.Add(assnBusiness);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}