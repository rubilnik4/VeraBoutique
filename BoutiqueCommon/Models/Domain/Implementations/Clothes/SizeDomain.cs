using System;
using System.Globalization;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueCommon.Models.Domain.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Доменная модель
    /// </summary>
    public class SizeDomain : SizeBase, ISizeDomain
    {
        public SizeDomain(ISizeBase size)
           : base(size.SizeType, size.Name)
        { }

        public SizeDomain(SizeType sizeType, string name)
            : base(sizeType, name)
        { }
    }
}