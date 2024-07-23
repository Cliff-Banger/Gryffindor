using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gryffindor.Web.Startup))]
namespace Gryffindor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
