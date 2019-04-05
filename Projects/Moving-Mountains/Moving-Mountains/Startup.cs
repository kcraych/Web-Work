using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Moving_Mountains.Startup))]
namespace Moving_Mountains
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
