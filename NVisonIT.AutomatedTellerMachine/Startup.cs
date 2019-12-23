using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NVisonIT.AutomatedTellerMachine.Startup))]
namespace NVisonIT.AutomatedTellerMachine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}