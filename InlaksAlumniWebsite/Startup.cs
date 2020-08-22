using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InlaksAlumniWebsite.Startup))]
namespace InlaksAlumniWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
