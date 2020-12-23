using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая базовая укороченная модель
    /// </summary>
    public interface ITestShortBase:  IModel<TestEnum>, IEquatable<ITestShortBase>
    {
        /// <summary>
        /// Тестовое перечисление
        /// </summary>
        TestEnum TestEnum { get; }

        /// <summary>
        /// Тестовое поле
        /// </summary>
        string Name { get; }
    }
}