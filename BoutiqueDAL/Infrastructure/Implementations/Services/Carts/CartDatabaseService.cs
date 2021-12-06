using System;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Carts.Validate;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Carts
{
    /// <summary>
    /// Сервис корзины в базе данных
    /// </summary>
    public class CartDatabaseService : DatabaseService<Guid, ICartMainDomain, CartEntity>, ICartDatabaseService
    {
        public CartDatabaseService(IBoutiqueDatabase boutiqueDatabase,
                                   ICartDatabaseValidateService cartDatabaseValidateService,
                                   ICartMainEntityConverter cartMainEntityConverter)
            : base(boutiqueDatabase, boutiqueDatabase.CartTable, cartDatabaseValidateService, cartMainEntityConverter)
        { }
    }
}