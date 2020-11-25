using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    /// <summary>
    /// Тестовая включенная доменная модель
    /// </summary>
    public class TestIncludeDomain: TestInclude, ITestIncludeDomain
    {
        public TestIncludeDomain(string name) 
            :base(name)
        { }
    }
}