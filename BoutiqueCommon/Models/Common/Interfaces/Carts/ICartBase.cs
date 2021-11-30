using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Base;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;

namespace BoutiqueCommon.Models.Common.Interfaces.Carts
{
    /// <summary>
    /// Корзина
    /// </summary>
    public interface ICartBase: IModel<string>, IEquatable<ICartBase>
    { }
}