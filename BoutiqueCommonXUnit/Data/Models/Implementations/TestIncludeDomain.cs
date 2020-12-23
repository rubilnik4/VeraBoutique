using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая включенная доменная модель
    /// </summary>
    public class TestIncludeDomain: TestIncludeBase, ITestIncludeDomain
    {
        public TestIncludeDomain(ITestIncludeBase testInclude)
          : base(testInclude.Name)
        { }

        public TestIncludeDomain(string name) 
            :base(name)
        { }
    }
}