using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using Newtonsoft.Json;

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

        [JsonConstructor]
        public TestIncludeTransfer(string name) 
            :base(name)
        {}
    }
}