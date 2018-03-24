using System;
using System.Web;
using SimpleInjector;
using WebFormsDi.Infrastructure;
using WebFormsDi.Models;

namespace WebFormsDi
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var container = new Container();
            container.RegisterSingleton<IMessageService, InjectedMessageService>();
            container.Verify();
            Context.SetContainer(container);
        }
    }
}
