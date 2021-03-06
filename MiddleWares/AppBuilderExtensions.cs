

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace ProjeCoreOrnekOzellikler.MiddleWares
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app,string root)
        {
            string path = Path.Combine(root, @"node_modules");

            var provider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = @"/node_modules";
            options.FileProvider = provider;

            app.UseStaticFiles(options);

            return app;
        }
    }
}
