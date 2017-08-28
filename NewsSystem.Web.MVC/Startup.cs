using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsSystem.Web.MVC.Startup))]
namespace NewsSystem.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
