using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IAAI.Filter;

namespace IAAI.Controllers
{
    [PermissionFilters]
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            //// 清除 Session
            //Session.Clear();

            //// 清除 Cookie
            //HttpCookie cookie = Request.Cookies["Userdata"];
            //if (cookie != null)
            //{
            //    cookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(cookie);
            //}

            FormsAuthentication.SignOut();
            Session.Abandon();

            // 重新導向到登入頁面
            return RedirectToAction("Login", "B_Members");
           
        }
    }
}