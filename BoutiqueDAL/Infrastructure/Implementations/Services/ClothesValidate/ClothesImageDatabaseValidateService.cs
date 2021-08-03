using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
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
    public class ClothesImageDatabaseValidateService : DatabaseValidateService<int, IClothesImageDomain, ClothesImageEntity>,
                                                       IClothesImageDatabaseValidateService
    {
        public ClothesImageDatabaseValidateService(IClothesImageTable clothesImageTable,
                                                   IClothesDatabaseValidateService clothesDatabaseValidateService)
            : base(clothesImageTable)
        {
            _clothesDatabaseValidateService = clothesDatabaseValidateService;
        }

        /// <summary>
        /// Сервис проверки данных из базы одежды
        /// </summary>
        private readonly IClothesDatabaseValidateService _clothesDatabaseValidateService;

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(IClothesImageDomain clothesImage) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateImage(clothesImage));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IClothesImageDomain clothesImage) =>
             await new ResultError().
            ResultErrorBindOkAsync(() => _clothesDatabaseValidateService.ValidateFind(clothesImage.ClothesId));

        /// <summary>
        /// Проверить наличие вложенных моделей
        /// </summary>
        protected override async Task<IResultError> ValidateIncludes(IEnumerable<IClothesImageDomain> clothesImages) =>
            await new ResultError().
            ResultErrorBindOkAsync(() => clothesImages.Select(clothesImage => clothesImage.ClothesId).
                                                       Distinct().
                                         Map(ids => _clothesDatabaseValidateService.ValidateFinds(ids)));

        /// <summary>
        /// Проверка на наличие главного изображения
        /// </summary>
        public IResultError ValidateByMain(IEnumerable<IClothesImageDomain> clothesImages) =>
             clothesImages.Where(clothesImage => clothesImage.IsMain).ToList().
             ToResultValueWhere(images => images.Count == 1,
                badFunc: images => ModelsErrors.FieldNotValid<int, IClothesImageDomain>(nameof(IClothesImageDomain.IsMain)));

        /// <summary>
        /// Проверка изображения
        /// </summary>
        private static IResultError ValidateImage(IClothesImageDomain clothesImage) =>
            clothesImage.Image.ToResultValueWhere(
                image => image?.Length > 0,
                _ => ModelsErrors.FieldNotValid<int, IClothesImageDomain>(nameof(clothesImage.Image), clothesImage));
    }
}