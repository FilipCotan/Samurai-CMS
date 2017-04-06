using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Samurai_CMS.Startup))]
namespace Samurai_CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
