using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая базовая модель
    /// </summary>
    public interface ITest : IModel<TestEnum>
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