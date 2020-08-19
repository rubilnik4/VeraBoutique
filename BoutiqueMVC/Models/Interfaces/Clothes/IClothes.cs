using System.Collections.Generic;
using BoutiqueMVC.Models.Implementations.Clothes.Parameters;
using VeraBoutique.Models.Implementations.Clothes.Parameters;

namespace BoutiqueMVC.Models.Interfaces.Clothes
{
    /// <summary>
    /// Базовый класс одежды
    /// </summary>
    public interface IClothes
    {
        /// <summary>
        /// Категории и тип одежды
        /// </summary>
        CategorizationClothes Categorization { get; }

        /// <summary>
        /// Описание
        /// </summary>
        InformationClothes Information { get; }

        /// <summary>
        /// Параметры одежды
        /// </summary>
        AttributesClothes Attributes { get; }

        /// <summary>
        /// Цена и количество
        /// </summary>
        Pricing Pricing { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        IReadOnlyList<byte> Image { get; }
    }
}