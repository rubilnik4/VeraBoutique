using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public class GenderEntity : Gender, IGenderEntity
    {
        public GenderEntity(GenderType genderType, string name)
            : this(genderType, name,
                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>(),
                   Enumerable.Empty<ClothesInformationEntity>())
        { }

        public GenderEntity(GenderType genderType, string name,
                            IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderEntities,
                            IEnumerable<ClothesInformationEntity>? clothesInformationEntities)
           : base(genderType, name)
        {
            ClothesTypeGenderEntities = clothesTypeGenderEntities?.ToList();
            ClothesInformationEntities = clothesInformationEntities?.ToList();
        }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderEntities { get; }

        /// <summary>
        /// Связующие сущности пола и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesInformationEntity>? ClothesInformationEntities { get; }
    }
}