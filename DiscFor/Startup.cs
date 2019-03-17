using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiscFor.Startup))]
namespace DiscFor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
