using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class F_OrganizationController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: F_Organization
        public ActionResult Index()
        {
            var organize = db.Organizations.FirstOrDefault();
            return View(organize);
        }
    }
}