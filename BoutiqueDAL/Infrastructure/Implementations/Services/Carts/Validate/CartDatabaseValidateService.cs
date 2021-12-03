using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Carts.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы корзин
    /// </summary>
    public class CartDatabaseValidateService : DatabaseValidateService<string, ICartMainDomain, CartEntity>,
                                               ICartDatabaseValidateService
    {
        public CartDatabaseValidateService(ICartTable cartTable)
            : base(cartTable)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(ICartMainDomain cartMain) =>
            new ResultError();
    }
}