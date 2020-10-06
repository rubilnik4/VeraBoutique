using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи группы размеров одежды
    /// </summary>
    public class SizeGroupController : ApiController<(ClothesSizeType, int), SizeGroupTransfer, ISizeGroupDomain>
    {
        public SizeGroupController(ISizeGroupDatabaseService sizeGroupDatabaseService,
                                   ISizeGroupTransferConverter sizeGroupTransferConverter)
         : base(sizeGroupDatabaseService, sizeGroupTransferConverter)
        { }
    }
}