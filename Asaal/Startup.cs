using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asaal.Startup))]
namespace Asaal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
