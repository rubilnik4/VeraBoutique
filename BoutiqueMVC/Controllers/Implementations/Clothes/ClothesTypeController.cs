using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
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
    public class ClothesTypeController : ApiController<string, ClothesTypeTransfer, IClothesTypeDomain>
    {
        public ClothesTypeController(IClothesTypeDatabaseService clothesTypeDatabaseService,
                                     IClothesTypeTransferConverter clothesTypeTransferConverter)
            : base(clothesTypeDatabaseService, clothesTypeTransferConverter)
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
        [HttpGet("gender/{genderType}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesTypeTransfer>>> GetByGender(GenderType genderType) =>
            await _clothesTypeDatabaseService.GetByGender(genderType).
            ResultCollectionOkTaskAsync(clothesTypes => _clothesTypeTransferConverter.ToTransfers(clothesTypes)).
            ToActionResultCollectionTaskAsync<string, ClothesTypeTransfer>();
    }
}
