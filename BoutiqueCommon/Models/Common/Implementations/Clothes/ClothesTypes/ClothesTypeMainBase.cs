﻿using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.ClothesTypes
{
    public abstract class ClothesTypeMainBase<TCategory> : ClothesTypeBase, IClothesTypeMainBase<TCategory>
        where TCategory : ICategoryBase
    {
        protected ClothesTypeMainBase(string name, SizeType sizeTypeDefault, TCategory category)
            : base(name, sizeTypeDefault, category.Name)
        {
            Category = category;
        }

        /// <summary>
        /// Категория одежды
        /// </summary>
        public TCategory Category { get; }
    }
}