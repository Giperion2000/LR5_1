using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LR5_1.Startup))]
namespace LR5_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
