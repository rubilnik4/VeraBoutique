using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.SizeGroupEntities;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class SizeGroupEntityConverter : EntityConverter<int, ISizeGroupDomain, SizeGroupEntity>,
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
        public override IResultValue<ISizeGroupDomain> FromEntity(SizeGroupEntity sizeGroupEntity) =>
            GetSizeGroupFunc(sizeGroupEntity).           
            ResultValueCurryOk(SizeDomainsFromComposite(sizeGroupEntity.SizeGroupComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать группу размеров одежды в модель базы данных
        /// </summary>
        public override SizeGroupEntity ToEntity(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupEntity(sizeGroupDomain, SizeToCompositeEntities(sizeGroupDomain.Sizes, sizeGroupDomain));

        /// <summary>
        /// Функция получения группы размеров одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupDomain>> GetSizeGroupFunc(ISizeGroupShortBase sizeGroup) =>
            new ResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupDomain>>(
                sizeDomains => new SizeGroupDomain(sizeGroup, sizeDomains));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        private IResultCollection<ISizeDomain> SizeDomainsFromComposite(IEnumerable<SizeGroupCompositeEntity>? sizeGroupCompositeEntities) =>
            sizeGroupCompositeEntities.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(sizeGroupCompositeEntities))).
            ResultValueBindOkToCollection(GetSizes).
            ResultCollectionBindOk(sizeEntities => _sizeEntityConverter.FromEntities(sizeEntities));

        /// <summary>
        /// Преобразовать размеры в связующую сущность
        /// </summary>
        private IEnumerable<SizeGroupCompositeEntity> SizeToCompositeEntities(IEnumerable<ISizeDomain> sizeDomains,
                                                                              ISizeGroupShortBase sizeGroup) =>
            _sizeEntityConverter.ToEntities(sizeDomains).
            Select(sizeEntity => new SizeGroupCompositeEntity(sizeEntity.Id, sizeGroup.Id));

        /// <summary>
        /// Получить сущности размера одежды
        /// </summary>
        private static IResultCollection<SizeEntity> GetSizes(IEnumerable<SizeGroupCompositeEntity> sizeGroupCompositeEntities) =>
            sizeGroupCompositeEntities.
            Select(sizeGroup => sizeGroup.Size.
                                ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(sizeGroup.Size)))).
            ToResultCollection();
    }
}