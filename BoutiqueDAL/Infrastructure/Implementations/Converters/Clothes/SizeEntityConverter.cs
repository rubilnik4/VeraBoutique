using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели размера одежды в модель базы данных
    /// </summary>
    public class SizeEntityConverter : EntityConverter<(SizeType, string), ISizeDomain, SizeEntity>,
                                       ISizeEntityConverter
    {
        /// <summary>
        /// Преобразовать размер одежды из модели базы данных
        /// </summary>
        public override IResultValue<ISizeDomain> FromEntity(SizeEntity sizeEntity) =>
            new SizeDomain(sizeEntity.SizeType,  sizeEntity.Name).
            Map(size => new ResultValue<ISizeDomain>(size));

        /// <summary>
        /// Преобразовать размер одежды в модель базы данных
        /// </summary>
        public override SizeEntity ToEntity(ISizeDomain sizeDomain) =>
            new SizeEntity(sizeDomain.SizeType, sizeDomain.Name);
    }
}