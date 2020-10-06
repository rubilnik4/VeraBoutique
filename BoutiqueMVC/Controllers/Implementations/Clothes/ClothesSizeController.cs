using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи размеров одежды
    /// </summary>
    public class ClothesSizeController : ApiController<string, ClothesSizeTransfer, IClothesSizeDomain>
    {
        public ClothesSizeController(IClothesSizeDatabaseService clothesSizeDatabaseService,
                                     IClothesSizeTransferConverter clothesSizeTransferConverter)
         : base(clothesSizeDatabaseService, clothesSizeTransferConverter)
        { }
    }
}