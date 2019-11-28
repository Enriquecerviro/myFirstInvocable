using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel;
using CoravelTest.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoravelTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            host.Services.UseScheduler(scheduler =>
            {
                scheduler
                .Schedule<firstWorker>()
                .EveryFiveSeconds();
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScheduler();
                    services.AddTransient<firstWorker>();
                });
    }
}
