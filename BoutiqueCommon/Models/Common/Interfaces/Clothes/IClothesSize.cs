using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды
    /// </summary>
    public interface IClothesSize: IModel<string>
    {
        /// <summary>
        /// Тип размера одежды
        /// </summary>
        ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Наименование размера
        /// </summary>
        string SizeName { get; }
    }
}