using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Interfaces.RestClients;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис типа одежды
    /// </summary>
    public class ClothesTypeRestService : RestServiceBase<string, IClothesTypeMainDomain, ClothesTypeMainTransfer>, 
                                          IClothesTypeRestService
    {
        public ClothesTypeRestService(IRestHttpClient restHttpClient, IClothesTypeMainTransferConverter clothesTypeTransferConverter)
            : base(restHttpClient, clothesTypeTransferConverter)
        { }
    }
}