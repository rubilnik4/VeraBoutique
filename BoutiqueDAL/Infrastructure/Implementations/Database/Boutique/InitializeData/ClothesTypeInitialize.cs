using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData
{
    /// <summary>
    /// Начальные данные таблицы, связующей пол и вид одежды
    /// </summary>
    public class ClothesTypeInitialize
    {
        /// <summary>
        /// Начальные данные вида одежды
        /// </summary>
        public static IReadOnlyCollection<ClothesTypeEntity> ClothesTypeData =>
            new List<ClothesTypeEntity>()
            {
                new ClothesTypeEntity("Пальто"),
                new ClothesTypeEntity("Куртки"),
                new ClothesTypeEntity("Толстовки"),
                new ClothesTypeEntity("Свитера"),
                new ClothesTypeEntity("Рубашки"),
                new ClothesTypeEntity("Брюки"),
                new ClothesTypeEntity("Джинсы"),
                new ClothesTypeEntity("Футболки"),
                new ClothesTypeEntity("Обувь"),
                new ClothesTypeEntity("Шорты"),
                new ClothesTypeEntity("Платья"),
                new ClothesTypeEntity("Юбки"),
            }.AsReadOnly();
    }
}