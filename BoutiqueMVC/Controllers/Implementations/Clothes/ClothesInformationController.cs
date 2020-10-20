using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи информации об одежде
    /// </summary>
    public class ClothesInformationController : ApiController<int, ClothesInformationTransfer, IClothesInformationDomain>
    {
        public ClothesInformationController(IClothesInformationDatabaseService clothesInformationDatabaseService,
                                            IClothesInformationTransferConverter clothesInformationTransferConverter)
           : base(clothesInformationDatabaseService, clothesInformationTransferConverter)
        { }
    }
}