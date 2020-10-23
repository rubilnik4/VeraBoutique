using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesInformationEntity : ClothesInformation, IClothesInformationEntity
    {
        public ClothesInformationEntity(string name, decimal price, byte[]? image, string description)
          : this(0, name, price, image, description)
        { }

        public ClothesInformationEntity(IClothesShort clothesShort, string description)
            : this(clothesShort.Id, clothesShort.Name, clothesShort.Price, clothesShort.Image, description)
        { }

        public ClothesInformationEntity(int id, string name, decimal price, byte[]? image, string description)
            : this(id, name,price, image, description, null, null,
                   Enumerable.Empty<ClothesColorCompositeEntity>(),
                   Enumerable.Empty<ClothesSizeGroupCompositeEntity>())
        { }

        public ClothesInformationEntity(IClothesShort clothesShort, string description,
                                        ClothesTypeEntity clothesTypeEntity,
                                        IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities,
                                        IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupCompositeEntities)
       : this(clothesShort.Id, clothesShort. Name, clothesShort.Price, clothesShort.Image,
              description, clothesTypeEntity.Name, clothesTypeEntity,
              clothesColorCompositeEntities, clothesSizeGroupCompositeEntities)
        { }

        public ClothesInformationEntity(int id, string name, decimal price, byte[]? image,
                                        string description, string? clothesTypeName, ClothesTypeEntity? clothesTypeEntity,
                                        IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities,
                                        IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupCompositeEntities)
          : base(id, name, description, price, image)
        {
            ClothesTypeName = clothesTypeName;
            ClothesTypeEntity = clothesTypeEntity;
            ClothesColorCompositeEntities = clothesColorCompositeEntities.ToList();
            ClothesSizeGroupCompositeEntities = clothesSizeGroupCompositeEntities.ToList();
        }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        public string? ClothesTypeName { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public ClothesTypeEntity? ClothesTypeEntity { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity> ClothesColorCompositeEntities { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity> ClothesSizeGroupCompositeEntities { get; }
    }
}