using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovingMountains.Startup))]
namespace MovingMountains
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
