using System.Collections.Generic;
using BoutiqueMVC.Models.Implementations.Clothes.Parameters;
using BoutiqueMVC.Models.Interfaces.Clothes;
using VeraBoutique.Models.Implementations.Clothes.Parameters;

namespace BoutiqueMVC.Models.Implementations.Clothes
{
    /// <summary>
    /// Базовый класс одежды
    /// </summary>
    public class Clothes : IClothes
    {
        public Clothes(CategorizationClothes categorization, InformationClothes information,
                       AttributesClothes attributes, Pricing pricing, IReadOnlyList<byte> image)
        {
            Categorization = categorization;
            Information = information;
            Attributes = attributes;
            Pricing = pricing;
            Image = image;
        }

        /// <summary>
        /// Категории и тип одежды
        /// </summary>
        public CategorizationClothes Categorization { get; }

        /// <summary>
        /// Описание
        /// </summary>
        public InformationClothes Information { get; }

        /// <summary>
        /// Параметры одежды
        /// </summary>
        public AttributesClothes Attributes { get; }

        /// <summary>
        /// Цена и количество
        /// </summary>
        public Pricing Pricing { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        public IReadOnlyList<byte> Image { get; }
    }
}