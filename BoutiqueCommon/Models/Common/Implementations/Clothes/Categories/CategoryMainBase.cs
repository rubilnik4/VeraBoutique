using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Categories
{
    /// <summary>
    /// Категория одежды. Основная модель
    /// </summary>
    public abstract class CategoryMainBase<TGender> : CategoryBase, ICategoryMainBase<TGender>
         where TGender : IGenderBase
    {
        protected CategoryMainBase(ICategoryBase category, IEnumerable<TGender> genders)
           : this(category.Name, genders)
        { }

        protected CategoryMainBase(string name, IEnumerable<TGender> genders)
            :base(name)
        {
            Genders = genders.ToList().AsReadOnly();
        }

        /// <summary>
        /// Типы пола
        /// </summary>
        public IReadOnlyCollection<TGender> Genders { get; }
    }
}