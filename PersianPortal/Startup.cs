using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersianPortal.Startup))]
namespace PersianPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
