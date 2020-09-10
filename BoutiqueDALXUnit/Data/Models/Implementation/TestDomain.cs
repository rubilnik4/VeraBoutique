using System;
using BoutiqueDALXUnit.Data.Database.Interfaces;
using BoutiqueDALXUnit.Data.Models.Implementation;
using BoutiqueDALXUnit.Data.Models.Interfaces;

namespace BoutiqueDALXUnit.Data.Database.Implementation
{
    public class TestDomain: Test, ITestDomain
    {
        public TestDomain(TestEnum testEnum, string name)
            :base(testEnum, name)
        { }
    }
}