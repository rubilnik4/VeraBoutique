using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы одежды
    /// </summary>
    public interface IClothesDatabaseValidateService : IDatabaseValidateService<int, IClothesMainDomain>
    { }
}