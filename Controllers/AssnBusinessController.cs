using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class AssnBusinessController : Controller
    {

        private IAAIDbContext db = new IAAIDbContext();


        // GET: AssnBusiness
        public ActionResult Index()
        {
            var business = db.AssnBusinesses.FirstOrDefault();
            return View(business);
        }
    }
}