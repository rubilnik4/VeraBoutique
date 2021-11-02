using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueMVC.Controllers.Base;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи вида одежды
    /// </summary>
    public class ClothesTypeController : ApiController<string, IClothesTypeMainDomain, ClothesTypeMainTransfer>
    {
        public ClothesTypeController(IClothesTypeDatabaseService clothesTypeDatabaseService,
                                     IClothesTypeMainTransferConverter clothesTypeTransferConverter)
            : base(clothesTypeDatabaseService, clothesTypeTransferConverter)
        { }
    }
}
