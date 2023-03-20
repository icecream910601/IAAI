using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Models;
using MvcPaging;

namespace IAAI.Controllers
{
    public class ForumController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        private const int DefaultPageSize = 15;  //一頁有幾筆

        // GET: F_Forum
        public ActionResult Index(int? page)
        {
            //var forum = db.Forum.Include(f => f.ForumMember);
            //return View(forum.ToList());

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            if (Session["Account"] != null)
            {
                List<Forum> forums = db.Forum.Include(f => f.ForumMember).ToList();
                foreach (var forum in forums)
                {
                    forum.ForumReplys = db.ForumReplies.Where(r => r.ForumId == forum.Id).ToList();
                    int responseCount = forum.ForumReplys.Count;
                    ViewData[$"ResponseCount_{forum.Id}"] = responseCount;

                    var latestReply = forum.ForumReplys.OrderByDescending(r => r.InitDate).FirstOrDefault();
                    string latestResponderName = latestReply?.ForumMember?.Name ?? "無";
                    ViewData[$"LatestResponder_{forum.Id}"] = latestResponderName;
                }
                ViewBag.listForums = forums;
                return View(forums.OrderBy(x => x.InitDate).ToPagedList(currentPageIndex, DefaultPageSize));
            }
            else
            {
                return RedirectToAction("ForumLogin", "ForumMembers");
            }
        }



        // GET: F_Forum/Details/5
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

            List<ForumReply> replyList = db.ForumReplies.Where(fme => fme.ForumId ==id).ToList();
            ViewBag.ReplyList = replyList;

            return View(forum);
        }

        // GET: F_Forum/Create
        public ActionResult Create()
        {
            ViewBag.ForumMemberId = new SelectList(db.ForumMembers, "Id", "Account");
            return View();
        }

        // POST: F_Forum/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Header,Main,InitDate,ForumMemberId")] Forum forum)
        {
            if (ModelState.IsValid)
            {
                forum.ForumMemberId = (int)Session["Id"];
                db.Forum.Add(forum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ForumMemberId = new SelectList(db.ForumMembers, "Id", "Account", forum.ForumMemberId);
            return View(forum);
        }

        // GET: F_Forum/Edit/5
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

        // POST: F_Forum/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,Main,InitDate,ForumMemberId")] Forum forum)
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

        // GET: F_Forum/Delete/5
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

        // POST: F_Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forum.Find(id);
            db.Forum.Remove(forum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Get
        public ActionResult Reply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var forum = db.Forum.Find(id);

            if (forum == null)
            {
                return HttpNotFound();
            }

            var forumReply = new ForumReply
            {
                ForumId = forum.Id,
                Forum = forum
            };

            return View(forumReply);
        }



        //Post
        //public ActionResult ReplyTo(ForumReply forumReply)
        //{
        //    forumReply.ForumMemberId= (int)Session["Id"];
        //    forumReply.InitDate = DateTime.Now;
        //    return RedirectToAction("Details");
        //}


        /// POST: YourController/ReplyTo
        [HttpPost]
        public ActionResult ReplyTo(ForumReply forumReply, int? forumId, string Main)
        {
            // 從 Session 中獲取當前使用者的會員 ID
            int memberId = (int)Session["Id"];

            // 根據 replyId 從資料庫中查詢要回覆的討論
            var forum = db.Forum.Find(forumId);

            // 創建一個新的回覆區物件，並填入相應的屬性
            forumReply = new ForumReply
            {
                Main = Main,
                Header = forum.Header,
                ForumId = forumId,
                ForumMemberId = memberId,
                InitDate = DateTime.Now,
            };

            // 將回覆區物件寫入資料庫或其他儲存位置
            db.ForumReplies.Add(forumReply);
            db.SaveChanges();

            // 返回到原始討論頁面或其他頁面
            return RedirectToAction("Details", "Forum", new { id = forumId });
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
