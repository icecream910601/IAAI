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
    public class LandingPageSlidersController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/LandingPageSliders
        public ActionResult Index()
        {
            return View(db.LandingPageSliders.ToList());
        }

        // GET: Manager/LandingPageSliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageSlider landingPageSlider = db.LandingPageSliders.Find(id);
            if (landingPageSlider == null)
            {
                return HttpNotFound();
            }
            return View(landingPageSlider);
        }

        // GET: Manager/LandingPageSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/LandingPageSliders/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LandingPageSlider landingPageSlider, HttpPostedFileBase SliderPicture)
        {
           
            if (ModelState.IsValid)
            {
                if (SliderPicture != null)
                {
                    if (SliderPicture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(landingPageSlider);
                    }

                    landingPageSlider.SliderPicture = Utility.SaveUpImage(SliderPicture);
                }

                db.LandingPageSliders.Add(landingPageSlider);
                db.SaveChanges();
                return RedirectToAction("Index", "LandingPageSliders", new { area = "Manager" });
            }

            return View(landingPageSlider);
        }

        // GET: Manager/LandingPageSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageSlider landingPageSlider = db.LandingPageSliders.Find(id);
            if (landingPageSlider == null)
            {
                return HttpNotFound();
            }
            return View(landingPageSlider);
        }

        // POST: Manager/LandingPageSliders/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SliderPicture")] LandingPageSlider landingPageSlider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landingPageSlider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(landingPageSlider);
        }

        // GET: Manager/LandingPageSliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageSlider landingPageSlider = db.LandingPageSliders.Find(id);
            if (landingPageSlider == null)
            {
                return HttpNotFound();
            }
            return View(landingPageSlider);
        }

        // POST: Manager/LandingPageSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandingPageSlider landingPageSlider = db.LandingPageSliders.Find(id);
            db.LandingPageSliders.Remove(landingPageSlider);
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
