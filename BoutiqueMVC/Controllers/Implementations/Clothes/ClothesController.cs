﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
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
    public class ClothesController : ApiController<int, ClothesInformationTransfer, IClothesInformationDomain>
    {
        public ClothesController(IClothesDatabaseService clothesDatabaseService,
                                 IClothesShortTransferConverter clothesShortTransferConverter,
                                 IClothesInformationTransferConverter clothesInformationTransferConverter)
           : base(clothesDatabaseService, clothesInformationTransferConverter)
        {
            _clothesDatabaseService = clothesDatabaseService;
            _clothesShortTransferConverter = clothesShortTransferConverter;
            _clothesInformationTransferConverter = clothesInformationTransferConverter;
        }

        /// <summary>
        /// Сервис вида одежды в базе данных
        /// </summary>
        private readonly IClothesDatabaseService _clothesDatabaseService;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesShortTransferConverter _clothesShortTransferConverter;

        /// <summary>
        /// Конвертер информации об одежде в трансферную модель
        /// </summary>
        private readonly IClothesInformationTransferConverter _clothesInformationTransferConverter;

        /// <summary>
        /// Получить одежду без изображений
        /// </summary>
        [HttpGet("clothesShorts/{genderType}/{clothesType}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<ClothesShortTransfer>>> GetClothesShorts(GenderType genderType, string clothesType) =>
            await _clothesDatabaseService.GetClothesShorts(genderType, clothesType).
            ResultCollectionOkTaskAsync(clothes => _clothesShortTransferConverter.ToTransfers(clothes)).
            ToActionResultCollectionTaskAsync<int, ClothesShortTransfer>();

        /// <summary>
        /// Получить информацию об одежде по идентификатору
        /// </summary>
        [HttpGet("includes/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClothesInformationTransfer>> GetIncludesById(int id) =>
            await _clothesDatabaseService.GetIncludesById(id).
            ResultValueOkTaskAsync(clothesInformation => _clothesInformationTransferConverter.ToTransfer(clothesInformation)).
            ToActionResultValueTaskAsync<int, ClothesInformationTransfer>();
    }
}