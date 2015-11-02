using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Obligatorisk1.Startup))]
namespace Obligatorisk1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
