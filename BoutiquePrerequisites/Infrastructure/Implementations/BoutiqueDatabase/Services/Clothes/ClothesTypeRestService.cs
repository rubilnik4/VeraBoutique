using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Base;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.Logger;

namespace BoutiquePrerequisites.Infrastructure.Implementations.BoutiqueDatabase.Services.Clothes
{
    /// <summary>
    /// Сервис загрузки типа одежды в базу данных
    /// </summary>
    public class ClothesTypeRestService : RestServiceBase<string, IClothesTypeDomain, ClothesTypeTransfer>, IClothesTypeRestService
    {
        public ClothesTypeRestService(IClothesTypeApiService clothesTypeApiService, 
                                      IClothesTypeTransferConverter clothesTypeTransferConverter,
                                      ILogger logger)
            : base(clothesTypeApiService, clothesTypeTransferConverter,  logger)
        { }
    }
}