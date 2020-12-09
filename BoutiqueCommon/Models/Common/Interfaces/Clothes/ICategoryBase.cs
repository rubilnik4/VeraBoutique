using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Категория одежды
    /// </summary>
    public interface ICategoryBase : IModel<string>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}