using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueMVC.Controllers.Base;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи группы размеров одежды
    /// </summary>
    public class SizeGroupController : ApiController<int, ISizeGroupMainDomain, SizeGroupMainTransfer>
    {
        public SizeGroupController(ISizeGroupDatabaseService sizeGroupDatabaseService,
                                   ISizeGroupMainTransferConverter sizeGroupTransferConverter)
            : base(sizeGroupDatabaseService, sizeGroupTransferConverter)
        { }
    }
}