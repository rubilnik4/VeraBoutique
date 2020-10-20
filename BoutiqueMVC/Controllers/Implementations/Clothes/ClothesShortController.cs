using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи одежды
    /// </summary>
    public class ClothesShortController : ApiController<int, ClothesShortTransfer, IClothesShortDomain>
    {
        public ClothesShortController(IClothesShortDatabaseService clothesShortDatabaseService,
                                      IClothesShortTransferConverter clothesShortTransferConverter)
            : base(clothesShortDatabaseService, clothesShortTransferConverter)
        {
            _clothesShortDatabaseService = clothesShortDatabaseService;
            _clothesShortTransferConverter = clothesShortTransferConverter;
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private readonly IClothesShortDatabaseService _clothesShortDatabaseService;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesShortTransferConverter _clothesShortTransferConverter;

        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        [HttpGet("withoutImages")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesShortTransfer>>> GetWithoutImages() =>
            await _clothesShortDatabaseService.GetWithoutImages().
            ResultCollectionOkTaskAsync(clothes => _clothesShortTransferConverter.ToTransfers(clothes)).
            ToActionResultCollectionTaskAsync<int, ClothesShortTransfer>();
    }
}