﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAAI.Filter;
using IAAI.Models;
using MvcPaging;

namespace IAAI.Areas.Manager.Controllers
{
    [PagePermission]
    [PermissionFilters]
    [Authorize]
    public class NewsController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        private const int DefaultPageSize = 15; //一頁有幾筆

        // GET: /Manager/News
        public ActionResult Index(int? page, string searchString)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0; //沒有頁數時如何處理
            var news = db.News.Include(n => n.MyCatalog);

            //
            if (!string.IsNullOrEmpty(searchString))
            {
                news = news.Where(n => n.Subject.Contains(searchString) || n.Content.Contains(searchString));
            }


            List<News> listNews = news.ToList();
            ViewBag.listNews = listNews;

            //
            ViewBag.SearchString = searchString;

            return View(news.OrderBy(x => x.StartDate).ToPagedList(currentPageIndex, DefaultPageSize));

            //var news = db.News.Include(n => n.MyCatalog);
            //return View(news.ToList());
        }

        // GET:/Manager/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        // GET:/Manager/News/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.NewsCatalogs, "Id", "Class");
            return View();
        }

        // POST:/Manager/News/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] // 允許 HTML 標記或輸入
        public ActionResult Create([Bind(Include = "Id,Picture,StartDate,Subject,Content,ClassId")] News news,
            HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(news);
                    }

                    news.Picture =Utility.SaveUpImage(Picture);
                }

                news.Content = Request.Form["Content"];

                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.NewsCatalogs, "Id", "Class", news.ClassId);
            return View(news);
        }

        // GET: /Manager/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            // 取得 content 資料
            string content = news.Content;

            // 放入 ViewBag 中
            ViewBag.Content = content;


            ViewBag.ClassId = new SelectList(db.NewsCatalogs, "Id", "Class", news.ClassId);
            return View(news);
        }

        // POST: /Manager/News/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)] // 允許 HTML 標記或輸入
        public ActionResult Edit([Bind(Include = "Id,Picture,StartDate,Subject,Content,ClassId")] News news,
            HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(news);
                    }

                    news.Picture = Utility.SaveUpImage(Picture);
                }

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassId = new SelectList(db.NewsCatalogs, "Id", "Class", news.ClassId);
            return RedirectToActionPermanent("Index", null, new { page = Request["page"] });
        }


        // GET: /Manager/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        // POST:/Manager/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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