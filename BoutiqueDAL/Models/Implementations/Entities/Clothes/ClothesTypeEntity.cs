using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public class ClothesTypeEntity : ClothesType, IClothesTypeEntity
    {
        public ClothesTypeEntity(string name, string categoryName)
            : this(name, categoryName, null,
                   Enumerable.Empty<ClothesInformationEntity>(),
                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>())
        { }

        public ClothesTypeEntity(string name, CategoryEntity categoryEntity)
            : this(name, categoryEntity.Name, categoryEntity,
                   Enumerable.Empty<ClothesInformationEntity>(),
                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>())
        { }

        public ClothesTypeEntity(string name, string categoryName, CategoryEntity? categoryEntity,
                                 IEnumerable<ClothesInformationEntity>? clothesInformationEntities,
                                 IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderEntities)
           : base(name)
        {
            CategoryName = categoryName;
            CategoryEntity = categoryEntity;
            ClothesInformationEntities = clothesInformationEntities?.ToList();
            ClothesTypeGenderEntities = clothesTypeGenderEntities?.ToList();
        }

        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        public CategoryEntity? CategoryEntity { get; }

        /// <summary>
        /// Связующие сущности категории и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesInformationEntity>? ClothesInformationEntities { get; }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderEntities { get; }
    }
}