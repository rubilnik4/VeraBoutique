using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Infrastructure.Interfaces.Carts
{
    /// <summary>
    /// Сервис корзины
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Создать корзину
        /// </summary>
        Task<IResultValue<ICartDomain>> CreateCart();
    }
}