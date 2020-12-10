using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Цвет одежды
    /// </summary>
    public interface IColorBase : IModel<string>, IEquatable<IColorBase>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}