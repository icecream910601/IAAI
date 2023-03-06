using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class F_HistoryController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: F_History
        public ActionResult Index()
        {
            var history = db.Histories.FirstOrDefault();
            return View(history);
        }
    }
}