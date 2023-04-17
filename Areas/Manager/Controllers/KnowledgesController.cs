using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Filter;
using IAAI.Models;
using MvcPaging;

namespace IAAI.Areas.Manager.Controllers
{
    [PagePermission]
    [PermissionFilters]
    [Authorize]


    public class KnowledgesController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        private const int DefaultPageSize = 15; //一頁有幾筆

        // GET: Manager/Knowledges
        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;  //沒有頁數時如何處理
            List<Knowledge> listKnowledges = db.Knowledges.ToList();
            ViewBag.listKnowledges = listKnowledges;

            return View(listKnowledges.OrderBy(x => x.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // GET: Manager/Knowledges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledges.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // GET: Manager/Knowledges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Knowledges/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] // 允許 HTML 標記或輸入
        public ActionResult Create([Bind(Include = "Id,InitDate,Picture,Subject,Content")] Knowledge knowledge,
            HttpPostedFileBase Picture)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Knowledges.Add(knowledge);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(knowledge);
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(knowledge);
                    }

                    knowledge.Picture = Utility.SaveUpImage(Picture);
                }

                //knowledge.Content = Request.Form["Content"];

                db.Knowledges.Add(knowledge);
                db.SaveChanges();

            }
            return RedirectToAction("Index");

        }

        // GET: Manager/Knowledges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledges.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }

            // 取得 content 資料
            string content =knowledge.Content;

            // 放入 ViewBag 中
            ViewBag.Content = content;

            return View(knowledge);
        }

        // POST: Manager/Knowledges/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] // 允許 HTML 標記或輸入
        public ActionResult Edit([Bind(Include = "Id,InitDate,Picture,Subject,Content")] Knowledge knowledge, HttpPostedFileBase Picture)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(knowledge).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(knowledge);
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(knowledge);
                    }

                    knowledge.Picture = Utility.SaveUpImage(Picture);
                }

                db.Entry(knowledge).State = EntityState.Modified;
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        // GET: Manager/Knowledges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knowledge knowledge = db.Knowledges.Find(id);
            if (knowledge == null)
            {
                return HttpNotFound();
            }
            return View(knowledge);
        }

        // POST: Manager/Knowledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knowledge knowledge = db.Knowledges.Find(id);
            db.Knowledges.Remove(knowledge);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
