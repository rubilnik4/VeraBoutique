using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommonXUnit.Data
{
    /// <summary>
    /// Данные типа пола
    /// </summary>
    public static class GenderData
    {
        /// <summary>
        /// Получить типы пола
        /// </summary>
        public static List<IGenderDomain> GetGendersDomain() =>
            new List<IGenderDomain>()
            {
                new GenderDomain(GenderType.Male, "Мужик" ),
                new GenderDomain(GenderType.Female, "Тетя"),
            };
    }
}