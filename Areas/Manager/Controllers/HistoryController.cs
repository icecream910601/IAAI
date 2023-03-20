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

    public class HistoryController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/History
        public ActionResult Index()
        {
            var history = db.Histories.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.History = history?.Historyy;

            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(History history)
        {
            var existingHistory = db.Histories.FirstOrDefault();
            if (existingHistory != null)
            {
                existingHistory.Historyy = history.Historyy;
                db.Entry(existingHistory).State = EntityState.Modified;
            }
            else
            {
                db.Histories.Add(history);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}