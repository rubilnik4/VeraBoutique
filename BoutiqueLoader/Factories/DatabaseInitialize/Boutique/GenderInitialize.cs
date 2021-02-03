using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueLoader.Factories.DatabaseInitialize.Boutique
{
    public static class GenderInitialize
    {
        /// <summary>
        /// Типы пола
        /// </summary>
        public static IEnumerable<IGenderDomain> Genders =>
            new List<IGenderDomain>
            {
                Male,
                Female,
                Child
            };

        /// <summary>
        /// Мужской пол
        /// </summary>
        public static IGenderDomain Male =>
            new GenderDomain(GenderType.Male, "Мужчины");

        /// <summary>
        /// Женский пол
        /// </summary>
        public static IGenderDomain Female =>
            new GenderDomain(GenderType.Female, "Женщины");

        /// <summary>
        /// Детский пол
        /// </summary>
        public static IGenderDomain Child =>
            new GenderDomain(GenderType.Child, "Дети");

        /// <summary>
        /// Мужской и женский пол
        /// </summary>
        public static IReadOnlyCollection<IGenderDomain> MaleAndFemale =>
            new List<IGenderDomain>()
            {
                Male,
                Female
            };
    }
}