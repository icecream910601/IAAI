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

namespace IAAI.Areas.Manager.Controllers
{
    [PagePermission]
    [PermissionFilters]
    [Authorize]

    public class NewsCatalogsController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: /Manager/NewsCatalogs
        public ActionResult Index()
        {
            return View(db.NewsCatalogs.ToList());
        }

        // GET: /Manager/NewsCatalogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCatalog newsCatalog = db.NewsCatalogs.Find(id);
            if (newsCatalog == null)
            {
                return HttpNotFound();
            }
            return View(newsCatalog);
        }

        // GET: /Manager/NewsCatalogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Manager/NewsCatalogs/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Class,InitDate")] NewsCatalog newsCatalog)
        {
            if (ModelState.IsValid)
            {
                db.NewsCatalogs.Add(newsCatalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsCatalog);
        }

        // GET: /Manager/NewsCatalogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCatalog newsCatalog = db.NewsCatalogs.Find(id);
            if (newsCatalog == null)
            {
                return HttpNotFound();
            }
            return View(newsCatalog);
        }

        // POST: /Manager/NewsCatalogs/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Class,InitDate")] NewsCatalog newsCatalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsCatalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsCatalog);
        }

        // GET: /Manager/NewsCatalogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsCatalog newsCatalog = db.NewsCatalogs.Find(id);
            if (newsCatalog == null)
            {
                return HttpNotFound();
            }
            return View(newsCatalog);
        }

        // POST: /Manager/NewsCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsCatalog newsCatalog = db.NewsCatalogs.Find(id);
            db.NewsCatalogs.Remove(newsCatalog);
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
