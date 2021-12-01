using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы цвета одежды
    /// </summary>
    public interface IColorClothesDatabaseValidateService : IDatabaseValidateService<string, IColorDomain>
    { }
}