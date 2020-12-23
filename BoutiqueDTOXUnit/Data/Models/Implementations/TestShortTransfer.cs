using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;

namespace BoutiqueDTOXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая трансферная модель основных данных
    /// </summary>
    public class TestShortTransfer : TestShortBase, ITestShortTransfer
    {
        public TestShortTransfer(TestEnum testEnum, string name)
            :base(testEnum, name)
        { }
    }
}