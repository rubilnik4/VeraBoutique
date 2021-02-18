using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи вида одежды
    /// </summary>
    public class ClothesTypeController : ApiController<string, IClothesTypeMainDomain, ClothesTypeMainTransfer>
    {
        public ClothesTypeController(IClothesTypeDatabaseService clothesTypeDatabaseService,
                                     IClothesTypeMainTransferConverter clothesTypeMainTransferConverter)
            : base(clothesTypeDatabaseService, clothesTypeMainTransferConverter)
        { }
    }
}
