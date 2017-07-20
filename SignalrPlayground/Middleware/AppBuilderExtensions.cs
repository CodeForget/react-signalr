using Microsoft.AspNetCore.Builder;
using Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

namespace SignalrPlayground.Middleware
{
    using Microsoft.Owin.Builder;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseAppBuilder(this IApplicationBuilder app, Action<IAppBuilder> configure)
        {
            app.UseOwin(addToPipeline =>
            {
                addToPipeline(next =>
                {
                    var appBuilder = new AppBuilder();
                    appBuilder.Properties["builder.DefaultApp"] = next;
                    appBuilder.UseCors(CorsOptions.AllowAll);

                    configure(appBuilder);

                    return appBuilder.Build<AppFunc>();
                });
            });

            return app;
        }

        public static void UseSignalR2(this IApplicationBuilder app)
        {
            var hubConfiguration = new HubConfiguration
            {
                EnableDetailedErrors = true,
                EnableJSONP = true
            };
            app.UseAppBuilder(appBuilder => appBuilder.MapSignalR("/signalr", hubConfiguration));
        }
    }
}
