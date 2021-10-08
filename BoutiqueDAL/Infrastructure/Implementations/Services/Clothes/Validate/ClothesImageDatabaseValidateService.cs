using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultErrors;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes.Validate
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