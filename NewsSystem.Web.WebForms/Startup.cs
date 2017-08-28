using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsSystem.Web.WebForms.Startup))]
namespace NewsSystem.Web.WebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
