using BoutiqueXamarinCommon.Infrastructure.Implementations.Calculate;
using Xunit;

namespace BoutiqueXamarinCommonXUnit.Infrastructure.Calculate
{
    /// <summary>
    /// Расчет цены. Тесты
    /// </summary>
    public class ClothesPricesTest
    {
        /// <summary>
        /// 
        /// </summary>
        [Theory]
        [InlineData(99999, 1000)]
        [InlineData(5555, 100)]
        [InlineData(1, 1)]
        [InlineData(0, 1)]
        public void GetPriceStep(decimal price, int stepResult)
        {
            int step = ClothesPrices.GetPriceStep(price);

            Assert.Equal(stepResult, step);
        }
    }
}