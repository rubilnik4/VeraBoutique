using System.Collections.Generic;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData
{
    /// <summary>
    /// Начальные данные таблицы типа пола
    /// </summary>
    public static class GenderInitialize
    {
        /// <summary>
        /// Начальные данные типа пола
        /// </summary>
        public static IReadOnlyCollection<GenderEntity> GenderData =>
            new List<GenderEntity>()
            {
               Male,
               Female,
               Child,
            }.AsReadOnly();

        /// <summary>
        /// Мужской и женский пол
        /// </summary>
        public static IReadOnlyCollection<GenderEntity> MaleAndFemale =>
         new List<GenderEntity>()
            {
               Male,
               Female,
            }.AsReadOnly();

        /// <summary>
        /// Мужской пол
        /// </summary>
        public static GenderEntity Male => new GenderEntity(GenderType.Male, "Мужчины");

        /// <summary>
        /// Женский пол
        /// </summary>
        public static GenderEntity Female => new GenderEntity(GenderType.Female, "Женщины");

        /// <summary>
        /// Детский пол
        /// </summary>
        public static GenderEntity Child => new GenderEntity(GenderType.Child, "Дети");
    }
}