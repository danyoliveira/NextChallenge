using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NextChallenge.Startup))]

namespace NextChallenge {
    public partial class Startup {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
