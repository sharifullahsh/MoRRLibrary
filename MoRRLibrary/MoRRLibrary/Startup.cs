using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoRRLibrary.Startup))]
namespace MoRRLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
