using System;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class SizeGroupEntityConverter: EntityConverter<int, ISizeGroupDomain, SizeGroupEntity>,
                                           ISizeGroupEntityConverter
    {
        /// <summary>
        /// Преобразовать группу размеров одежды из модели базы данных
        /// </summary>
        public override IResultValue<ISizeGroupDomain> FromEntity(SizeGroupEntity sizeGroupEntity) =>
            new SizeGroupDomain(sizeGroupEntity).
            ToResultValue();

        /// <summary>
        /// Преобразовать группу размеров одежды в модель базы данных
        /// </summary>
        public override SizeGroupEntity ToEntity(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupEntity(sizeGroupDomain);
    }
}