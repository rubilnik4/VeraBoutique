using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды
    /// </summary>
    public interface ISize: IModel<(SizeType, string)>, IEquatable<ISize>
    {
        /// <summary>
        /// Тип размера одежды
        /// </summary>
        SizeType SizeType { get; }

        /// <summary>
        /// Наименование размера
        /// </summary>
        string SizeName { get; }
    }
}