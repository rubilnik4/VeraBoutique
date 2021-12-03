using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping.Sequences
{
    /// <summary>
    /// Генератор чисел для базы данных
    /// </summary>
    public class DatabaseSequence
    {
        private DatabaseSequence(string id, int startNumber, int increment)
        {
            Id = id;
            StartNumber = startNumber;
            Increment = increment;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Стартовый номер
        /// </summary>
        public int StartNumber { get; }

        /// <summary>
        /// Коэффициент увеличения
        /// </summary>
        public int Increment { get; }

        /// <summary>
        /// Sql команда
        /// </summary>
        public string SqlSequenceCommand =>
            $"nextval('\"{Id}\"')";

        /// <summary>
        /// Применить идентификатор к базе
        /// </summary>
        public void ApplyGenerator(ModelBuilder modelBuilder) =>
            modelBuilder.HasSequence<int>(Id).StartsAt(StartNumber).IncrementsBy(Increment);

        /// <summary>
        /// Генератор чисел для одежды
        /// </summary>
        public static DatabaseSequence ClothesSequence =>
            new ("ClothesSequence", 10000, 1);

        /// <summary>
        /// Генератор чисел для изображений
        /// </summary>
        public static DatabaseSequence ClothesImageSequence =>
            new ("ClothesImageSequence", 10000, 1);
    }
}