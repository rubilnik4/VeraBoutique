using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Base;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи размеров одежды
    /// </summary>
    public class SizeController : ApiController<int, ISizeDomain, SizeTransfer>
    {
        public SizeController(ISizeDatabaseService sizeDatabaseService,
                              ISizeTransferConverter sizeTransferConverter)
         : base(sizeDatabaseService, sizeTransferConverter)
        { }
    }
}