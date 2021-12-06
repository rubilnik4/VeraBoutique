using System;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Models.Implementations.Entities.Carts;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Carts
{
    /// <summary>
    /// Таблица позиций в корзине
    /// </summary>
    public interface ICartItemTable : IDatabaseTable<Guid, ICartItemDomain, CartItemEntity>
    { }
}