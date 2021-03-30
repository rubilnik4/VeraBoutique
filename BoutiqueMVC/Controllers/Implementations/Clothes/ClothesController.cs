using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultValue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи информации об одежде
    /// </summary>
    public class ClothesController : ApiController<int, IClothesMainDomain, ClothesMainTransfer>
    {
        public ClothesController(IClothesDatabaseService clothesDatabaseService,
                                 IClothesMainTransferConverter clothesMainTransferConverter,
                                 IClothesTransferConverter clothesTransferConverter)
           : base(clothesDatabaseService, clothesMainTransferConverter)
        {
            _clothesDatabaseService = clothesDatabaseService;
            _clothesTransferConverter = clothesTransferConverter;
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private readonly IClothesDatabaseService _clothesDatabaseService;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTransferConverter _clothesTransferConverter;

        /// <summary>
        /// Получить одежду по типу пола и категории
        /// </summary>
        [HttpGet("{genderType}/{clothesType}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesTransfer>>> GetClothes(GenderType genderType, string clothesType) =>
            await _clothesDatabaseService.GetClothes(genderType, clothesType).
            ResultCollectionOkTaskAsync(clothes => _clothesTransferConverter.ToTransfers(clothes)).
            ToActionResultCollectionTaskAsync<int, ClothesTransfer>();
    }
}