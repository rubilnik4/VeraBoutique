using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
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
            : this(genderType, name, Enumerable.Empty<ClothesTypeGenderEntity>())
        { }

        public GenderEntity(GenderType genderType, string name,
                            IEnumerable<ClothesTypeGenderEntity> clothesTypeGenderEntities)
           : base(genderType, name)
        {
            ClothesTypeGenderEntities = clothesTypeGenderEntities.ToList();
        }

        /// <summary>
        /// Связующие сущности пола и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeGenderEntity> ClothesTypeGenderEntities { get; }
    }
}