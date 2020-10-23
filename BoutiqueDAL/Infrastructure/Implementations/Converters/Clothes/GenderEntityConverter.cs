using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели типа пола в модель базы данных
    /// </summary>
    public class GenderEntityConverter: EntityConverter<GenderType, IGenderDomain, IGenderEntity, GenderEntity>,
                                        IGenderEntityConverter
    {
        /// <summary>
        /// Преобразовать тип пола из модели базы данных
        /// </summary>
        public override IGenderDomain FromEntity(IGenderEntity genderEntity) =>
            new GenderDomain(genderEntity.GenderType, genderEntity.Name);

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public override GenderEntity ToEntity(IGenderDomain genderDomain) =>
            new GenderEntity(genderDomain.GenderType, genderDomain.Name);
    }
}