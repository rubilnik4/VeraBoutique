using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели размера одежды в модель базы данных
    /// </summary>
    public class SizeEntityConverter : EntityConverter<(SizeType, int), ISizeDomain, SizeEntity>, ISizeEntityConverter
    {
        /// <summary>
        /// Преобразовать размер одежды из модели базы данных
        /// </summary>
        public override ISizeDomain FromEntity(SizeEntity sizeEntity) =>
            new SizeDomain(sizeEntity.SizeType, sizeEntity.SizeValue, sizeEntity.SizeName);

        /// <summary>
        /// Преобразовать размер одежды в модель базы данных
        /// </summary>
        public override SizeEntity ToEntity(ISizeDomain sizeDomain) =>
            new SizeEntity(sizeDomain.SizeType, sizeDomain.SizeValue, sizeDomain.SizeName);
    }
}