using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Library_Managment_Project.Startup))]
namespace Library_Managment_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
