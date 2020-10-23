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
               new ClothesInformationEntity(10000, "Платье", 55m, Properties.Resources.TestImage, "Платье пернатое"),
               new ClothesInformationEntity(10001, "Джинсы", 35m, Properties.Resources.TestImage, "Джинсы резанные"),
           };
    }
}