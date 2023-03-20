using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class MastersController : Controller
    {

        private IAAIDbContext db = new IAAIDbContext();
        // GET: F_Masters
        public ActionResult Index()
        {
            var master = db.Masters.ToList();

            return View(master);
        }


        // GET: F_Masters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Masters.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

    }
}