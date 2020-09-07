using System;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Clothes
{
    /// <summary>
    /// Тип пола
    /// </summary>
    public interface IGender: IModel<GenderType>, IEquatable<IGender>
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