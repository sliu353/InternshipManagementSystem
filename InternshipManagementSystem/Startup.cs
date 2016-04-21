using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InternshipManagementSystem.Startup))]
namespace InternshipManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
