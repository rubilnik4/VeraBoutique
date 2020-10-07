using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class SizeGroupEntityConverter : EntityConverter<(ClothesSizeType, int), ISizeGroupDomain, SizeGroupEntity>,
                                            ISizeGroupEntityConverter
    {
        public SizeGroupEntityConverter(ISizeEntityConverter sizeEntityConverter)
        {
            _sizeEntityConverter = sizeEntityConverter;
        }

        /// <summary>
        /// Преобразования модели размера одежды в модель базы данных
        /// </summary>
        private readonly ISizeEntityConverter _sizeEntityConverter;

        /// <summary>
        /// Преобразовать группу размеров одежды из модели базы данных
        /// </summary>
        public override ISizeGroupDomain FromEntity(SizeGroupEntity sizeGroupEntity) =>
            new SizeGroupDomain(sizeGroupEntity.ClothesSizeType, sizeGroupEntity.SizeNormalize,
                                _sizeEntityConverter.FromEntities(sizeGroupEntity.Sizes));

        /// <summary>
        /// Преобразовать группу размеров одежды в модель базы данных
        /// </summary>
        public override SizeGroupEntity ToEntity(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupEntity(sizeGroupDomain.ClothesSizeType, sizeGroupDomain.SizeNormalize,
                                _sizeEntityConverter.ToEntities(sizeGroupDomain.Sizes));
    }
}