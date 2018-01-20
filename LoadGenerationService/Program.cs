using System.Net;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LoadGeneratorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                //.UseKestrel(options =>
                //{
                //    options.Listen(IPAddress.Loopback, 5050);
                //    options.Limits.MaxConcurrentConnections = 1000;
                //})
                .UseKestrel()
                .Build();
    }
}
