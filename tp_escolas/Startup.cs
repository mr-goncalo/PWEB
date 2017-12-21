using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tp_escolas.Startup))]
namespace tp_escolas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
