using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы размера одежды
    /// </summary>
    public interface ISizeDatabaseValidateService : IDatabaseValidateService<(SizeType, string), ISizeDomain>
    { }
}