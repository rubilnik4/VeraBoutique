using System;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Base;

namespace BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images
{
    /// <summary>
    /// Изображение. Доменная модель
    /// </summary>
    public interface IClothesImageDomain : IClothesImageBase, IDomainModel<Guid>
    { }
}