using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping
{
    /// <summary>
    /// Генератор чисел для одежды
    /// </summary>
    public static class ClothesSequences
    {
        /// <summary>
        /// Наименование генератора идентификаторов
        /// </summary>
        public const string CLOTHES_ID_GENERATOR = "ClothesIdGenerator";

        /// <summary>
        /// Стартовое число идентификаторов
        /// </summary>
        public const int CLOTHES_ID_START_NUMBER = 10000;

        /// <summary>
        /// Применить идентификатор к базе
        /// </summary>
        public static void ApplyGenerator(ModelBuilder modelBuilder) =>
            modelBuilder.HasSequence<int>(CLOTHES_ID_GENERATOR).StartsAt(CLOTHES_ID_START_NUMBER).IncrementsBy(1);
        
    }
}