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
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class SizeGroupMainEntityConverter : EntityConverter<int, ISizeGroupMainDomain, SizeGroupEntity>,
                                                ISizeGroupMainEntityConverter
    {
        public SizeGroupMainEntityConverter(ISizeEntityConverter sizeEntityConverter)
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
        public override IResultValue<ISizeGroupMainDomain> FromEntity(SizeGroupEntity sizeGroupEntity) =>
            GetSizeGroupFunc(sizeGroupEntity).           
            ResultValueCurryOk(SizeDomainsFromComposite(sizeGroupEntity.SizeGroupComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать группу размеров одежды в модель базы данных
        /// </summary>
        public override SizeGroupEntity ToEntity(ISizeGroupMainDomain sizeGroupMainDomain) =>
            new SizeGroupEntity(sizeGroupMainDomain, SizeToCompositeEntities(sizeGroupMainDomain.Sizes, sizeGroupMainDomain));

        /// <summary>
        /// Функция получения группы размеров одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupMainDomain>> GetSizeGroupFunc(ISizeGroupBase sizeGroup) =>
            new ResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupMainDomain>>(
                sizeDomains => new SizeGroupMainDomain(sizeGroup, sizeDomains));

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
                                                                              ISizeGroupBase sizeGroup) =>
            _sizeEntityConverter.ToEntities(sizeDomains).
            Select(sizeEntity => new SizeGroupCompositeEntity(sizeEntity.Id, sizeGroup.Id));

        /// <summary>
        /// Получить сущности размера одежды
        /// </summary>
        private static IResultCollection<SizeEntity> GetSizes(IEnumerable<SizeGroupCompositeEntity> sizeGroupCompositeEntities) =>
            sizeGroupCompositeEntities.
            Select(composite => composite.Size.
                                ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(composite.Size)))).
            ToResultCollection();
    }
}