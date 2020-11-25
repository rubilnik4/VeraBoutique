using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Вид одежды
    /// </summary>
    public interface IClothesType : IModel<string>, IEquatable<IClothesType>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}