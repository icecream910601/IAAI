using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;
using MvcPaging;

namespace IAAI.Controllers
{

    public class KnowledgesController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        private const int DefaultPageSize = 15;  //一頁有幾筆

        // GET: Knowledges
        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;  //沒有頁數時如何處理
            List<Knowledge> listKnowledges = db.Knowledges.ToList();
            ViewBag.listKnowledges = listKnowledges;

            return View(listKnowledges.OrderBy(x => x.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Knowledes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Knowledge knowledge= db.Knowledges.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }
    }
}