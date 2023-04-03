using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IAAI.Filter;
using IAAI.Models;
using Newtonsoft.Json;

namespace IAAI.Areas.Manager.Controllers
{
    //[PagePermission]
    //[PermissionFilters]
    //[Authorize]

    public class MembersController : Controller
    {
        private StringBuilder sbTree = new StringBuilder();

        IAAIDbContext db = new IAAIDbContext();

        [AllowAnonymous]
        // GET: Login/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                //db.ViewLogins.Add(viewLogin);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                Member member = db.Members.FirstOrDefault(x => x.Account == viewLogin.Account);
                if (member == null)
                {
                    ViewBag.message = "無此帳號!";
                    return View(viewLogin);
                }

                string password = Utility.GenerateHashWithSalt(viewLogin.Password, member.Salt);
                if (password != member.Password)
                {
                    ViewBag.message = "密碼錯誤!";
                    return View(viewLogin);
                }


                //Utility.GetPerssion(member);
                string Userdata = JsonConvert.SerializeObject(member);
                Utility.SetAuthenTicket(Userdata, member.Account);

                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View(viewLogin);
        }

        [PermissionFilters]

        // GET: Members

        public ActionResult Index()
        {

            string strUserData = ((FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket.UserData;
            Member member = JsonConvert.DeserializeObject<Member>(strUserData);
            string account = member.Account;
            ViewBag.account = account;

            return View(db.Members.ToList());
        }

        [PermissionFilters]
        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [PermissionFilters]
        // GET: Members/Create
        public ActionResult Create()
        {
            List<Permission> permissions = db.Permissions.ToList();  //將資料放到記憶體 //將物件轉成要的東西
            var roots = permissions.Where(x => x.ParentId == null);  //1.找跟節點

            sbTree.Append("[");
            foreach (Permission root in roots)
            {
                GetTreeData(root);  //權限設定  消息管理
                sbTree.Append(",");
            }
            sbTree.Append("]");
            ViewBag.tree = sbTree.ToString();
            return View();
        }

        private void GetTreeData(Permission node)
        {
            sbTree.Append($@"{{'id':'{node.Code}','text':'{node.Subject}'");  //2.

            //看有沒有兒子 //有兒子加逗號
            if (node.Permissions.Count > 0)
            {
                sbTree.Append(",'children':[");  //3.

                //4.走訪全部兒子
                foreach (Permission childNode in node.Permissions)
                {
                    GetTreeData(childNode);
                    sbTree.Append(",");
                }

                sbTree.Append("]");  //3.
            }

            sbTree.Append("}"); //可能有兒子可能沒兒子  //2.再補中間的

        }

        [PermissionFilters]
        // POST: Members/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            var existingMember = db.Members.Where(m => m.Account == member.Account).FirstOrDefault();

            if (ModelState.IsValid && existingMember == null)
            {

                member.Salt = Utility.CreateSalt();
                member.Password = Utility.GenerateHashWithSalt(member.Password, member.Salt);

                //member.HashPassword();
                db.Members.Add(member);
                db.SaveChanges();
                ViewBag.Msg = "註冊成功";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "格式不正確/此帳號已註冊過";

                //顯示
                List<Permission> permissions = db.Permissions.ToList();  //將資料放到記憶體 //將物件轉成要的東西
                var roots = permissions.Where(x => x.ParentId == null);  //1.找跟節點

                sbTree.Append("[");
                foreach (Permission root in roots)
                {
                    GetTreeData(root);  //權限設定  消息管理
                    sbTree.Append(",");
                }
                sbTree.Append("]");
                ViewBag.tree = sbTree.ToString();

                return View();

            }

        }

        [PermissionFilters]
        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);

            if (member == null)
            {
                return HttpNotFound();
            }


            List<Permission> permissions = db.Permissions.ToList();  //將資料放到記憶體 //將物件轉成要的東西
            var roots = permissions.Where(x => x.ParentId == null);  //1.找跟節點

            sbTree.Append("[");
            foreach (Permission root in roots)
            {
                GetTreeData(root);  //全縣設定  消息管理
                sbTree.Append(",");
            }
            sbTree.Append("]");
            ViewBag.tree = sbTree.ToString();


            return View(member);
        }

        [PermissionFilters]
        // POST: Members/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member, string newPassword)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(newPassword))
                {
                    member.Password = Utility.GenerateHashWithSalt(newPassword, member.Salt);
                }

                //member.HashPassword();
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        [PermissionFilters]
        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);

            string strUserData = ((FormsIdentity)(System.Web.HttpContext.Current.User.Identity)).Ticket.UserData;
            Member member1 = JsonConvert.DeserializeObject<Member>(strUserData);
            if (member1.Account == member.Account)
            {
                return RedirectToAction("Index");
            }



            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [PermissionFilters]
        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
