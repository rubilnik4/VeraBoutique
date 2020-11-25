using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Цвет одежды
    /// </summary>
    public interface IColorClothes : IModel<string>, IEquatable<IColorClothes>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}