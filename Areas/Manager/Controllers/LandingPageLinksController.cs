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
    public class LandingPageLinksController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/LandingPageLinks
        public ActionResult Index()
        {
            return View(db.LandingPageLinks.ToList());
        }

        // GET: Manager/LandingPageLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageLink landingPageLink = db.LandingPageLinks.Find(id);
            if (landingPageLink == null)
            {
                return HttpNotFound();
            }
            return View(landingPageLink);
        }

        // GET: Manager/LandingPageLinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/LandingPageLinks/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( LandingPageLink landingPageLink, HttpPostedFileBase LinkPicture)
        {
            //if (ModelState.IsValid)
            //{
            //    db.LandingPageLinks.Add(landingPageLink);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(landingPageLink);
            if (ModelState.IsValid)
            {
                if (LinkPicture != null)
                {
                    if (LinkPicture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(landingPageLink);
                    }

                    landingPageLink.LinkPicture = Utility.SaveUpImage(LinkPicture);
                }

                db.LandingPageLinks.Add(landingPageLink);
                db.SaveChanges();
                return RedirectToAction("Index", "LandingPageLinks", new { area = "Manager" });
            }

            return View(landingPageLink);
        }

        // GET: Manager/LandingPageLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageLink landingPageLink = db.LandingPageLinks.Find(id);
            if (landingPageLink == null)
            {
                return HttpNotFound();
            }
            return View(landingPageLink);
        }

        // POST: Manager/LandingPageLinks/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LinkPicture,Link")] LandingPageLink landingPageLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landingPageLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(landingPageLink);
        }

        // GET: Manager/LandingPageLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageLink landingPageLink = db.LandingPageLinks.Find(id);
            if (landingPageLink == null)
            {
                return HttpNotFound();
            }
            return View(landingPageLink);
        }

        // POST: Manager/LandingPageLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandingPageLink landingPageLink = db.LandingPageLinks.Find(id);
            db.LandingPageLinks.Remove(landingPageLink);
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
