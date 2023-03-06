using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;

namespace IAAI.Controllers
{
    public class F_ContactController : Controller
    {
        // GET: F_Contact
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Submit(FormCollection form)
        {
            //// 獲取驗證碼的值
            //string userInputCaptchaCode = Request.Form["CaptchaCode"];
            //// 使用 BotDetect.Mvc 提供的 Validate 方法驗證碼是否正確
            //MvcCaptcha captcha = new MvcCaptcha("validatedigit");

            //if (!captcha.Validate(userInputCaptchaCode))
            //{

            //    // 驗證失敗，將輸入的資料存到 TempData 中
            //    TempData["Name"] = form["Name"];
            //    TempData["Gender"] = form["Gender"];
            //    TempData["Tel"] = form["Tel"];
            //    TempData["Email"] = form["Email"];
            //    TempData["Subject"] = form["Subject"];
            //    TempData["Article"] = form["Article"];

            //    ModelState.AddModelError("MvcCaptcha", "驗證碼不正確");
            //}



            string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
            //string toEmail = Request.Form["Email"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            DateTime now = DateTime.Now;
            string Title = $"{now.ToString("yyyy/MM/dd")}國際縱火調查人員協會臺灣分會詢問信";

            //建立 html 郵件格式
            string body =
                $"<h3>Name : {Request.Form["Name"].Trim()}</h3>" +
                $"<h3>Gender : {Request.Form["Gender"].Trim()}</h3>" +
                $"<h3>Phone : {Request.Form["Tel"].Trim()}</h3>" +
                $"<h3>Email : {Request.Form["Email"].Trim()}</h3>" +
                $"<h3>Subject : {Request.Form["Subject"].Trim()}</h3>" +
                $"<h3>Article : </h3>" +
                $"<p>{Request.Form["Article"].Trim()}</p>";

            SendGmailMail(fromEmail, fromEmail, Title, body, password);

            ViewBag.Message = "Thank you for contacting us!";

            return View("Index");
        }

        public static void SendGmailMail(string fromAddress, string toAddress, string Subject, string MailBody,
            string password)
        {
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = MailBody;
            // SMTP Server
            SmtpClient mailSender = new SmtpClient("smtp.gmail.com");
            System.Net.NetworkCredential basicAuthenticationInfo =
                new System.Net.NetworkCredential(fromAddress, password);
            mailSender.Credentials = basicAuthenticationInfo;
            mailSender.Port = 587;
            mailSender.EnableSsl = true;
            mailSender.Send(mailMessage);
            mailMessage.Dispose();
            mailSender = null;
        }
    }
}