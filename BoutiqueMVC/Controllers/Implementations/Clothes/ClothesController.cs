using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ImageTransfers;
using BoutiqueDTO.Routes.Clothes;
using BoutiqueMVC.Controllers.Implementations.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace BoutiqueMVC.Controllers.Implementations.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи информации об одежде
    /// </summary>
    public class ClothesController : ApiController<int, IClothesMainDomain, ClothesMainTransfer>
    {
        public ClothesController(IClothesDatabaseService clothesDatabaseService,
                                 IClothesMainTransferConverter clothesMainTransferConverter,
                                 IClothesDetailTransferConverter clothesDetailTransferConverter,
                                 IClothesTransferConverter clothesTransferConverter, 
                                 IClothesImageTransferConverter clothesImageTransferConverter)
           : base(clothesDatabaseService, clothesMainTransferConverter)
        {
            _clothesDatabaseService = clothesDatabaseService;
            _clothesDetailTransferConverter = clothesDetailTransferConverter;
            _clothesTransferConverter = clothesTransferConverter;
            _clothesImageTransferConverter = clothesImageTransferConverter;
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private readonly IClothesDatabaseService _clothesDatabaseService;

        /// <summary>
        /// Конвертер уточненной информации об одежде в трансферную модель
        /// </summary>
        private readonly IClothesDetailTransferConverter _clothesDetailTransferConverter;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTransferConverter _clothesTransferConverter;

        /// <summary>
        /// Конвертер изображений в трансферную модель
        /// </summary>
        private readonly IClothesImageTransferConverter _clothesImageTransferConverter;

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

        /// <summary>
        /// Получить уточненную информацию об одежде по типу пола и категории
        /// </summary>
        [HttpGet(ClothesRoutes.DETAIL_ROUTE + "/{genderType}/{clothesType}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesDetailTransfer>>> GetClothesDetails(GenderType genderType, string clothesType) =>
            await _clothesDatabaseService.GetClothesDetails(genderType, clothesType).
            ResultCollectionOkTaskAsync(clothes => _clothesDetailTransferConverter.ToTransfers(clothes)).
            ToActionResultCollectionTaskAsync<int, ClothesDetailTransfer>();

        /// <summary>
        /// Получить главное изображение одежды по идентификатору
        /// </summary>
        [HttpGet(ClothesRoutes.IMAGE_ROUTE + "/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<byte[]>> GetImage(int id) =>
            await _clothesDatabaseService.GetImage(id).
            ToImageResultValueTaskAsync();

        /// <summary>
        /// Получить изображения одежды по идентификатору
        /// </summary>
        [HttpGet(ClothesRoutes.IMAGES_ROUTE + "/{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesImageTransfer>>> GetImages(int id) =>
            await _clothesDatabaseService.GetImages(id).
            ResultCollectionOkTaskAsync(images => _clothesImageTransferConverter.ToTransfers(images)).
            ToActionResultCollectionTaskAsync<int, ClothesImageTransfer>();
    }
}