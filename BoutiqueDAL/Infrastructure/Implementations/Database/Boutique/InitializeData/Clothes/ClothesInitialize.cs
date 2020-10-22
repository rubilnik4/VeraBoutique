using System.Collections.Generic;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Clothes
{
    /// <summary>
    /// Начальные данные таблицы одежды
    /// </summary>
    public static class ClothesInitialize
    {
        /// <summary>
        /// Начальные данные таблицы размеров одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesInformationEntity> ClothesInformationEntities =>
           new List<ClothesInformationEntity>
           {
               new ClothesInformationEntity(1, "Платье", "Платье пернатое", 55m, Properties.Resources.TestImage ),
               new ClothesInformationEntity(2, "Джинсы", "Джинсы резанные", 35m, Properties.Resources.TestImage ),
           };
    }
}