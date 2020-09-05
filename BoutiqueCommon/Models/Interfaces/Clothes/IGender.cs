using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Interfaces.Base;

namespace BoutiqueCommon.Models.Interfaces.Clothes
{
    /// <summary>
    /// Тип пола
    /// </summary>
    public interface IGender: IDomainModel<GenderType>
    {
        /// <summary>
        /// Тип пола
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}