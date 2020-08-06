using System.Collections;
using System.Collections.Generic;
using VeraButik.Models.Enums;
using VeraButik.Models.Implementations.Clothes.Parameters;
using VeraButik.Models.Implementations.Clothes.Size;
using VeraButik.Models.Interfaces.Clothes;

namespace VeraButik.Models.Implementations.Clothes
{
    /// <summary>
    /// Базовый класс одежды
    /// </summary>
    public class Clothes : IClothes
    {
        public Clothes(CategorisationClothes categorisation, InformationClothes information,
                       AttributesClothes attributes, Pricing pricing, IReadOnlyList<byte> image)
        {
            Categorisation = categorisation;
            Information = information;
            Attributes = attributes;
            Pricing = pricing;
            Image = image;
        }

        /// <summary>
        /// Категории и тип одежды
        /// </summary>
        public CategorisationClothes Categorisation { get; }

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