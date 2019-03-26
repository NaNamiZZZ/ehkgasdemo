using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EhkBackend.ui.Startup))]
namespace EhkBackend.ui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
