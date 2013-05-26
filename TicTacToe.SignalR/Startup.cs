using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;
using Microsoft.AspNet.SignalR;

namespace TicTacToe.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            var config = new HubConfiguration 
            { 
                EnableCrossDomain = true,
                EnableJavaScriptProxies = true,
                EnableDetailedErrors = true
            };

            
            app.MapHubs(config);
        }
    }
}
