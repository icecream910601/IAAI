using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IAAI.Models;
using Newtonsoft.Json;

namespace IAAI.Filter
{
    public class PagePermission : ActionFilterAttribute
    {
        IAAIDbContext db = new IAAIDbContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //取得當前執行的Action所屬的Controller名稱，以便後續權限檢查時使用。
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var permission = GetCurrentMemberPermission(); // 假設可以取得當前使用者的權限值
            var hasPermission = CheckPermission(permission, controllerName);
            if (!hasPermission)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
                filterContext.Controller.TempData["ErrorMessage"] = "你沒有權限拜訪此頁面";
            }
        }

        private string GetCurrentMemberPermission()
        {
            string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            Member member = JsonConvert.DeserializeObject<Member>(strUserData);
            return member.Permission; // 假設Permission欄位是儲存使用者權限值的欄位名稱
        }

        
        private bool CheckPermission(string permission, string controllerName)
        {
            // 1. 取得該人的 Permission 欄位值
            string[] permissions = permission.Split(',');

            // 2. 在 Permission 資料表中，透過 LINQ 查詢，找到與該人的 Permission 相對應的 Controller

            var result = db.Permissions
                .FirstOrDefault(p => permissions.Contains(p.Code) && p.Controller == controllerName);

            // 3. 若找到該 Controller，代表該人有權限訪問相關頁面；反之則沒有
            return result != null;
        }
    }
}