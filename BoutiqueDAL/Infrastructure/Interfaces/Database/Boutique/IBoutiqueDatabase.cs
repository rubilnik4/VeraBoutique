using System;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique
{
    /// <summary>
    /// База данных магазина
    /// </summary>
    public interface IBoutiqueDatabase : IDatabase, IDisposable
    {
        /// <summary>
        /// Таблица пола базы данных
        /// </summary>
        IGenderTable GendersTable { get; }

        /// <summary>
        /// Таблица базы данных категорий одежды
        /// </summary>
        ICategoryTable CategoryTable { get; }

        /// <summary>
        /// Таблица базы данных изображений одежды
        /// </summary>
        IClothesImageTable ClothesImageTable { get; }

        /// <summary>
        /// Таблица базы данных вида одежды
        /// </summary>
        IClothesTypeTable ClotheTypeTable { get; }

        /// <summary>
        /// Таблица базы данных размеров одежды
        /// </summary>
        ISizeTable SizeTable { get; } 

        /// <summary>
        /// Таблица базы данных группы размеров одежды
        /// </summary>
        ISizeGroupTable SizeGroupTable { get; }

        /// <summary>
        /// Таблица базы данных цвета одежды
        /// </summary>
        IColorClothesTable ColorClothesTable { get; }

        /// <summary>
        /// Таблица базы данных одежды
        /// </summary>
        IClothesTable ClothesTable { get; }
    }
}