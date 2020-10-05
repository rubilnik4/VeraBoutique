using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Группа размеров одежды разной маркировки
    /// </summary>
    public interface IClothesSizeGroup: IModel<string>
    {
        /// <summary>
        /// Размеры одежды
        /// </summary>
        IReadOnlyCollection<ClothesSize> ClothesSizes { get; }

        /// <summary>
        /// Базовый тип размера группы
        /// </summary>
        ClothesSizeType ClothesSizeBase { get; }

        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
    }
}