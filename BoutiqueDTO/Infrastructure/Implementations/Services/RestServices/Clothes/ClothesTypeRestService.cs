using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис типа одежды
    /// </summary>
    public class ClothesTypeRestService : RestServiceBase<string, IClothesTypeDomain, ClothesTypeTransfer>, 
                                          IClothesTypeRestService
    {
        public ClothesTypeRestService(IClothesTypeApiService clothesTypeApiService, 
                                      IClothesTypeTransferConverter clothesTypeTransferConverter,
                                      IBoutiqueLogger boutiqueLogger)
            : base(clothesTypeApiService, clothesTypeTransferConverter,  boutiqueLogger)
        { }
    }
}