﻿using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Размер одежды
    /// </summary>
    public interface ISize: IModel<(SizeType, int)>
    {
        /// <summary>
        /// Тип размера одежды
        /// </summary>
        SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        int SizeValue { get; }

        /// <summary>
        /// Наименование размера
        /// </summary>
        string SizeName { get; }

        /// <summary>
        /// Укороченное наименование размера
        /// </summary>
        string ClothesSizeNameShort { get; }
    }
}