using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfer;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfer;
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
    public class ClothesTypeController : ApiController<string, ClothesTypeFullTransfer, IClothesTypeFullDomain>
    {
        public ClothesTypeController(IClothesTypeDatabaseService clothesTypeDatabaseService,
                                     IClothesTypeShortTransferConverter clothesTypeShortTransferConverter,
                                     IClothesTypeFullTransferConverter clothesTypeFullTransferConverter)
            : base(clothesTypeDatabaseService, clothesTypeFullTransferConverter)
        {
            _clothesTypeDatabaseService = clothesTypeDatabaseService;
            _clothesTypeShortTransferConverter = clothesTypeShortTransferConverter;
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private readonly IClothesTypeDatabaseService _clothesTypeDatabaseService;

        /// <summary>
        /// Конвертер основной информации вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTypeShortTransferConverter _clothesTypeShortTransferConverter;

        /// <summary>
        /// Получить вид одежды по типу пола
        /// </summary>
        [HttpGet("genderCategory/{genderType}/{category}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesTypeShortTransfer>>> GetByGenderCategory(GenderType genderType,
                                                                                                           string category) =>
            await _clothesTypeDatabaseService.GetByGenderCategory(genderType, category).
            ResultCollectionOkTaskAsync(clothesTypes => _clothesTypeShortTransferConverter.ToTransfers(clothesTypes)).
            ToActionResultCollectionTaskAsync<string, ClothesTypeShortTransfer>();
    }
}
