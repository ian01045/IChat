using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Linq;
using IChat;
using Microsoft.AspNet.SignalR;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(IChat.Startup))]

namespace IChat
{
    public class Startup
    {
        //public void Configuration(IAppBuilder app)
        //{
        //    // 如需如何設定應用程式的詳細資訊，請參閱  http://go.microsoft.com/fwlink/?LinkID=316888
        //}
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}
