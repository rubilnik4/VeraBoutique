using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using Microsoft.AspNetCore.Components;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Infrastructure.Implementation.Carts
{
    /// <summary>
    /// Сервис корзины
    /// </summary>
    public class CartService: ICartService
    {
        public CartService(ICartDatabaseService cartDatabaseService)
        {
            _cartDatabaseService = cartDatabaseService;
        }

        /// <summary>
        /// Сервис корзин в базе данных
        /// </summary>
        private readonly ICartDatabaseService _cartDatabaseService;

        /// <summary>
        /// Создать корзину
        /// </summary>
        public async Task<IResultValue<ICartDomain>> CreateCart() =>
            await new CartMainDomain(Guid.Empty, Enumerable.Empty<ICartItemDomain>()).
            MapAsync(cart => _cartDatabaseService.Post(cart)).
            ResultValueBindOkBindAsync(id => _cartDatabaseService.Get(id));
    }
}