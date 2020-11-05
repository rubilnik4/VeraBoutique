using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesEntity : ClothesMain, IClothesEntity
    {
        public ClothesEntity(IClothesShort clothesShort, string description,
                             GenderType genderType, string clothesTypeName)
            : this(clothesShort.Id, clothesShort.Name, clothesShort.Price, clothesShort.Image,
                   description, genderType, clothesTypeName)
        { }

        public ClothesEntity(int id, string name, decimal price, byte[]? image,
                             string description, GenderType genderType, string clothesTypeName)
            : this(id, name,price, image, description,
                   genderType, null, clothesTypeName, null,
                   Enumerable.Empty<ClothesColorCompositeEntity>(),
                   Enumerable.Empty<ClothesSizeGroupCompositeEntity>())
        { }

        public ClothesEntity(IClothesShort clothesShort, string description,
                             GenderEntity gender, ClothesTypeFullEntity clothesType,
                             IEnumerable<ClothesColorCompositeEntity> clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupComposites)
       : this(clothesShort, description, 
              gender.GenderType, gender, clothesType.Name, clothesType,
              clothesColorComposites, clothesSizeGroupComposites)
        { }

        public ClothesEntity(IClothesShort clothesShort, string description,
                             GenderType genderType, GenderEntity? gender,
                             string clothesTypeName, ClothesTypeFullEntity? clothesType,
                             IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites)
      : this(clothesShort.Id, clothesShort.Name, clothesShort.Price, clothesShort.Image, description,
             genderType, gender, clothesTypeName, clothesType,
             clothesColorComposites, clothesSizeGroupComposites)
        { }

        public ClothesEntity(int id, string name, decimal price, byte[]? image, string description,
                             GenderType genderType, GenderEntity? gender,
                             string clothesTypeName, ClothesTypeFullEntity? clothesType,
                             IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites)
          : base(id, name, description, price, image)
        {
            GenderType = genderType;
            Gender = gender;
            ClothesTypeName = clothesTypeName;
            ClothesType = clothesType;
            ClothesColorComposites = clothesColorComposites?.ToList();
            ClothesSizeGroupComposites = clothesSizeGroupComposites?.ToList();
        }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public GenderEntity? Gender { get; }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        public string ClothesTypeName { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public ClothesTypeFullEntity? ClothesType { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity>? ClothesColorComposites { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? ClothesSizeGroupComposites { get; }
    }
}