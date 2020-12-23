using System.Collections.Generic;
using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Interfaces;

namespace BoutiqueCommonXUnit.Data.Models.Implementations
{
    public class TestShortDomain: TestShortBase, ITestShortDomain
    {
       public TestShortDomain(TestEnum testEnum, string name) 
           :base(testEnum, name)
       { }
    }
}