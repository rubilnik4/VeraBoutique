using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Models.Implementations.Entities.Carts;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Carts
{
    /// <summary>
    /// Таблица корзины
    /// </summary>
    public interface ICartTable : IDatabaseTable<string, ICartDomain, CartEntity>
    { }
}