using System.Collections.Generic;
using System.ComponentModel.Design;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.ClothesValidate
{
    /// <summary>
    /// Сервис проверки данных из базы изображений одежды
    /// </summary>
    public interface IClothesImageDatabaseValidateService : IDatabaseValidateService<int, IClothesImageDomain>
    {
        /// <summary>
        /// Проверка на наличие главного изображения
        /// </summary>
        IResultError ValidateByMain(IEnumerable<IClothesImageDomain> clothesImages);
    }
}