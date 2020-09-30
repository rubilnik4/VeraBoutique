using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая вложенная базовая модель
    /// </summary>
    public interface ITestInclude : IModel<string>, IEquatable<ITestInclude>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; }
    }
}