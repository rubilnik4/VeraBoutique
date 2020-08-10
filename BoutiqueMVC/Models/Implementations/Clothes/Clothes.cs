using System.Collections.Generic;
using VeraBoutique.Models.Implementations.Clothes.Parameters;
using VeraBoutique.Models.Interfaces.Clothes;

namespace VeraBoutique.Models.Implementations.Clothes
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