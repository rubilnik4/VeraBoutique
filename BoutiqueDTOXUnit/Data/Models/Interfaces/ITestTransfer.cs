using System.Collections.Generic;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Models.Implementations;

namespace BoutiqueDTOXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая трансферная модель
    /// </summary>
    public interface ITestTransfer: ITestBase<TestIncludeTransfer>, ITestShortTransfer
    { }
}