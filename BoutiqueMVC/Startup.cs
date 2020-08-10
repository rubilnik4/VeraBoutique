using BoutiqueMVC.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoutiqueMVC
{
    public class Startup
    {
        /// <summary>
        /// ��������� ������������
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ����������� ������������
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            NHibernateInjection.ConfigureServices(services, Configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// ���������
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
