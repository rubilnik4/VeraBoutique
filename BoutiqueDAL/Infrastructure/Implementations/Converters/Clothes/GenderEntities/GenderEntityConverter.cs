using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.GenderEntities
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
            new GenderDomain(genderEntity).
            ToResultValue();

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public override GenderEntity ToEntity(IGenderDomain genderDomain) =>
            new GenderEntity(genderDomain);
    }
}