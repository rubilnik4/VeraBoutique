using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Base;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи цвета одежды
    /// </summary>
    public class ColorController : ApiController<string, IColorDomain, ColorTransfer>
    {
        public ColorController(IColorDatabaseService colorDatabaseService,
                               IColorTransferConverter colorTransferConverter)
            : base(colorDatabaseService, colorTransferConverter)
        { }
    }
}