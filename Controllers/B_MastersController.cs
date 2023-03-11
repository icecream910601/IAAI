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

namespace IAAI.Controllers
{
    [PagePermission]
    [PermissionFilters]
    [Authorize]
    public class B_MastersController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: B_Masters
        public ActionResult Index()
        {
            return View(db.Masters.ToList());
        }

        // GET: B_Masters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Masters.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // GET: B_Masters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: B_Masters/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Picture,Name,PresentJob,Education,Introduction,Experience,Description")] Master master, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(master);
                    }

                    master.Picture = CertifiedMember.SaveUpImage(Picture);
                }


                db.Masters.Add(master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master);
        }

        // GET: B_Masters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Masters.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }

            // 取得 content 資料
            string description = master.Description;

            // 放入 ViewBag 中
            ViewBag.Content = description;

            return View(master);
        }

        // POST: B_Masters/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture,Name,PresentJob,Education,Introduction,Experience,Description")] Master master, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(master);
                    }

                   master.Picture = CertifiedMember.SaveUpImage(Picture);
                }
                db.Entry(master).State = EntityState.Modified;
                db.SaveChanges();
            }

            //return View(certifiedMember);
            return RedirectToActionPermanent("Index", null,
                new { page = Request["page"] });
        }

        // GET: B_Masters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master master = db.Masters.Find(id);
            if (master == null)
            {
                return HttpNotFound();
            }
            return View(master);
        }

        // POST: B_Masters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master master = db.Masters.Find(id);
            db.Masters.Remove(master);
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
