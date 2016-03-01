using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleMIM.Web.Startup))]
namespace SimpleMIM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
