using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    public class TestDomain: Test, ITestDomain
    {
        public TestDomain(TestEnum testEnum, string name)
            :base(testEnum, name)
        { }
    }
}