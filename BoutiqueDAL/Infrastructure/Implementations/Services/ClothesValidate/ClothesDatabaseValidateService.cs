﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultError;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы одежды
    /// </summary>
    public class ClothesDatabaseValidateService : DatabaseValidateService<int, IClothesDomain, ClothesEntity>,
                                                  IClothesDatabaseValidateService
    {
        public ClothesDatabaseValidateService(IClothesTable clothesTable,
                                              IGenderDatabaseValidateService genderDatabaseValidateService,
                                              IClothesTypeDatabaseValidateService clothesTypeDatabaseValidateService,
                                              IColorClothesDatabaseValidateService colorClothesDatabaseValidateService,
                                              ISizeGroupDatabaseValidateService sizeGroupDatabaseValidateService)
            : base(clothesTable)
        {
            _genderDatabaseValidateService = genderDatabaseValidateService;
            _clothesTypeDatabaseValidateService = clothesTypeDatabaseValidateService;
            _colorClothesDatabaseValidateService = colorClothesDatabaseValidateService;
            _sizeGroupDatabaseValidateService = sizeGroupDatabaseValidateService;
        }

        /// <summary>
        /// Сервис проверки данных из базы пола одежды
        /// </summary>
        private readonly IGenderDatabaseValidateService _genderDatabaseValidateService;

        /// <summary>
        /// Сервис проверки данных из базы типов одежды
        /// </summary>
        private readonly IClothesTypeDatabaseValidateService _clothesTypeDatabaseValidateService;

        /// <summary>
        /// Сервис проверки данных из базы цвета одежды
        /// </summary>
        private readonly IColorClothesDatabaseValidateService _colorClothesDatabaseValidateService;

        /// <summary>
        /// Сервис проверки данных из базы группы размера одежды
        /// </summary>
        private readonly ISizeGroupDatabaseValidateService _sizeGroupDatabaseValidateService;

        /// <summary>
        /// Проверить модель
        /// </summary>
        protected override IResultError ValidateModel(IClothesDomain clothes) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(clothes)).
            ResultErrorBindOk(() => ValidateDescription(clothes)).
            ResultErrorBindOk(() => ValidatePrice(clothes)).
            ResultErrorBindOk(() => ValidateClothesTypeName(clothes)).
            ResultErrorBindOk(() => ValidateColors(clothes)).
            ResultErrorBindOk(() => ValidateSizeGroups(clothes));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IClothesDomain clothes) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => _genderDatabaseValidateService.ValidateFind(clothes.Gender.Id)).
            ResultErrorBindOkBindAsync(() => _clothesTypeDatabaseValidateService.ValidateFind(clothes.ClothesTypeShort.Id)).
            ResultErrorBindOkBindAsync(() => _colorClothesDatabaseValidateService.ValidateFinds(clothes.Colors.Select(color => color.Id))).
            ResultErrorBindOkBindAsync(() => _sizeGroupDatabaseValidateService.ValidateFinds(clothes.SizeGroups.Select(sizeGroup => sizeGroup.Id)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<IClothesDomain> clothesDomains) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => clothesDomains.Select(clothes => clothes.Gender.Id).
                                                        Distinct().
                                         Map(ids => _genderDatabaseValidateService.ValidateFinds(ids))).
            ResultErrorBindOkBindAsync(() => clothesDomains.Select(clothes => clothes.ClothesTypeShort.Id).
                                                            Distinct().
                                             Map(ids => _clothesTypeDatabaseValidateService.ValidateFinds(ids))).
            ResultErrorBindOkBindAsync(() => clothesDomains.SelectMany(clothesType => clothesType.Colors.Select(color => color.Id)).
                                                            Distinct().
                                             Map(ids => _colorClothesDatabaseValidateService.ValidateFinds(ids))).
            ResultErrorBindOkBindAsync(() => clothesDomains.SelectMany(clothesType => clothesType.SizeGroups.Select(sizeGroup => sizeGroup.Id)).
                                                            Distinct().
                                             Map(ids => _sizeGroupDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultError ValidateName(IClothesDomain clothes) =>
            clothes.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => ModelsErrors.FieldNotValid<int, IClothesDomain>(nameof(clothes.Name), clothes));

        /// <summary>
        /// Проверка описания
        /// </summary>
        private static IResultError ValidateDescription(IClothesDomain clothes) =>
            clothes.Description.ToResultValueWhere(
                description => !String.IsNullOrWhiteSpace(description),
                _ => ModelsErrors.FieldNotValid<int, IClothesDomain>(nameof(clothes.Description), clothes));

        /// <summary>
        /// Проверка цены
        /// </summary>
        private static IResultError ValidatePrice(IClothesDomain clothes) =>
             clothes.Price.ToResultValueWhere(
                price => price > 0,
                _ => ModelsErrors.FieldNotValid<int, IClothesDomain>(0, nameof(clothes.Price), clothes));

        /// <summary>
        /// Проверка имени типа одежды
        /// </summary>
        private static IResultError ValidateClothesTypeName(IClothesDomain clothes) =>
            clothes.ClothesTypeName.ToResultValueWhere(
                clothesTypeName => !String.IsNullOrWhiteSpace(clothesTypeName),
                _ => ModelsErrors.FieldNotValid<int, IClothesDomain>(nameof(clothes.ClothesTypeName), clothes));

        /// <summary>
        /// Проверка цветов
        /// </summary>
        private IResultError ValidateColors(IClothesDomain clothes) =>
            _colorClothesDatabaseValidateService.ValidateQuantity(clothes.Colors);

        /// <summary>
        /// Проверка групп размеров
        /// </summary>
        private IResultError ValidateSizeGroups(IClothesDomain clothes) =>
            _sizeGroupDatabaseValidateService.ValidateQuantity(clothes.SizeGroups);
    }
}