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
        Repository<user_master> repository = new Repository<user_master>();
        public ActionResult Index(int id)
        {
            ViewBag.user = repository.GetById(id);
            return View(dc.user_master);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(user_master user)
        {
            user_master _user = new user_master();
            _user.name = user.name;
            _user.phone = user.phone;
            _user.email = user.email;

            repository.Create(_user);

            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id = 0) //這裡的"id"和RoutedConfig內對於路由'url: "{controller}/{action}/{id}"'的顯示管理用的"id"要一樣才能傳值，不然就是後面要接?idx=2用Get方法傳值
        {
            //將id傳給Model
            repository.Delete(repository.GetById(id));

            //轉到Index Action顯示刪除完的結果
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            return View(repository.GetById(id));//把GetById方法回傳的Categories變數_category傳到View
        }

        [HttpPost]
        public ActionResult Edit(user_master user)
        {
            //接收表單傳過來的資料
            user_master _user = new user_master();
            _user.id = user.id;
            _user.name = user.name;
            _user.phone = user.phone;
            _user.email = user.email;

            //將資料傳給Model修改
            repository.Update(_user);

            //轉到Index Action顯示修改完的結果
            return RedirectToAction("Index");
        }


        public ActionResult ChatRoomTemplate()
        {
            return View();
        }
    }
}