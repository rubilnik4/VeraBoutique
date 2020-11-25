using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи цвета одежды
    /// </summary>
    public class ColorClothesController : ApiController<string, ColorClothesTransfer, ColorClothesTransfer, IColorClothesDomain, IColorClothesDomain>
    {
        public ColorClothesController(IColorClothesDatabaseService colorClothesDatabaseService,
                                      IColorClothesTransferConverter colorClothesTransferConverter)
            : base(colorClothesDatabaseService, colorClothesTransferConverter)
        { }
    }
}