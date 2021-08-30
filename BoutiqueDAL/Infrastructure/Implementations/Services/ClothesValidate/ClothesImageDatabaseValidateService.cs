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
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.ClothesValidate
{
    public class ClothesImageDatabaseValidateService : DatabaseValidateService<int, IClothesImageDomain, ClothesImageEntity>,
                                                       IClothesImageDatabaseValidateService
    {
        public ClothesImageDatabaseValidateService(IClothesImageTable clothesImageTable)
            : base(clothesImageTable)
        { }

        /// <summary>
        /// Проверить модель
        /// </summary>
        public override IResultError ValidateModel(IClothesImageDomain clothesImage) =>
            new ResultError().
            ResultErrorBindOk(() => ValidateImage(clothesImage));

        /// <summary>
        /// Проверка на наличие главного изображения
        /// </summary>
        public IResultError ValidateByMain(IEnumerable<IClothesImageDomain> clothesImages) =>
             clothesImages.Where(clothesImage => clothesImage.IsMain).ToList().
             ToResultValueWhere(images => images.Count == 1,
                badFunc: images => DatabaseFieldErrors.FieldNotValid(images.Count, nameof(IClothesImageTable),
                                                                     "Превышено количество главных изображений"));

        /// <summary>
        /// Проверка изображения
        /// </summary>
        private static IResultError ValidateImage(IClothesImageDomain clothesImage) =>
            clothesImage.Image.ToResultValueWhere(
                image => image?.Length > 0,
                image => DatabaseFieldErrors.FieldNotValid(image, nameof(IClothesImageTable)));
    }
}