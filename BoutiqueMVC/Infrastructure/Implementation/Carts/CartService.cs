using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Implementation.Validation.Common;
using BoutiqueCommon.Models.Domain.Implementations.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts;
using BoutiqueMVC.Infrastructure.Interfaces.Carts;
using Microsoft.AspNetCore.Components;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Infrastructure.Implementation.Carts
{
    /// <summary>
    /// Сервис корзины
    /// </summary>
    public class CartService : ICartService
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
        public async Task<IResultValue<ICartDomain>> CreateCart(string authorId) =>
            await authorId.ToResultValueWhere(EmailValidation.IsValid,
                                              email => ErrorResultFactory.ValueNotValidError(email, GetType(), "Неправильный адрес почты при создании корзины")).
            ResultValueOk(email => new CartMainDomain(Guid.Empty, DateTime.Now, email, Enumerable.Empty<ICartItemDomain>())).
            ResultValueBindOkAsync(cart => _cartDatabaseService.Post(cart)).
            ResultValueBindOkBindAsync(id => _cartDatabaseService.Get(id));
    }
}