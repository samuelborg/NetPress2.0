using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetPress.Startup))]
namespace NetPress
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
