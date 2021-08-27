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
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Async.ResultExtension.ResultErrors;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы одежды
    /// </summary>
    public class ClothesDatabaseValidateService : DatabaseValidateService<int, IClothesMainDomain, ClothesEntity>,
                                                  IClothesDatabaseValidateService
    {
        public ClothesDatabaseValidateService(IClothesTable clothesTable,
                                              IGenderDatabaseValidateService genderDatabaseValidateService,
                                              IClothesTypeDatabaseValidateService clothesTypeDatabaseValidateService,
                                              IColorClothesDatabaseValidateService colorClothesDatabaseValidateService,
                                              ISizeGroupDatabaseValidateService sizeGroupDatabaseValidateService,
                                              IClothesImageDatabaseValidateService clothesImageDatabaseValidateService)
            : base(clothesTable)
        {
            _genderDatabaseValidateService = genderDatabaseValidateService;
            _clothesTypeDatabaseValidateService = clothesTypeDatabaseValidateService;
            _colorClothesDatabaseValidateService = colorClothesDatabaseValidateService;
            _sizeGroupDatabaseValidateService = sizeGroupDatabaseValidateService;
            _clothesImageDatabaseValidateService = clothesImageDatabaseValidateService;
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
        /// Сервис проверки данных из базы изображений одежды
        /// </summary>
        private readonly IClothesImageDatabaseValidateService _clothesImageDatabaseValidateService;

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(IClothesMainDomain clothesMain) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(clothesMain)).
            ResultErrorBindOk(() => ValidateDescription(clothesMain)).
            ResultErrorBindOk(() => ValidatePrice(clothesMain)).
            ResultErrorBindOk(() => ValidateClothesTypeName(clothesMain)).
            ResultErrorBindOk(() => ValidateImages(clothesMain)).
            ResultErrorBindOk(() => ValidateColors(clothesMain)).
            ResultErrorBindOk(() => ValidateSizeGroups(clothesMain));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IClothesMainDomain clothesMain) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => _genderDatabaseValidateService.ValidateFind(clothesMain.Gender.Id)).
            ResultErrorBindOkBindAsync(() => _clothesTypeDatabaseValidateService.ValidateFind(clothesMain.ClothesType.Id)).
            ResultErrorBindOkBindAsync(() => clothesMain.Colors.Select(color => color.Id).Distinct().
                                             MapAsync(ids => _colorClothesDatabaseValidateService.ValidateFinds(ids))).
            ResultErrorBindOkBindAsync(() => clothesMain.SizeGroups.Select(sizeGroup => sizeGroup.Id).Distinct().
                                             MapAsync(ids => _sizeGroupDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<IClothesMainDomain> clothesDomains) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => clothesDomains.Select(clothes => clothes.Gender.Id).
                                                        Distinct().
                                         Map(ids => _genderDatabaseValidateService.ValidateFinds(ids))).
            ResultErrorBindOkBindAsync(() => clothesDomains.Select(clothes => clothes.ClothesType.Id).
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
        private static IResultError ValidateName(IClothesMainDomain clothesMain) =>
            clothesMain.Name.ToResultValueWhere(
                name => !String.IsNullOrWhiteSpace(name),
                _ => DatabaseFieldErrors.FieldNotValid(clothesMain.Name, nameof(IClothesTable)));

        /// <summary>
        /// Проверка описания
        /// </summary>
        private static IResultError ValidateDescription(IClothesMainDomain clothesMain) =>
            clothesMain.Description.ToResultValueWhere(
                description => !String.IsNullOrWhiteSpace(description),
                _ => DatabaseFieldErrors.FieldNotValid(clothesMain.Description, nameof(IClothesTable)));

        /// <summary>
        /// Проверка цены
        /// </summary>
        private static IResultError ValidatePrice(IClothesMainDomain clothesMain) =>
             clothesMain.Price.ToResultValueWhere(
                price => price > 0,
                _ => DatabaseFieldErrors.FieldNotValid(0, clothesMain.Price, nameof(IClothesTable)));

        /// <summary>
        /// Проверка имени типа одежды
        /// </summary>
        private static IResultError ValidateClothesTypeName(IClothesMainDomain clothesMain) =>
            clothesMain.ClothesTypeName.ToResultValueWhere(
                clothesTypeName => !String.IsNullOrWhiteSpace(clothesTypeName),
                _ => DatabaseFieldErrors.FieldNotValid(clothesMain.ClothesTypeName, nameof(IClothesTable)));

        /// <summary>
        /// Проверка изображений
        /// </summary>
        private IResultError ValidateImages(IClothesMainDomain clothesMain) =>
            _clothesImageDatabaseValidateService.ValidateQuantity(clothesMain.Images).
             ResultErrorBindOk(() => _clothesImageDatabaseValidateService.ValidateByMain(clothesMain.Images)).
             ResultErrorBindOk(() => _clothesImageDatabaseValidateService.ValidateModels(clothesMain.Images));

        /// <summary>
        /// Проверка цветов
        /// </summary>
        private IResultError ValidateColors(IClothesMainDomain clothesMain) =>
            _colorClothesDatabaseValidateService.ValidateQuantity(clothesMain.Colors);

        /// <summary>
        /// Проверка групп размеров
        /// </summary>
        private IResultError ValidateSizeGroups(IClothesMainDomain clothesMain) =>
            _sizeGroupDatabaseValidateService.ValidateQuantity(clothesMain.SizeGroups);
    }
}