using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityFramework.Startup))]
namespace IdentityFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
