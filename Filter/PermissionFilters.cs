using IAAI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Xml;
using IAAI.Controllers;
using IAAI.Migrations;

namespace IAAI.Filter
{
    public class PermissionFilters : ActionFilterAttribute
    {

        IAAIDbContext db = new IAAIDbContext();
        //StringBuilder sbMenu = new StringBuilder();

        public string Module { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{

           

            ////取得UserData
            string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;

            
            Member member = JsonConvert.DeserializeObject<Member>(strUserData);
                string permit = member.Permission;


            filterContext.Controller.ViewBag.name = member.Name.ToString();

                //判斷Controller是否有權限設定
                //bool hasPermission = db.Permissions.Any(p => p.Controller == filterContext.ActionDescriptor.ControllerDescriptor.ControllerName && permit.Contains(p.Controller.ToString()));


            //todo:遞迴組字串
            //跑遞迴 組成ul li 的結構

            StringBuilder sbMenu = new StringBuilder();

                List<Permission> permissions = db.Permissions.ToList(); //將資料放到記憶體 //將物件轉成要的東西
                var roots = permissions.Where(x => x.ParentId == null); //1.找跟節點

                if (permit != null )
                {

                    sbMenu.Append(" <ul class=\"nav\" id=\"main-menu\">");

                    foreach (Permission root in roots) //等於Null的情況去跑裡面的階層
                    {
                        GetTreeData(root, sbMenu, permit); //權限設定  消息管理
                    }

                    sbMenu.Append("</ul>");
                    filterContext.Controller.ViewBag.menu = sbMenu.ToString();

                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                    filterContext.Controller.TempData["ErrorMessage"] = "你沒有權限拜訪此頁面";
                }
            //}

            //base.OnActionExecuting(filterContext);


        }


        private void GetTreeData(Permission node, StringBuilder sbMenu, String permit)
        {



            if (permit.Contains(node.Code.ToString()))
            {

                sbMenu.Append("<li>");

                if (node.ParentId == null)
                {
                    sbMenu.Append(
                        $"<a href=\"{node.Url}\"><i class=\"fa fa-sitemap\"> </i> {node.Subject} <span class=\"fa arrow\"></span></a>");
                }
                else
                {
                    sbMenu.Append($"<a href = \"{node.Url}\">  {node.Subject} </a> ");
                }

                //看有沒有兒子 //有兒子加逗號
                if (node.Permissions.Count > 0)
                {
                    sbMenu.Append("<ul class=\"nav nav-second-level\" > "); //3.

                    //4.走訪全部兒子
                    foreach (Permission childNode in node.Permissions)
                    {
                        GetTreeData(childNode, sbMenu, permit);
                    }

                    sbMenu.Append("</ul>"); //3.
                }

                sbMenu.Append("</li>"); //可能有兒子可能沒兒子  //2.再補中間的
            }
        }

    }
}