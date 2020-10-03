﻿using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Вид одежды. Сущность базы данных
    /// </summary>
    public class ClothesTypeEntity : ClothesType, IClothesTypeEntity
    {
        public ClothesTypeEntity(string name)
            : this(name, null, null, Enumerable.Empty<ClothesTypeGenderEntity>())
        { }

        public ClothesTypeEntity(string name, string? categoryEntityId)
            : this(name, categoryEntityId, null, Enumerable.Empty<ClothesTypeGenderEntity>())
        { }

        public ClothesTypeEntity(string name, IEnumerable<ClothesTypeGenderEntity> clothesTypeGenderEntities)
            : this(name, null, null, clothesTypeGenderEntities)
        { }

        public ClothesTypeEntity(string name, string? categoryEntityId, CategoryEntity? categoryEntity,
                                 IEnumerable<ClothesTypeGenderEntity> clothesTypeGenderEntities)
           : base(name)
        {
            CategoryEntityId = categoryEntityId;
            CategoryEntity = categoryEntity;
            ClothesTypeGenderEntities = clothesTypeGenderEntities.ToList();
        }

        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        public string? CategoryEntityId { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        public CategoryEntity? CategoryEntity { get; }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderEntity> ClothesTypeGenderEntities { get; }
    }
}