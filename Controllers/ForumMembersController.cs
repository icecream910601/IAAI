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
    public class ForumMembersController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();


        // GET: Login/Create
        public ActionResult ForumLogin()
        {
            return View();
        }


        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForumLogin(ViewForumMemberLogin viewForumMemberLoginLogin)
        {
            if (ModelState.IsValid)
            {
                viewForumMemberLoginLogin.HashPassword();

                ForumMember forumMember = db.ForumMembers.FirstOrDefault(x => x.Account == viewForumMemberLoginLogin.Account && x.Password == viewForumMemberLoginLogin.Password);
                if (forumMember != null) // 如果帳號和密碼正確
                {
                    Session["Id"] = forumMember.Id;
                    Session["Name"] = forumMember.Name;
                    Session["Account"] = viewForumMemberLoginLogin.Account; // 將使用者帳號存到 Session 中
                    return RedirectToAction("Index", "Forum"); // 重定向到論壇會員列表頁面
                }
                else // 如果帳號和密碼不正確
                {
                    ViewBag.message = "帳號或密碼不正確!"; // 增加錯誤訊息
                }
            }

            return View(viewForumMemberLoginLogin);
        }

        public ActionResult Logout()
        {
            // 清除 Session 中的資料
            Session.Clear();
            Session.Abandon();

            // 重新導向至首頁
            return RedirectToAction("ForumLogin", "ForumMembers");
        }


        // GET: F_ForumMembers
        public ActionResult Index()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("ForumLogin", "ForumMembers"); // 重定向到登入頁面
            }

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

            List<ForumMemberExp> expList = db.ForumMemberExps.Where(fme => fme.ForumMemberId == forumMemberId).ToList();
            while (expList.Count() < 3) // 檢查 expList 數量是否足夠，如果不足則新增空白的 ForumMemberExp 物件
            {
                expList.Add(new ForumMemberExp { ForumMemberId = forumMemberId.Value });
            }
            ViewBag.MemberExp = expList;


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
        public ActionResult Create(ForumMember forumMember, List<ForumMemberExp> forumMemberExp, HttpPostedFileBase Picture)
        {
            var existingMember = db.ForumMembers.Where(m => m.Account == forumMember.Account).FirstOrDefault();

            if (ModelState.IsValid && existingMember == null)
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

                forumMember.HashPassword();
                forumMember.HashConfirmedPassword();

                db.ForumMembers.Add(forumMember);
                db.SaveChanges();


                foreach (var exp in forumMemberExp)
                {
                    exp.ForumMemberId = forumMember.Id;

                    if (exp.HistoryUnit != null && exp.HistoryJobTitle != null && exp.StartYear != null && exp.StartMonth != null && exp.EndYear != null && exp.EndMonth != null)
                    {
                        db.ForumMemberExps.Add(exp);
                    }

                }


                db.SaveChanges();

                return RedirectToAction("ForumLogin");
            }
            else
            {
                ViewBag.Msg = "格式不正確/此帳號已註冊過";
            }

            return View();
        }



        // GET: F_ForumMembers/Edit/5
        //public ActionResult Edit(int? forumMemberId)
        //{
        //    if (forumMemberId == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ForumMember forumMember = db.ForumMembers.Find(forumMemberId);
        //    if (forumMember == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    List<ForumMemberExp> expList = db.ForumMemberExps.Where(fme => fme.ForumMemberId == forumMemberId).ToList();
        //    ViewBag.MemberExp = expList;

        //    return View(forumMember);
        //}
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

        // GET: F_ForumMembers/Delete/5
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






        //POST: F_ForumMembers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ForumMember forumMember = db.ForumMembers.Find(id);
        //    db.ForumMembers.Remove(forumMember);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
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
