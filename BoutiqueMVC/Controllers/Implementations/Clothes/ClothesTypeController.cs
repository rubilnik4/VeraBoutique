using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesType;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
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
    public class ClothesTypeController : ApiController<string, ClothesTypeShortTransfer, IClothesTypeShortDomain>
    {
        public ClothesTypeController(IClothesTypeDatabaseService clothesTypeDatabaseService,
                                     IClothesTypeShortTransferConverter clothesTypeShortTransferConverter,
                                     IClothesTypeTransferConverter clothesTypeTransferConverter)
            : base(clothesTypeDatabaseService, clothesTypeShortTransferConverter)
        {
            _clothesTypeDatabaseService = clothesTypeDatabaseService;
            _clothesTypeTransferConverter = clothesTypeTransferConverter;
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private readonly IClothesTypeDatabaseService _clothesTypeDatabaseService;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTypeTransferConverter _clothesTypeTransferConverter;

        /// <summary>
        /// Получить вид одежды по типу пола
        /// </summary>
        [HttpGet("genderCategory/{genderType}/{category}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesTypeTransfer>>> GetByGenderCategory(GenderType genderType,
                                                                                                      string category) =>
            await _clothesTypeDatabaseService.GetByGenderCategory(genderType, category).
            ResultCollectionOkTaskAsync(clothesTypes => _clothesTypeTransferConverter.ToTransfers(clothesTypes)).
            ToActionResultCollectionTaskAsync<string, ClothesTypeTransfer>();
    }
}
