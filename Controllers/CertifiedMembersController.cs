using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class CertifiedMembersController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: F_CertifiedMembers
        public ActionResult Index()
        {
            var CMembers = db.Certified.ToList();
            return View(CMembers);
        }
    }
}