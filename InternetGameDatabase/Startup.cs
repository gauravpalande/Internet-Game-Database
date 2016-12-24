using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InternetGameDatabase.Startup))]
namespace InternetGameDatabase
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
