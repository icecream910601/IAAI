using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class LandingPageController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: LandingPage
        public ActionResult Index()
        {
            var landingPageSliders = db.LandingPageSliders.ToList();
            var landingPageLinks = db.LandingPageLinks.ToList();
            var news= db.News.ToList();

            var viewModel = new LandingPageViewModel
            {
                Sliders = landingPageSliders,
                Links = landingPageLinks,
                News=news
            };

            return View(viewModel);
        }
    }
}