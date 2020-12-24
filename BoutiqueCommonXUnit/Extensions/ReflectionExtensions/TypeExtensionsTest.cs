using System.Collections.Generic;
using BoutiqueCommon.Extensions.ReflectionExtensions;
using Xunit;

namespace BoutiqueCommonXUnit.Extensions.ReflectionExtensions
{
    /// <summary>
    /// Методы расширения для типов данных
    /// </summary>
    public class TypeExtensionsTest
    {
        [Fact]
        public void GetNameWithoutGenerics()
        {
            var type = new List<string>().GetType();
            string typeWithoutGeneric = type.GetNameWithoutGenerics();
            
            Assert.Equal("List", typeWithoutGeneric);
        }
    }
}