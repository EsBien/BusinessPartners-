using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog.Web;
using System.Threading;

namespace MyFirstWebProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("main on Thread with Id: " + Thread.CurrentThread.ManagedThreadId);

            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //.ConfigureLogging((logger) => 
            //{ 
            //    logger.ClearProviders();
            //    logger.SetMinimumLevel(LogLevel.Trace);
            //})
            .UseNLog();
    }
}
