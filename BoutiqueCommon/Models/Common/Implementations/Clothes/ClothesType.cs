using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Implementations.Clothes
{
    /// <summary>
    /// Вид одежды
    /// </summary>
    public class ClothesType: IClothesType
    {
        public ClothesType(string name, IEnumerable<GenderType> genders)
        {
            Name = name;
            Genders = genders.ToList().AsReadOnly();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => Name;

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Типы пола
        /// </summary>
        public IReadOnlyCollection<GenderType> Genders { get; }
    }
}