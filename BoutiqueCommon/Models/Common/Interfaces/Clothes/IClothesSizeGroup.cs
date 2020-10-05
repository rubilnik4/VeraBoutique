using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа
    /// </summary>
    public interface IClothesSizeGroup: IModel<string>
    {
        /// <summary>
        /// Базовый размер одежды
        /// </summary>
        IClothesSize ClothesSizeBase { get; }

        /// <summary>
        /// Дополнительные размеры одежды
        /// </summary>
        IReadOnlyCollection<IClothesSize> ClothesSizesAdditional { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}