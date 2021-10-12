using System;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Implementation.Logger;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueUserManagement.Infrastructure.Implementations.Services;

namespace BoutiqueUserManagement
{
    public class Program
    {
        /// <summary>
        /// Стартовый метод
        /// </summary>
        public static async Task Main() =>
           await BoutiqueUserService.DeleteUsers(new ConsoleBoutiqueLogger());
    }
}
