using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IChat.Models;

namespace IChat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        db_ichatEntities dc = new db_ichatEntities();
        public ActionResult Index()
        {
            return View(dc.user_master);
        }
    }
}