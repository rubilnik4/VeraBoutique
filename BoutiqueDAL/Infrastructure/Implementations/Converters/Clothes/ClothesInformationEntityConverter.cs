using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели информации об одежде в модель базы данных
    /// </summary>
    public class ClothesInformationEntityConverter : EntityConverter<int, IClothesInformationDomain, ClothesInformationEntity>,
                                                     IClothesInformationEntityConverter
    {
        public ClothesInformationEntityConverter(IColorClothesEntityConverter colorClothesEntityConverter,
                                                 ISizeGroupEntityConverter sizeGroupEntityConverter)
        {
            _colorClothesEntityConverter = colorClothesEntityConverter;
            _sizeGroupEntityConverter = sizeGroupEntityConverter;
        }

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly IColorClothesEntityConverter _colorClothesEntityConverter;

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ISizeGroupEntityConverter _sizeGroupEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IClothesInformationDomain FromEntity(ClothesInformationEntity clothesInformationEntity) =>
            new ClothesInformationDomain(clothesInformationEntity.Id, clothesInformationEntity.Name,
                                         clothesInformationEntity.Description,
                                         ColorClothesDomainsFromComposite(clothesInformationEntity.ClothesColorCompositeEntities),
                                         SizeGroupDomainsFromComposite(clothesInformationEntity.ClothesSizeGroupCompositeEntities),
                                         clothesInformationEntity.Price, clothesInformationEntity.Image);

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesInformationEntity ToEntity(IClothesInformationDomain clothesShortDomain) =>
            new ClothesInformationEntity(clothesShortDomain.Id, clothesShortDomain.Name,
                                         clothesShortDomain.Description,
                                         ColorClothesToCompositeEntities(clothesShortDomain.Colors, clothesShortDomain.Id),
                                         SizeGroupToCompositeEntities(clothesShortDomain.SizeGroups, clothesShortDomain.Id),
                                         clothesShortDomain.Price, clothesShortDomain.Image);

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию цветов
        /// </summary>
        private IEnumerable<IColorClothesDomain> ColorClothesDomainsFromComposite(IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities) =>
            clothesColorCompositeEntities.
            Select(clothesColorCompositeEntity => clothesColorCompositeEntity.ColorClothesEntity).
            Where(colorClothesEntity => colorClothesEntity != null).
            Select(colorClothesEntity => _colorClothesEntityConverter.FromEntity(colorClothesEntity!));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        private IEnumerable<ISizeGroupDomain> SizeGroupDomainsFromComposite(IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeCompositeEntities) =>
            clothesSizeCompositeEntities.
            Select(clothesSizeCompositeEntity => clothesSizeCompositeEntity.SizeGroupEntity).
            Where(sizeGroupEntity => sizeGroupEntity != null).
            Select(sizeGroupEntity => _sizeGroupEntityConverter.FromEntity(sizeGroupEntity!));

        /// <summary>
        /// Преобразовать цвета в связующую сущность
        /// </summary>
        private IEnumerable<ClothesColorCompositeEntity> ColorClothesToCompositeEntities(IEnumerable<IColorClothesDomain> colorClothesDomains, 
                                                                                         int clothesId) =>
            _colorClothesEntityConverter.ToEntities(colorClothesDomains).
            Select(colorClothesEntity => new ClothesColorCompositeEntity(clothesId, colorClothesEntity.Name));


        /// <summary>
        /// Преобразовать размеры в связующую сущность
        /// </summary>
        private IEnumerable<ClothesSizeGroupCompositeEntity> SizeGroupToCompositeEntities(IEnumerable<ISizeGroupDomain> sizeGroupDomains,
                                                                                          int clothesId) =>
            _sizeGroupEntityConverter.ToEntities(sizeGroupDomains).
            Select(sizeGroupEntity => new ClothesSizeGroupCompositeEntity(clothesId, sizeGroupEntity.ClothesSizeType, sizeGroupEntity.SizeNormalize));


    }
}