using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate
{
    /// <summary>
    /// Сервис проверки данных из базы размера одежды
    /// </summary>
    public interface ISizeDatabaseValidateService : IDatabaseValidateService<int, ISizeDomain>
    { }
}