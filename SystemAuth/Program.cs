using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace SystemAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseKestrel(options =>
                              {
                                  options.Listen(IPAddress.Any, 5080);
                                  options.Listen(IPAddress.Any, 5443, listenOptions =>
                                  {
                                      listenOptions.UseHttps("backend.pfx", "2ooixuui");
                                  });
                              })
                              .UseUrls("https://0.0.0.0:5443");
                });
    }
}
