using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Тип пола. Структура базы данных
    /// </summary>
    public class GenderEntity : GenderBase, IGenderEntity
    {
        public GenderEntity(IGenderBase gender)
            : this(gender.GenderType, gender.Name)
        { }

        public GenderEntity(GenderType genderType, string name)
            : this(genderType, name, null, null)
        { }

        public GenderEntity(GenderType genderType, string name,
                            IEnumerable<GenderCategoryCompositeEntity>? genderCategoryComposites,
                            IEnumerable<ClothesEntity>? clothes)
           : base(genderType, name)
        {
            GenderCategoryComposites = genderCategoryComposites?.ToList();
            Clothes = clothes?.ToList();
        }

        /// <summary>
        /// Связующие сущности типа пола и категории одежды
        /// </summary>
        public IReadOnlyCollection<GenderCategoryCompositeEntity>? GenderCategoryComposites { get; }

        /// <summary>
        /// Связующие сущности пола и одежды
        /// </summary>
        public IReadOnlyCollection<ClothesEntity>? Clothes { get; }
    }
}