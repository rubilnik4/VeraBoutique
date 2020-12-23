using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая вложенная базовая модель
    /// </summary>
    public interface ITestIncludeBase : IModel<string>, IEquatable<ITestIncludeBase>
    {
        /// <summary>
        /// Имя
        /// </summary>
        string Name { get; }
    }
}