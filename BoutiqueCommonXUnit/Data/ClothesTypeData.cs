using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Данные вида одежды
    /// </summary>
    public static class ClothesTypeData
    {
        /// <summary>
        /// Получить виды одежды
        /// </summary>
        public static List<IClothesTypeDomain> GetClothesTypeDomain() =>
            new List<IClothesTypeDomain>()
            {
                new ClothesTypeDomain("Пиджак"),
                new ClothesTypeDomain("Брюки"),
            };
    }
}