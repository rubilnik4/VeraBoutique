using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesInformationEntity : ClothesInformation, IClothesInformationEntity
    {
        public ClothesInformationEntity(string name, string description, decimal price, byte[]? image)
          : this(1, name, description, price, image)
        { }

        public ClothesInformationEntity(int id, string name, string description, decimal price, byte[]? image)
            : this(id, name, description, price, image,
                   Enumerable.Empty<ClothesColorCompositeEntity>(),
                   Enumerable.Empty<ClothesSizeGroupCompositeEntity>())
        { }

        public ClothesInformationEntity(int id, string name, string description, decimal price, byte[]? image,
                                        IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities,
                                        IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupCompositeEntities)
          : base(id, name, description, price, image)
        {
            ClothesColorCompositeEntities = clothesColorCompositeEntities.ToList();
            ClothesSizeGroupCompositeEntities = clothesSizeGroupCompositeEntities.ToList();
        }

        public int Generated { get; set; } = 1;

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