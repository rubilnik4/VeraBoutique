using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    public interface IClothesRestService : IRestServiceBase<int, IClothesFullDomain>
    { }
}