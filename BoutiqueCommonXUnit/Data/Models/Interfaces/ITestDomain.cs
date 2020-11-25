using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая включенная модель
    /// </summary>
    public interface ITestDomain : ITestShortDomain
    {
        /// <summary>
        /// Включенные сущности
        /// </summary>
        IReadOnlyCollection<ITestIncludeDomain> TestIncludes { get; }
    }
}