using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Models.Interfaces;

namespace BoutiqueDTOXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая вложенная трансферная модель
    /// </summary>
    public class TestIncludeTransfer:  TestIncludeBase, ITestIncludeTransfer
    {
        public TestIncludeTransfer(ITestIncludeBase testInclude)
            :this(testInclude.Name)
        { }

        public TestIncludeTransfer(string name) 
            :base(name)
        {}
    }
}