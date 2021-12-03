using BoutiqueMVC.Infrastructure.Implementation.Carts;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    public static class ServicesRegistration
    {
        /// <summary>
        /// Внедрить зависимости сервисов
        /// </summary>
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICartService, CartService>();
        }
    }
}