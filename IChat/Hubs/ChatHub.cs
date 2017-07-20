using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using IChat.Models;


namespace IChat.Hubs
{
    public class ChatHub : Hub
    {
        private db_ichatEntities dc = new db_ichatEntities();
        public class User
        {
            public string ConnectionID { get; set; }
            public string Name { get; set; }
            public User(string name, string connectionId)
            {
                this.Name = name;
                this.ConnectionID = connectionId;
            }
        }
        public static List<User> users = new List<User>();

        //public void SendClient(string name, string message, string connId)
        //{
        //    var user = users.Where(s => s.ConnectionID == connId).FirstOrDefault();
        //    if (user != null)
        //    {
        //        Clients.Client(connId).addMessage(name, message);
        //        Clients.Client(Context.ConnectionId).addMessage(name, message);
        //    }
        //    else
        //    {
        //        Clients.Client(Context.ConnectionId).showMessage("該用戶已離線!");
        //    }
        //}

        //发送消息  
        public void SendMessage(string connectionId, string message)
        {
            Clients.All.hello();
            var user = users.Where(s => s.ConnectionID == connectionId).FirstOrDefault();
            if (user != null)
            {
                Clients.Client(connectionId).addMessage(message, Context.ConnectionId, false);
                //给自己发送，把用户的ID传给自己  
                Clients.Client(Context.ConnectionId).addMessage(message, connectionId, true);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("该用户已离线");
            }
        }


        public void GetName(string name)
        {
            //查询用户  
            var user = users.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId);
            if (user != null)
            {
                //user.Name = name;
                //Clients.Client(Context.ConnectionId).showId(Context.ConnectionId);
                var user_db = dc.user_master.Where(u => u.name == name).FirstOrDefault();
                if (user_db != null)
                {
                    user_db.IsOnline = true;
                    user_db.ConnectionId = Context.ConnectionId;
                }
                dc.SaveChanges();
            }
            GetUsers();
        }
        /// <summary>  
        /// 重写连接事件  
        /// </summary>  
        /// <returns></returns>  
        public override Task OnConnected()
        {
            //查询用户  
            var user = users.Where(w => w.ConnectionID == Context.ConnectionId).SingleOrDefault();
            //判断用户是否存在，否则添加集合  
            if (user == null)
            {

                user = new User("", Context.ConnectionId);
                users.Add(user);
            }
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = users.Where(p => p.ConnectionID == Context.ConnectionId).FirstOrDefault();
            //判断用户是否存在，存在则删除  
            if (user != null)
            {
                var user_db = dc.user_master.Where(u => u.ConnectionId == Context.ConnectionId).FirstOrDefault();
                if (user_db != null)
                {
                    user_db.IsOnline = false;
                    user_db.ConnectionId = "";
                }
                dc.SaveChanges();
                //删除用户  
                users.Remove(user);
            }
            GetUsers();//获取所有用户的列表  
            return base.OnDisconnected(stopCalled);
        }
        //获取所有用户在线列表  
        private void GetUsers()
        {
            //var list = users.Select(s => new { s.Name, s.ConnectionID }).ToList();
            //string jsonList = JsonConvert.SerializeObject(list);
            //Clients.All.getUsers(jsonList);

            var users = dc.user_master.ToList();
            Clients.All.getUsers(users);
        }

        //public string GetUserId(string userId)
        //{
        //    var id = Context.ConnectionId;
        //    return id; 
        //}
        //public override Task OnConnected()
        //{
        //    string name = Context.User.Identity.Name
        //    Groups.Add(Context.ConnectionId, name);
        //    return base.OnConnected();
        //}
    }
}