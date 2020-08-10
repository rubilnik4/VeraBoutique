using System;

namespace VeraBoutique.Models.Implementations.Clothes.Parameters
{
    /// <summary>
    /// Описание товара
    /// </summary>
    public class InformationClothes
    {
        public InformationClothes(string shortName, string name, string description)
        {
            if (String.IsNullOrWhiteSpace(shortName)) throw new ArgumentNullException(nameof(shortName));
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            if (String.IsNullOrWhiteSpace(description)) throw new ArgumentNullException(nameof(description));

            ShortName = shortName;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Укороченное наименование
        /// </summary>
        public string ShortName { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; }
    }
}