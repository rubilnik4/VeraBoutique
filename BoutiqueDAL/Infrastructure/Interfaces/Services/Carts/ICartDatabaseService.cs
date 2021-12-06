using System;
using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Carts
{
    /// <summary>
    /// Сервис корзин в базе данных
    /// </summary>
    public interface ICartDatabaseService : IDatabaseService<Guid, ICartMainDomain>
    { }
}