using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IChat.Models;

namespace IChat.Controllers
{
    public class AccountController : Controller
    {
        private db_ichatEntities dc = new db_ichatEntities();
        // GET: Account
        public ActionResult Index(user_master _user)
        {
            if (_user.email == null && _user.password == null) return View();
            var user = dc.user_master.Where(u => u.email == _user.email && u.password == _user.password).FirstOrDefault();
            if (user != null)
            {
                Response.Cookies["IChat_user"].Value = user.name;
                string returnUrl = "~/Home/Index";
                if (Request.QueryString["returnUrl"] != null)
                {
                    returnUrl = Request.QueryString["returnUrl"];
                }
                return RedirectToAction("Index", "Home", new { id = user.id });
                //return Redirect(returnUrl);
            }
            else
            {
                ViewBag.message = "帳號密碼錯誤請重新輸入!!";
                return View();
            }
            
        }
    }
}