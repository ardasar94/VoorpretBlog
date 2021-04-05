using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VoorpretBlog.Startup))]
namespace VoorpretBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
