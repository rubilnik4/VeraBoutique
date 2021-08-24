using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities
{
    using ClothesFunc = Func<IEnumerable<IColorDomain>, IEnumerable<ISizeGroupMainDomain>,
                             IClothesDetailDomain>;

    public class ClothesDetailEntityConverter : EntityConverter<int, IClothesDetailDomain, ClothesEntity>, 
                                                IClothesDetailEntityConverter
    {
        public ClothesDetailEntityConverter(IColorClothesEntityConverter colorClothesEntityConverter,
                                            ISizeGroupMainEntityConverter sizeGroupMainEntityConverter)
        {
            _colorClothesEntityConverter = colorClothesEntityConverter;
            _sizeGroupMainEntityConverter = sizeGroupMainEntityConverter;
        }

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        private readonly IColorClothesEntityConverter _colorClothesEntityConverter;

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ISizeGroupMainEntityConverter _sizeGroupMainEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesDetailDomain> FromEntity(ClothesEntity clothesEntity) =>
            GetClothesFunc(clothesEntity).
            ResultValueCurryOk(ClothesMainEntityConverter.ColorClothesFromComposite(clothesEntity.ClothesColorComposites, 
                                                                                    _colorClothesEntityConverter)).
            ResultValueCurryOk(ClothesMainEntityConverter.SizeGroupFromComposite(clothesEntity.ClothesSizeGroupComposites,
                                                                                 _sizeGroupMainEntityConverter)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesEntity ToEntity(IClothesDetailDomain clothesDetailDomain) =>
            new ClothesEntity(clothesDetailDomain, null!,
                              ClothesMainEntityConverter.ColorClothesToComposite(clothesDetailDomain.Colors, clothesDetailDomain.Id,
                                                                                 _colorClothesEntityConverter),
                              ClothesMainEntityConverter.SizeGroupToComposite(clothesDetailDomain.SizeGroups, clothesDetailDomain.Id,
                                                                              _sizeGroupMainEntityConverter));

        /// <summary>
        /// Функция получения информации об одежде
        /// </summary>
        private static IResultValue<ClothesFunc> GetClothesFunc(IClothesBase clothes) =>
            new ResultValue<ClothesFunc>((colors, sizes) => new ClothesDetailDomain(clothes, colors, sizes));
    }
}