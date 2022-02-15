using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Goose.WebMVC.Startup))]
namespace Goose.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
