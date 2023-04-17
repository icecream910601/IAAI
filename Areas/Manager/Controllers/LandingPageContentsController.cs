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
    public class LandingPageContentsController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/LandingPageContents
        public ActionResult Index()
        {
            return View(db.LandingPageContents.ToList());
        }

        // GET: Manager/LandingPageContents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageContent landingPageContent = db.LandingPageContents.Find(id);
            if (landingPageContent == null)
            {
                return HttpNotFound();
            }
            return View(landingPageContent);
        }

        // GET: Manager/LandingPageContents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/LandingPageContents/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LandingPageContent landingPageContent, HttpPostedFileBase Pictures1, HttpPostedFileBase Pictures2, HttpPostedFileBase Pictures3, HttpPostedFileBase Pictures4)
        {

            //過驗證
            ModelState.Remove("Picture1");
            ModelState.Remove("Picture2");
            ModelState.Remove("Picture3");
            ModelState.Remove("Picture4");

            if (Pictures1 != null)
            {
                if (Pictures1.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                {
                    ViewBag.Message = "檔案型態錯誤!";
                    return View(landingPageContent);
                }

                landingPageContent.Picture1 = Utility.SaveUpImage(Pictures1);
            }
            else
            {
                ModelState.AddModelError("Picture1","error");
            }


            if (Pictures2 != null)
            {
                if (Pictures2.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                {
                    ViewBag.Message = "檔案型態錯誤!";
                    return View(landingPageContent);
                }

                landingPageContent.Picture2 = Utility.SaveUpImage(Pictures2);
            }
            else
            {
                ModelState.AddModelError("Picture2", "error");
            }



            if (Pictures3 != null)
            {
                if (Pictures3.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                {
                    ViewBag.Message = "檔案型態錯誤!";
                    return View(landingPageContent);
                }

                landingPageContent.Picture3 = Utility.SaveUpImage(Pictures3);
            }
            else
            {
                ModelState.AddModelError("Picture3", "error");
            }


            if (Pictures4 != null)
            {
                if (Pictures4.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                {
                    ViewBag.Message = "檔案型態錯誤!";
                    return View(landingPageContent);
                }

                landingPageContent.Picture4 = Utility.SaveUpImage(Pictures4);
            }
            else
            {
                ModelState.AddModelError("Picture4", "error");
            }

            if (ModelState.IsValid)
            {
                db.LandingPageContents.Add(landingPageContent);
                db.SaveChanges();
            }

            return View(landingPageContent);
        }


        // GET: Manager/LandingPageContents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageContent landingPageContent = db.LandingPageContents.Find(id);
            if (landingPageContent == null)
            {
                return HttpNotFound();
            }
            return View(landingPageContent);
        }

        // POST: Manager/LandingPageContents/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture1,Picture2,Picture3,Picture4")] LandingPageContent landingPageContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landingPageContent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(landingPageContent);
        }

        // GET: Manager/LandingPageContents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandingPageContent landingPageContent = db.LandingPageContents.Find(id);
            if (landingPageContent == null)
            {
                return HttpNotFound();
            }
            return View(landingPageContent);
        }

        // POST: Manager/LandingPageContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandingPageContent landingPageContent = db.LandingPageContents.Find(id);
            db.LandingPageContents.Remove(landingPageContent);
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
