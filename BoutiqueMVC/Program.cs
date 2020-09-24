using System;
using System.Threading.Tasks;
using BoutiqueMVC.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BoutiqueMVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await RunWebHost(args);
        }

        /// <summary>
        /// Запустить сервер
        /// </summary>
        private static async Task RunWebHost(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
                await Startup.PreLoadAsync(scope.ServiceProvider);
            }
            await webHost.RunAsync();
        }

        /// <summary>
        /// Опередить параметры запуска
        /// </summary>
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.
            CreateDefaultBuilder(args).
            UseStartup<Startup>();
    }
}
