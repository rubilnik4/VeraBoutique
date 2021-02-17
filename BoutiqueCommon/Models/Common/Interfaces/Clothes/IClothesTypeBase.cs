using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Вид одежды. Базовые данные
    /// </summary>
    public interface IClothesTypeBase : IModel<string>, IEquatable<IClothesTypeBase>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Категория
        /// </summary>
        string CategoryName { get; }
    }
}