using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Models.Common.Implementations.Clothes.SizeGroups;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes
{
    /// <summary>
    /// Одежда
    /// </summary>
    public abstract class ClothesMainBase<TImage, TGender, TClothesType, TColor, TSizeGroup, TSize> :
        ClothesDetailBase<TColor, TSizeGroup, TSize>,
        IClothesMainBase<TImage, TGender, TClothesType, TColor, TSizeGroup, TSize>
        where TImage : IClothesImageBase
        where TGender : IGenderBase
        where TClothesType : IClothesTypeBase
        where TColor : IColorBase
        where TSizeGroup : ISizeGroupMainBase<TSize>
        where TSize : ISizeBase
    {
        protected ClothesMainBase(int id, string name, string description, decimal price, IEnumerable<TImage> images,
                                  TGender gender, TClothesType clothesType,
                                  IEnumerable<TColor> colors, IEnumerable<TSizeGroup> sizeGroups)
            : base(id, name, description, price, gender.GenderType, clothesType.Name, colors, sizeGroups)
        {
            Images = images.ToList();
            Gender = gender;
            ClothesType = clothesType;
        }

        /// <summary>
        /// Изображение
        /// </summary>
        public IReadOnlyCollection<TImage> Images { get; }

        /// <summary>
        /// Тип пола
        /// </summary>
        public TGender Gender { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public TClothesType ClothesType { get; }
    }
}