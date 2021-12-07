using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Genders
{
    /// <summary>
    /// Тип пола с категориями
    /// </summary>
    public abstract class GenderCategoryBase<TCategory, TClothesType> : GenderBase, IGenderCategoryBase<TCategory, TClothesType>
        where TCategory : ICategoryClothesTypeBase<TClothesType>
        where TClothesType : class, IClothesTypeBase
    {
        protected GenderCategoryBase(IGenderBase gender, IEnumerable<TCategory> categories)
            :this(gender.GenderType, gender.Name, categories)
        { }

        protected GenderCategoryBase(GenderType genderType, string name, IEnumerable<TCategory> categories)
            : base(genderType, name)
        {
            Categories = categories.ToList().AsReadOnly();
        }

        /// <summary>
        /// Категории одежды
        /// </summary>
        public IReadOnlyCollection<TCategory> Categories { get; }
    }
}