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
    public class B_CertifiedMembersController : Controller
    {
         IAAIDbContext db = new IAAIDbContext();

        // GET: CertifiedMembers
        public ActionResult Index()
        {
            return View(db.Certified.ToList());
        }

        // GET: CertifiedMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.Certified.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // GET: CertifiedMembers/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: CertifiedMembers/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Country,Title,Company,InitDate")] CertifiedMember certifiedMember, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(certifiedMember);
                    }

                    certifiedMember.Picture = CertifiedMember.SaveUpImage(Picture);
                }


                db.Certified.Add(certifiedMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(certifiedMember);
        }



        // GET: CertifiedMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.Certified.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // POST: CertifiedMembers/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture,FirstName,LastName,Country,Title,Company,InitDate")] CertifiedMember certifiedMember, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(certifiedMember);
                    }

                    certifiedMember.Picture = CertifiedMember.SaveUpImage(Picture);
                }
                db.Entry(certifiedMember).State = EntityState.Modified;
                db.SaveChanges();
            }

            //return View(certifiedMember);
            return RedirectToActionPermanent("Index", null,
                new { page = Request["page"] });
        }

        // GET: CertifiedMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.Certified.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // POST: CertifiedMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CertifiedMember certifiedMember = db.Certified.Find(id);
            db.Certified.Remove(certifiedMember);
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
