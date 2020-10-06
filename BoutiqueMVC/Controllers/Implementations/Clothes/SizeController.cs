using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи размеров одежды
    /// </summary>
    public class SizeController : ApiController<(SizeType, int), SizeTransfer, ISizeDomain>
    {
        public SizeController(ISizeDatabaseService sizeDatabaseService,
                              ISizeTransferConverter sizeTransferConverter)
         : base(sizeDatabaseService, sizeTransferConverter)
        { }
    }
}