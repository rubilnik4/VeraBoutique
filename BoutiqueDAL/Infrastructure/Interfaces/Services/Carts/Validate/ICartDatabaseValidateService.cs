using BoutiqueCommon.Models.Domain.Interfaces.Carts;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Carts.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы корзин
    /// </summary>
    public interface ICartDatabaseValidateService : IDatabaseValidateService<string, ICartMainDomain>
    { }
}