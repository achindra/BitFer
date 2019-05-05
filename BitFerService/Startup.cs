using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BitFerService.Startup))]

namespace BitFerService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}