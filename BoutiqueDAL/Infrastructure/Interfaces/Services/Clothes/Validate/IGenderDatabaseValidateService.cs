using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы пола одежды
    /// </summary>
    public interface IGenderDatabaseValidateService : IDatabaseValidateService<GenderType, IGenderDomain>
    { }
}