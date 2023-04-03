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
    public class AssnReferController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/AssnRefer
        public ActionResult Index()
        {
            var refer = db.AssnRefers.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.Refer = refer?.Refer;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AssnRefer assnRefer)
        {
            var existingRefer = db.AssnRefers.FirstOrDefault();

            if (existingRefer != null)
            {
                existingRefer.Refer = assnRefer.Refer;
                db.Entry(existingRefer).State = EntityState.Modified;
            }
            else
            {
                db.AssnRefers.Add(assnRefer);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}