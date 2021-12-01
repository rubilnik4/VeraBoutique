using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы категорий одежды
    /// </summary>
    public interface ICategoryDatabaseValidateService : IDatabaseValidateService<string, ICategoryMainDomain>
    { }
}