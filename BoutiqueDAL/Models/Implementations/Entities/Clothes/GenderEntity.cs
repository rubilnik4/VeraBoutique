using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Пол. Структура базы данных
    /// </summary>
    public class GenderEntity : GenderBase, IGenderEntity
    {
        public GenderEntity(GenderType genderType, string name)
            : this(genderType, name,
                   Enumerable.Empty<ClothesTypeGenderCompositeEntity>(),
                   Enumerable.Empty<ClothesEntity>())
        { }

        public GenderEntity(GenderType genderType, string name,
                            IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites,
                            IEnumerable<ClothesEntity>? clothes)
           : base(genderType, name)
        {
            ClothesTypeGenderComposites = clothesTypeGenderComposites?.ToList();
            Clothes = clothes?.ToList();
        }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderCompositeEntity>? ClothesTypeGenderComposites { get; }

        /// <summary>
        /// Связующие сущности пола и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}