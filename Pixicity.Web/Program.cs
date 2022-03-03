using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Pixicity.Domain.Helpers;

namespace Pixicity.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IOHelper.CreateDirectory("wwwroot/images/avatars");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
