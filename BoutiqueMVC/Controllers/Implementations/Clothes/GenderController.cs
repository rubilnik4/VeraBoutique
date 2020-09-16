using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола
    /// </summary>
    public class GenderController : ApiController<GenderType, IGenderTransfer, IGenderDomain>
    {
        public GenderController(IGenderDatabaseService genderDatabaseService, IGenderTransferConverter genderTransferConverter)
            : base(genderDatabaseService, genderTransferConverter)
        { }
    }
}
