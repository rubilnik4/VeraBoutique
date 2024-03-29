﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Routes.Clothes;
using BoutiqueMVC.Controllers.Base;
using BoutiqueMVC.Extensions.Controllers.Async;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultCollections;

namespace BoutiqueMVC.Controllers.Clothes
{
    /// <summary>
    /// Контроллер для получения и записи пола
    /// </summary>
    public class GenderController : ApiController<GenderType, IGenderDomain, GenderTransfer>
    {
        public GenderController(IGenderDatabaseService genderDatabaseService, 
                                IGenderTransferConverter genderTransferConverter,
                                IGenderCategoryTransferConverter genderCategoryTransferConverter)
            : base(genderDatabaseService, genderTransferConverter)
        {
            _genderDatabaseService = genderDatabaseService;
            _genderCategoryTransferConverter = genderCategoryTransferConverter;
        }

        /// <summary>
        /// Сервис типа пола одежды в базе данных
        /// </summary>
        private readonly IGenderDatabaseService _genderDatabaseService;

        /// <summary>
        /// Конвертер типа пола c категорией в трансферную модель
        /// </summary>
        private readonly IGenderCategoryTransferConverter _genderCategoryTransferConverter;

        /// <summary>
        /// Получить типы пола одежды с категорией
        /// </summary>
        [HttpGet(GenderRoutes.GENDER_CATEGORY_ROUTE)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<GenderCategoryTransfer>>> GetGenderCategories() =>
            await _genderDatabaseService.GetGenderCategories().
            ResultCollectionOkTaskAsync(_genderCategoryTransferConverter.ToTransfers).
            ToActionResultCollectionTaskAsync<GenderType, GenderCategoryTransfer>();
    }
}
