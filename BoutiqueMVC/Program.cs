using System.Threading.Tasks;
using BoutiqueMVC.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoutiqueMVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
               await Startup.PreLoadAsync(scope.ServiceProvider);
            }
            await webHost.RunAsync();
        }

        /// <summary>
        /// Запустить сервис
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).
                    UseStartup<Startup>();
    }
}
