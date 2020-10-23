using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class SizeGroupEntityConverter : EntityConverter<(ClothesSizeType, int), ISizeGroupDomain, ISizeGroupEntity, SizeGroupEntity>,
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
        public override ISizeGroupDomain FromEntity(ISizeGroupEntity sizeGroupEntity) =>
            new SizeGroupDomain(sizeGroupEntity.ClothesSizeType, sizeGroupEntity.SizeNormalize,
                                SizeDomainsFromComposite(sizeGroupEntity.SizeGroupCompositeEntities));

        /// <summary>
        /// Преобразовать группу размеров одежды в модель базы данных
        /// </summary>
        public override SizeGroupEntity ToEntity(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupEntity(sizeGroupDomain.ClothesSizeType, sizeGroupDomain.SizeNormalize,
                                SizeToCompositeEntities(sizeGroupDomain.Sizes, sizeGroupDomain.ClothesSizeType,
                                                        sizeGroupDomain.SizeNormalize));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        private IEnumerable<ISizeDomain> SizeDomainsFromComposite(IEnumerable<SizeGroupCompositeEntity> sizeGroupCompositeEntities) =>
            sizeGroupCompositeEntities.
            Select(sizeGroupComposite => sizeGroupComposite.SizeEntity).
            Where(sizeEntity => sizeEntity != null).
            Select(sizeEntity => _sizeEntityConverter.FromEntity(sizeEntity!));

        /// <summary>
        /// Преобразовать размеры в связующую сущность
        /// </summary>
        private IEnumerable<SizeGroupCompositeEntity> SizeToCompositeEntities(IEnumerable<ISizeDomain> sizeDomains,
                                                                              ClothesSizeType clothesSizeType, int sizeNormalize) =>
            _sizeEntityConverter.ToEntities(sizeDomains).
            Select(sizeEntity => new SizeGroupCompositeEntity(sizeEntity.SizeType, sizeEntity.SizeName,
                                                              clothesSizeType, sizeNormalize,
                                                              new SizeEntity(sizeEntity.SizeType, sizeEntity.SizeName), null));
    }
}