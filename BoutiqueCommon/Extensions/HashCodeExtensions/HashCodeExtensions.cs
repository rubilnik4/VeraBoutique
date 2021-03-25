using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;

namespace BoutiqueCommon.Extensions.HashCodeExtensions
{
    /// <summary>
    /// Методы расширения для нахождения хэш кодов
    /// </summary>
    public static class HashCodeExtensions
    {
        /// <summary>
        /// Получить хэш-код коллекции одежды
        /// </summary>
        public static double GetHashCodes<TModel>(this IEnumerable<TModel> clothesModels)
            where TModel : notnull =>
            clothesModels.
            Select(model => model.GetHashCode()).
            DefaultIfEmpty(0).
            Average();
    }
}