using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис типа пола
    /// </summary>
    public interface IGenderRestService: IRestServiceBase<GenderType, IGenderDomain>
    { }
}