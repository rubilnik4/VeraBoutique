using System;
using Functional.FunctionalExtensions.Sync;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Calculate
{
    /// <summary>
    /// Расчет цены
    /// </summary>
    public static class ClothesPrices
    {
        /// <summary>
        /// Рассчитать шаг цены
        /// </summary>
        public static int GetPriceStep(decimal price) =>
            Math.Log10((double)price).
            Map(Math.Ceiling).
            Map(pow => Math.Pow(10, pow) / 100).
            Map(step => (int)Math.Floor(step)).
            Map(step => Math.Max(step, 1));
    }
}