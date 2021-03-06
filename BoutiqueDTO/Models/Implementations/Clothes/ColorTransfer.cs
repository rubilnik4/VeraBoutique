﻿using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;
using Newtonsoft.Json;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Цвет одежды. Трансферная модель
    /// </summary>
    public class ColorTransfer : ColorBase, IColorTransfer
    {
        public ColorTransfer(IColorBase color)
            :this (color.Name)
        { }

        [JsonConstructor]
        public ColorTransfer(string name)
            :base (name)
        { }
    }
}