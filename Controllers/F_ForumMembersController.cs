using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;

namespace IAAI.Controllers
{
    public class F_ForumMembersController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: F_ForumMembers
        public ActionResult Index()
        {
            return View(db.ForumMembers.ToList());
        }

        // GET: F_ForumMembers/Details/5
        public ActionResult Details(int? forumMemberId)
        {
            if (forumMemberId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumMember forumMember = db.ForumMembers.Find(forumMemberId);
            if (forumMember == null)
            {
                return HttpNotFound();
            }
            return View(forumMember);
        }

        // GET: F_ForumMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: F_ForumMembers/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(ForumMember forumMember, ForumMemberExp forumMemberExp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //       db.ForumMembers.Add(forumMember);
        //        db.SaveChanges();

        //        //forumMemberExp.ForumMember = null;
        //        //forumMemberExp.ForumMemberId = forumMember.Id;
        //        //db.ForumMemberExps.Add(forumMemberExp);

        //        forumMember.ForumMemberExps = new List<ForumMemberExp>();
        //        forumMemberExp.ForumMemberId = forumMember.Id;
        //        forumMemberExp.ForumMember = null;
        //        forumMember.ForumMemberExps.Add(forumMemberExp);

        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ForumMember forumMember, ForumMemberExp forumMemberExp1, ForumMemberExp forumMemberExp2, ForumMemberExp forumMemberExp3)
        {
            if (ModelState.IsValid)
            {
                db.ForumMembers.Add(forumMember);
                db.SaveChanges();

                forumMember.ForumMemberExps = new List<ForumMemberExp>();

                if (forumMemberExp1 != null)
                {
                    forumMemberExp1.ForumMemberId = forumMember.Id;
                    forumMemberExp1.ForumMember = null;
                    forumMember.ForumMemberExps.Add(forumMemberExp1);
                }

                if (forumMemberExp2 != null)
                {
                    forumMemberExp2.ForumMemberId = forumMember.Id;
                    forumMemberExp2.ForumMember = null;
                    forumMember.ForumMemberExps.Add(forumMemberExp2);
                }

                if (forumMemberExp3 != null)
                {
                    forumMemberExp3.ForumMemberId = forumMember.Id;
                    forumMemberExp3.ForumMember = null;
                    forumMember.ForumMemberExps.Add(forumMemberExp3);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }



        // GET: F_ForumMembers/Edit/5
        public ActionResult Edit(int? forumMemberId)
        {
            if (forumMemberId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForumMember forumMember = db.ForumMembers.Find(forumMemberId);
            if (forumMember == null)
            {
                return HttpNotFound();
            }

            List<ForumMemberExp> expList = db.ForumMemberExps.Where(fme => fme.ForumMemberId == forumMemberId).ToList();
            ViewBag.MemberExp = expList;

            return View(forumMember);
        }



        // POST: F_ForumMembers/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(ForumMember forumMember)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(forumMember).State = EntityState.Modified;
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    return View(forumMember);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ForumMember forumMember, List<ForumMemberExp> memberExpList)
        {
            if (ModelState.IsValid)
            {
                // 更新會員資訊
                db.Entry(forumMember).State = EntityState.Modified;

                // 更新會員經驗資料
                foreach (var memberExp in memberExpList)
                {
                    db.Entry(memberExp).State = EntityState.Modified;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // 如果有錯誤發生，重新載入經驗資料並返回編輯頁面
            var expList = db.ForumMemberExps.Where(fme => fme.ForumMemberId == forumMember.Id).ToList();
            ViewBag.MemberExp = expList;

            return View(forumMember);
        }





        // GET: F_ForumMembers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ForumMember forumMember = db.ForumMembers.Find(id);
        //    if (forumMember == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(forumMember);
        //}
        public ActionResult Delete(int? forumMemberId)
        {
            if (forumMemberId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ForumMember forumMember = db.ForumMembers.Find(forumMemberId);

            if (forumMember == null)
            {
                return HttpNotFound();
            }

            var forumMemberExp = new ForumMemberExp
            {
                ForumMember = forumMember
            };

            return View(forumMemberExp);
        }



        //POST: F_ForumMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ForumMember forumMember = db.ForumMembers.Find(id);
            db.ForumMembers.Remove(forumMember);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int? forumMemberId)
        //{
        //    ForumMember forumMember = db.ForumMembers.Find(forumMemberId);
        //    if (forumMember == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // Find the ForumMemberExp to delete
        //    ForumMemberExp forumMemberExp = db.ForumMemberExps.SingleOrDefault(f => f.ForumMemberId == forumMemberId);

        //    // Check if the ForumMemberExp exists
        //    if (forumMemberExp == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    // Remove both the ForumMemberExp and ForumMember
        //    db.ForumMemberExps.Remove(forumMemberExp);
        //    db.ForumMembers.Remove(forumMember);

        //    // Save changes to the database
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}



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
