using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes.ClothesTransfers;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель
    /// </summary>
    public class CategoryClothesTypeTransferConverter : TransferConverter<string, ICategoryClothesTypeDomain, CategoryClothesTypeTransfer>,
                                                        ICategoryClothesTypeTransferConverter
    {
        public CategoryClothesTypeTransferConverter(IClothesTypeTransferConverter clothesTypeTransferConverter)
        {
            _clothesTypeTransferConverter = clothesTypeTransferConverter;
        }

        /// <summary>
        /// Конвертер основной информации вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTypeTransferConverter _clothesTypeTransferConverter;

        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override CategoryClothesTypeTransfer ToTransfer(ICategoryClothesTypeDomain categoryClothesTypeDomain) =>
            new CategoryClothesTypeTransfer(categoryClothesTypeDomain,
                                            _clothesTypeTransferConverter.ToTransfers(categoryClothesTypeDomain.ClothesTypes));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<ICategoryClothesTypeDomain> FromTransfer(CategoryClothesTypeTransfer categoryClothesTypeTransfer) =>
            GetCategoryFunc(categoryClothesTypeTransfer).
            ResultValueCurryOk(_clothesTypeTransferConverter.GetDomains(categoryClothesTypeTransfer.ClothesTypes)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения категории одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<IClothesTypeDomain>, ICategoryClothesTypeDomain>> GetCategoryFunc(ICategoryBase category) =>
            new ResultValue<Func<IEnumerable<IClothesTypeDomain>, ICategoryClothesTypeDomain>>(
                clothesTypes => new CategoryClothesTypeDomain(category, clothesTypes));
    }
}