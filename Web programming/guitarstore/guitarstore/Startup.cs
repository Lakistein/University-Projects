using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuitarStore.Startup))]
namespace GuitarStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
