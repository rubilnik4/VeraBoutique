using System.Linq;
using BoutiqueCommon.Extensions.HashCodeExtensions;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommonXUnit.Data.Clothes;
using Xunit;

namespace BoutiqueCommonXUnit.Extensions.HashCodesExtensions
{
    /// <summary>
    /// Методы расширения для нахождения хэш кодов. Тесты
    /// </summary>
    public class HashCodesExtensionsTest
    {
        /// <summary>
        /// Получить хэш-код коллекции одежды
        /// </summary>
        [Fact]
        public void GetHashCodes_Ok()
        {
            var sizeGroups = SizeGroupData.SizeGroupDomains;
            var hashCode = sizeGroups.GetHashCodes();

            Assert.Equal(hashCode, sizeGroups.Average(sizeGroup => sizeGroup.GetHashCode()));
        }

        /// <summary>
        /// Получить хэш-код коллекции одежды
        /// </summary>
        [Fact]
        public void GetHashCodes_Empty()
        {
            var sizeGroups = Enumerable.Empty<ISizeGroupMainDomain>().ToList();
            var hashCode = sizeGroups.GetHashCodes();

            Assert.Equal(hashCode, 0);
        }
    }
}