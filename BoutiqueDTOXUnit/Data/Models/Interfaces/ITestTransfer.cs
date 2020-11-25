using System.Collections.Generic;
using BoutiqueDTOXUnit.Data.Models.Implementations;

namespace BoutiqueDTOXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая трансферная модель
    /// </summary>
    public interface ITestTransfer: ITestShortTransfer
    {
        /// <summary>
        /// Включенные сущности
        /// </summary>
        IReadOnlyCollection<TestIncludeTransfer> TestIncludes { get; }
    }
}