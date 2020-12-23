using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая базовая модель
    /// </summary>
    public interface ITestBase<TInclude> : ITestShortBase, IEquatable<ITestBase<TInclude>>
        where TInclude : ITestIncludeBase
    {
        /// <summary>
        /// Вложения
        /// </summary>
        IReadOnlyCollection<TInclude> TestIncludes { get; }
    }
}