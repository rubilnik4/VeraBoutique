using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes.Validate
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