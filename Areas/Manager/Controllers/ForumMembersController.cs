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

    public class ForumMembersController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: /Manager/ForumMembers
        public ActionResult Index()
        {

            return View(db.ForumMembers.ToList());
        }



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
            while (expList.Count() < 3) // 檢查 expList 數量是否足夠，如果不足則新增空白的 ForumMemberExp 物件
            {
                expList.Add(new ForumMemberExp { ForumMemberId = forumMemberId.Value });
            }
            ViewBag.MemberExp = expList;

            return View(forumMember);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ForumMember forumMember, List<ForumMemberExp> forumMemberExp, HttpPostedFileBase Picture, string newPassword, string newConfirmedPassword)
        {
            var expList = db.ForumMemberExps.Where(fme => fme.ForumMemberId == forumMember.Id).ToList();

            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(forumMember);
                    }

                    forumMember.Copy = CertifiedMember.SaveUpImage(Picture);
                }

                if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(newConfirmedPassword))
                {
                    forumMember.HashPassword();
                    forumMember.HashConfirmedPassword();
                }

                // 更新會員資訊
                db.Entry(forumMember).State = EntityState.Modified;

                // 刪除原本的會員經驗資料
                //
                foreach (var exp in expList)
                {
                    db.ForumMemberExps.Remove(exp);
                }

                // 新增新的會員經驗資料
                foreach (var exp in forumMemberExp)
                {
                    exp.ForumMemberId = forumMember.Id;
                    db.ForumMemberExps.Add(exp);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // 如果有錯誤發生，重新載入經驗資料並返回編輯頁面
            //
            ViewBag.MemberExp = expList;

            return View(forumMember);
        }


        // GET: /Manager/ForumMembers/Details/5
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

            List<ForumMemberExp> expList = db.ForumMemberExps.Where(fme => fme.ForumMemberId == forumMemberId).ToList();
            while (expList.Count() < 3) // 檢查 expList 數量是否足夠，如果不足則新增空白的 ForumMemberExp 物件
            {
                expList.Add(new ForumMemberExp { ForumMemberId = forumMemberId.Value });
            }
            ViewBag.MemberExp = expList;


            return View(forumMember);
        }

        // GET: /Manager/ForumMembers/Delete/5
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

            List<ForumMemberExp> expList = db.ForumMemberExps.Where(fme => fme.ForumMemberId == forumMemberId).ToList();
            ViewBag.MemberExp = expList;

            return View(forumMember);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? forumMemberId)
        {
            ForumMember forumMember = db.ForumMembers.Find(forumMemberId);
            if (forumMember == null)
            {
                return HttpNotFound();
            }

            // Find all the ForumMemberExps to delete
            var forumMemberExps = db.ForumMemberExps.Where(f => f.ForumMemberId == forumMemberId).ToList();

            // Remove all the ForumMemberExps
            foreach (var exp in forumMemberExps)
            {
                // Set the ForumMemberId to null
                exp.ForumMemberId = null;

                // Remove the ForumMemberExp
                db.ForumMemberExps.Remove(exp);
            }

            // Remove the ForumMember
            db.ForumMembers.Remove(forumMember);

            // Save changes to the database
            db.SaveChanges();

            return RedirectToAction("Index", "ForumMembers");
        }



    }
}
