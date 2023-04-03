using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class AssnSurveyController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: AssnSurvey
        public ActionResult Index()
        {
            var survey = db.AssnSurveys.FirstOrDefault();

            return View(survey);
        }
    }
}