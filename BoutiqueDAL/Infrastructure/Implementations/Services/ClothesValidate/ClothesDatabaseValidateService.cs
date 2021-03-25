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
    public class ClothesDatabaseValidateService : DatabaseValidateService<int, IClothesMainDomain, ClothesEntity>,
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
        protected override IResultError ValidateModel(IClothesMainDomain clothesMain) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateName(clothesMain)).
            ResultErrorBindOk(() => ValidateDescription(clothesMain)).
            ResultErrorBindOk(() => ValidatePrice(clothesMain)).
            ResultErrorBindOk(() => ValidateClothesTypeName(clothesMain)).
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
                _ => ModelsErrors.FieldNotValid<int, IClothesMainDomain>(nameof(clothesMain.Name), clothesMain));

        /// <summary>
        /// Проверка описания
        /// </summary>
        private static IResultError ValidateDescription(IClothesMainDomain clothesMain) =>
            clothesMain.Description.ToResultValueWhere(
                description => !String.IsNullOrWhiteSpace(description),
                _ => ModelsErrors.FieldNotValid<int, IClothesMainDomain>(nameof(clothesMain.Description), clothesMain));

        /// <summary>
        /// Проверка цены
        /// </summary>
        private static IResultError ValidatePrice(IClothesMainDomain clothesMain) =>
             clothesMain.Price.ToResultValueWhere(
                price => price > 0,
                _ => ModelsErrors.FieldNotValid<int, IClothesMainDomain>(0, nameof(clothesMain.Price), clothesMain));

        /// <summary>
        /// Проверка имени типа одежды
        /// </summary>
        private static IResultError ValidateClothesTypeName(IClothesMainDomain clothesMain) =>
            clothesMain.ClothesTypeName.ToResultValueWhere(
                clothesTypeName => !String.IsNullOrWhiteSpace(clothesTypeName),
                _ => ModelsErrors.FieldNotValid<int, IClothesMainDomain>(nameof(clothesMain.ClothesTypeName), clothesMain));

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