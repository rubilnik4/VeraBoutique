using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола
    /// </summary>
    public class GenderController : ApiController<GenderType, IGenderDomain, GenderTransfer>
    {
        public GenderController(IGenderDatabaseService genderDatabaseService, IGenderTransferConverter genderTransferConverter)
            : base(genderDatabaseService, genderTransferConverter)
        { }
    }
}
