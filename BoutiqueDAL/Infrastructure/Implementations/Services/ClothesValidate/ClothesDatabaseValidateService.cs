using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base.DatabaseTable;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
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
    public class ClothesDatabaseValidateService : DatabaseValidateService<int, IClothesFullDomain, ClothesFullEntity>,
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
        protected override IResultError ValidateModel(IClothesFullDomain clothesFull) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(clothesFull)).
            ResultErrorBindOk(() => ValidateDescription(clothesFull)).
            ResultErrorBindOk(() => ValidatePrice(clothesFull)).
            ResultErrorBindOk(() => ValidateClothesTypeName(clothesFull)).
            ResultErrorBindOk(() => ValidateColors(clothesFull)).
            ResultErrorBindOk(() => ValidateSizeGroups(clothesFull));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IClothesFullDomain clothesFull) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => _genderDatabaseValidateService.ValidateFind(clothesFull.Gender.Id)).
            ResultErrorBindOkBindAsync(() => _clothesTypeDatabaseValidateService.ValidateFind(clothesFull.ClothesTypeShort.Id)).
            ResultErrorBindOkBindAsync(() => _colorClothesDatabaseValidateService.ValidateFinds(clothesFull.Colors.Select(color => color.Id))).
            ResultErrorBindOkBindAsync(() => _sizeGroupDatabaseValidateService.ValidateFinds(clothesFull.SizeGroups.Select(sizeGroup => sizeGroup.Id)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<IClothesFullDomain> clothesDomains) =>
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
        private static IResultError ValidateName(IClothesFullDomain clothesFull) =>
            clothesFull.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => ModelsErrors.FieldNotValid<int, IClothesFullDomain>(nameof(clothesFull.Name), clothesFull));

        /// <summary>
        /// Проверка описания
        /// </summary>
        private static IResultError ValidateDescription(IClothesFullDomain clothesFull) =>
            clothesFull.Description.ToResultValueWhere(
                description => !String.IsNullOrWhiteSpace(description),
                _ => ModelsErrors.FieldNotValid<int, IClothesFullDomain>(nameof(clothesFull.Description), clothesFull));

        /// <summary>
        /// Проверка цены
        /// </summary>
        private static IResultError ValidatePrice(IClothesFullDomain clothesFull) =>
             clothesFull.Price.ToResultValueWhere(
                price => price > 0,
                _ => ModelsErrors.FieldNotValid<int, IClothesFullDomain>(0, nameof(clothesFull.Price), clothesFull));

        /// <summary>
        /// Проверка имени типа одежды
        /// </summary>
        private static IResultError ValidateClothesTypeName(IClothesFullDomain clothesFull) =>
            clothesFull.ClothesTypeName.ToResultValueWhere(
                clothesTypeName => !String.IsNullOrWhiteSpace(clothesTypeName),
                _ => ModelsErrors.FieldNotValid<int, IClothesFullDomain>(nameof(clothesFull.ClothesTypeName), clothesFull));

        /// <summary>
        /// Проверка цветов
        /// </summary>
        private IResultError ValidateColors(IClothesFullDomain clothesFull) =>
            _colorClothesDatabaseValidateService.ValidateQuantity(clothesFull.Colors);

        /// <summary>
        /// Проверка групп размеров
        /// </summary>
        private IResultError ValidateSizeGroups(IClothesFullDomain clothesFull) =>
            _sizeGroupDatabaseValidateService.ValidateQuantity(clothesFull.SizeGroups);
    }
}