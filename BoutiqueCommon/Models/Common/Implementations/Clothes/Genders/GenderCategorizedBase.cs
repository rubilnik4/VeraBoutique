using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Genders
{
    /// <summary>
    /// Тип пола с категориями
    /// </summary>
    public class GenderCategorizedBase<TCategory> : GenderBase, IGenderCategorizedBase<TCategory>
        where TCategory : ICategoryBase
    {
        public GenderCategorizedBase(IGenderBase gender, IEnumerable<TCategory> categories)
            :this(gender.GenderType, gender.Name, categories)
        { }

        public GenderCategorizedBase(GenderType genderType, string name, IEnumerable<TCategory> categories)
            : base(genderType, name)
        {
            Categories = categories.ToList().AsReadOnly();
        }

        /// <summary>
        /// Категории одежды
        /// </summary>
        public IReadOnlyCollection<TCategory> Categories { get; }

        #region IEquatable
        public override bool Equals(object? obj) => obj is IGenderCategorizedBase<TCategory> genderCategorized && 
                                                    Equals(genderCategorized);

        public bool Equals(IGenderCategorizedBase<TCategory>? other) =>
            other?.Id == Id &&
            other?.Categories.SequenceEqual(Categories) == true;

        public override int GetHashCode() => HashCode.Combine(GenderType, CategoryBase.GetCategoriesHashCodes(Categories));
        #endregion
    }
}