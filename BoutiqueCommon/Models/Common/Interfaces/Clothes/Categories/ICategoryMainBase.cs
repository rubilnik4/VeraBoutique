using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories
{
    /// <summary>
    /// Категория одежды. Основная модель
    /// </summary>
    public interface ICategoryMainBase<TGender>: ICategoryBase, IEquatable<ICategoryMainBase<TGender>>
        where  TGender: IGenderBase
    {
        /// <summary>
        /// Типы пола
        /// </summary>
        IReadOnlyCollection<TGender> Genders { get; }
    }
}