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
    [PagePermission]
    [PermissionFilters]
    [Authorize]
    public class B_HistoriesController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        public void SaveToDb(History history)
        {
            //Write to the database
            //db.Historys.Add(History);
            //db.SaveChanges();

            // Query for existing History
            var existingHistory = db.Histories.FirstOrDefault();

            if (existingHistory != null)
            {
                // Update existing History with new values
                existingHistory.Historyy = history.Historyy;


                db.Entry(existingHistory).State = EntityState.Modified;
            }
            else
            {
                // Create new History with the submitted values
                db.Histories.Add(history);

            }
            db.SaveChanges();


        }

        public History ReadFromDb()
        {
            // Read from the database
            return db.Histories.FirstOrDefault();
        }


        public ActionResult Index()
        {
            var history = ReadFromDb();
            ViewBag.History = history?.Historyy;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(History history)
        {
            SaveToDb(history);

            return RedirectToAction("Index");

        }

    }
}
