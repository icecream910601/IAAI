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
    public class ForumController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/Forum
        public ActionResult Index()
        {
            var forum = db.Forum.Include(f => f.ForumMember);
            return View(forum.ToList());
        }

        // GET: B_Forum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }

            List<ForumReply> replyList = db.ForumReplies.Where(fme => fme.ForumId == id).ToList();
            ViewBag.ReplyList = replyList;
            return View(forum);
        }

        // GET: Manager/Forum/Create
        public ActionResult Create()
        {
            ViewBag.ForumMemberId = new SelectList(db.ForumMembers, "Id", "Account");
            return View();
        }



        // POST: Manager/Forum/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Forum.Add(forum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ForumMemberId = new SelectList(db.ForumMembers, "Id", "Account", forum.ForumMemberId);
            return View(forum);
        }

        // GET: Manager/Forum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForumMemberId = new SelectList(db.ForumMembers, "Id", "Account", forum.ForumMemberId);
            return View(forum);
        }

        // POST: Manager/Forum/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Forum forum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ForumMemberId = new SelectList(db.ForumMembers, "Id", "Account", forum.ForumMemberId);
            return View(forum);
        }

        // GET: Manager/Forum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // POST: Manager/Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forum.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }

            // 刪除相關聯的 ForumReply 資料
            List<ForumReply> replies = db.ForumReplies.Where(r => r.ForumId == id).ToList();
            foreach (var reply in replies)
            {
                db.ForumReplies.Remove(reply);
            }

            // 刪除 Forum 資料
            db.Forum.Remove(forum);
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
