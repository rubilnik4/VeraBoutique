using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис категорий одежды
    /// </summary>
    public interface ICategoryRestService : IRestServiceBase<string, ICategoryDomain>
    { }
}