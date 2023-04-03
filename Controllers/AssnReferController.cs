using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class AssnReferController : Controller
    {

        private IAAIDbContext db = new IAAIDbContext();

        // GET: AssnRefer
        public ActionResult Index()
        {
            var refer = db.AssnRefers.FirstOrDefault();

            return View(refer);
        }
    }
}