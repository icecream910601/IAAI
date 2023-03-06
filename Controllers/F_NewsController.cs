using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;
using MvcPaging;

namespace IAAI.Controllers
{
    public class F_NewsController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        private const int DefaultPageSize = 15;  //一頁有幾筆

        // GET: test
        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;  //沒有頁數時如何處理
            var news = db.News.Include(n => n.MyCatalog);
            List<News> listNews = news.ToList();
            ViewBag.listNews = listNews;

            return View(news.OrderBy(x => x.StartDate).ToPagedList(currentPageIndex, DefaultPageSize));
            //return View();
        }

        // GET: B_News2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
    }
}