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
                new GenderEntity(GenderType.Female, "Женщины"),
                new GenderEntity(GenderType.Male, "Мужчины"),
                 new GenderEntity(GenderType.Child, "Дети"),
            }.AsReadOnly();
    }
}