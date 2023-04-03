using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class AssnLicensesController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: AssnLicenses
        public ActionResult Index()
        {
            var licenses = db.AssnLicenses.FirstOrDefault();
            return View(licenses);
        }
    }
}