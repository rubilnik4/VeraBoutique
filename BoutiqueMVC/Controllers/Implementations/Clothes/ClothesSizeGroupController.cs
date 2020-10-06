using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи группы размеров одежды
    /// </summary>
    public class ClothesSizeGroupController : ApiController<string, ClothesSizeGroupTransfer, IClothesSizeGroupDomain>
    {
        public ClothesSizeGroupController(IClothesSizeGroupDatabaseService clothesSizeGroupDatabaseService,
                                          IClothesSizeGroupTransferConverter clothesSizeGroupTransferConverter)
         : base(clothesSizeGroupDatabaseService, clothesSizeGroupTransferConverter)
        { }
    }
}