using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;


namespace IChat.Hubs
{
    public class ChatHub : Hub
    {
        //public void Hello()
        //{
        //    Clients.All.hello();
        //}

        public void Send(string userId, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(userId, message);
            //string id_name = Context.User.Identity.Name;
            //Clients.Group(id_name).addChatMessage(id_name, message);
            //Clients.Client(userId).send(message);
            Clients.User(userId).send(message);
            //Clients.All.send(userId,message);
        }

        public string GetUserId(string userId)
        {
            var id = Context.ConnectionId;
            return id; 
        }
        //public override Task OnConnected()
        //{
        //    string name = Context.User.Identity.Name
        //    Groups.Add(Context.ConnectionId, name);
        //    return base.OnConnected();
        //}
    }
}