using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Implementation.Clothes
{
    /// <summary>
    /// Пол для одежды
    /// </summary>
    public class Gender
    {
        public Gender(GenderType genderType, string name)
        {
            GenderType = genderType;
            Name = name;
        }

        /// <summary>
        /// Тип пола
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; }
    }
}