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

    public class AssnSurveyController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/AssnSurvey
        public ActionResult Index()
        {
            var survey = db.AssnSurveys.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.Survey = survey?.Survey;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AssnSurvey assnSurvey)
        {
            var existingSurvey = db.AssnSurveys.FirstOrDefault();

            if (existingSurvey != null)
            {
                existingSurvey.Survey = assnSurvey.Survey;
                db.Entry(existingSurvey).State = EntityState.Modified;
            }
            else
            {
                db.AssnSurveys.Add(assnSurvey);
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}