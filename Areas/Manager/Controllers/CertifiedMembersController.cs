using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IAAI.Filter;
using IAAI.Models;
using Newtonsoft.Json.Linq;

namespace IAAI.Areas.Manager.Controllers
{
    [PagePermission]
    [PermissionFilters]
    [Authorize]
    public class CertifiedMembersController : Controller
    {
        private IAAIDbContext db = new IAAIDbContext();

        // GET: Manager/CertifiedMembers
        public ActionResult Index()
        {
            return View(db.Certified.ToList());
        }

        // GET: Manager/CertifiedMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.Certified.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        //網頁一起動就取得資料
        // GET: CertifiedMembers/Create
        public async Task<ActionResult> Create()
        {
            //StringBuilder dropList = new StringBuilder();
            string url = "https://www.mofa.gov.tw/OpenDataForm.aspx?s=A37697969F737464";
            HttpClient client = new HttpClient(); //
            var response = await client.GetAsync(url); //
            var jsonString = await response.Content.ReadAsStringAsync(); //
            var jsonData = JArray.Parse(jsonString);//

            //下拉選單顯示 
            List<SelectListItem> enNameList = new List<SelectListItem>();
            foreach (var countries in jsonData)
            {
                var enName = (string)countries["EnSortName"];
                enNameList.Add(new SelectListItem() { Text = enName, Value = enName });
            }

            ViewBag.enName = enNameList;
            return View();
        }


        // POST: Manager/CertifiedMembers/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CertifiedMember certifiedMember, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(certifiedMember);
                    }

                    certifiedMember.Picture = Utility.SaveUpImage(Picture);
                }

                db.Certified.Add(certifiedMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(certifiedMember);
        }

        // GET: Manager/CertifiedMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.Certified.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // POST: Manager/CertifiedMembers/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( CertifiedMember certifiedMember, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(certifiedMember);
                    }

                    certifiedMember.Picture = Utility.SaveUpImage(Picture);
                }
                db.Entry(certifiedMember).State = EntityState.Modified;
                db.SaveChanges();
            }

            //return View(certifiedMember);
            return RedirectToActionPermanent("Index", null,
                new { page = Request["page"] });
        }

        // GET: Manager/CertifiedMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertifiedMember certifiedMember = db.Certified.Find(id);
            if (certifiedMember == null)
            {
                return HttpNotFound();
            }
            return View(certifiedMember);
        }

        // POST: Manager/CertifiedMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CertifiedMember certifiedMember = db.Certified.Find(id);
            db.Certified.Remove(certifiedMember);
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
