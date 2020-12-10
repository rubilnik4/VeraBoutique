using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели типа пола в модель базы данных
    /// </summary>
    public class GenderEntityConverter: EntityConverter<GenderType, IGenderDomain, GenderEntity>,
                                        IGenderEntityConverter
    {
        /// <summary>
        /// Преобразовать тип пола из модели базы данных
        /// </summary>
        public override IResultValue<IGenderDomain> FromEntity(GenderEntity genderEntity) =>
            new GenderDomain(genderEntity.GenderType, genderEntity.Name).
            Map(gender => new ResultValue<IGenderDomain>(gender));

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public override GenderEntity ToEntity(IGenderDomain genderDomain) =>
            new (genderDomain);
    }
}