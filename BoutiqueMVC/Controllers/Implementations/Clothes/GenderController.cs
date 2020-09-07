using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ApiController<GenderType, IGenderTransfer, IGenderDomain>
    {
        public GenderController(IGenderService genderService, IGenderTransferConverter genderTransferConverter)
            :base(genderService, genderTransferConverter)
        { }
    }
}
