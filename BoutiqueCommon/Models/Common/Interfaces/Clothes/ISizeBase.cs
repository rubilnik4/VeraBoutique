using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды
    /// </summary>
    public interface ISizeBase: IModel<(SizeType, string)>
    {
        /// <summary>
        /// Тип размера одежды
        /// </summary>
        SizeType SizeType { get; }

        /// <summary>
        /// Наименование размера
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        string SizeNameShort { get; }
    }
}