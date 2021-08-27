using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database
{
    /// <summary>
    /// База данных магазина Entity Framework
    /// </summary>
    public partial class BoutiqueEntityDatabase
    {
        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        public IGenderTable GendersTable =>
            new GenderTable(Genders);

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        public ICategoryTable CategoryTable => 
            new CategoryTable(Categories);

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        public IColorTable ColorTable =>
            new ColorTable(ColorClothes);

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        public IClothesTypeTable ClotheTypeTable =>
            new ClothesTypeTable(ClothesTypes);

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        public ISizeTable SizeTable => 
            new SizeTable(Sizes);

        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        public ISizeGroupTable SizeGroupTable => 
            new SizeGroupTable(SizeGroups);

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        public IClothesImageTable ClothesImageTable =>
            new ClothesImageTable(ClothesImages);

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        public IClothesTable ClothesTable => 
            new ClothesTable(Clothes);
    }
}