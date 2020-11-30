using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.SizeGroupEntities;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities
{
    public class SizeGroupShortEntityConverter : 
        EntityConverter<(ClothesSizeType, int), ISizeGroupShortDomain, SizeGroupShortEntity>,
        ISizeGroupShortEntityConverter
    {
        /// <summary>
        /// Преобразовать группу размеров одежды из модели базы данных
        /// </summary>
        public override IResultValue<ISizeGroupShortDomain> FromEntity(SizeGroupShortEntity sizeGroupShortEntity) =>
            new SizeGroupShortDomain(sizeGroupShortEntity).
            Map(sizeGroup => new ResultValue<ISizeGroupShortDomain>(sizeGroup));

        /// <summary>
        /// Преобразовать группу размеров одежды в модель базы данных
        /// </summary>
        public override SizeGroupShortEntity ToEntity(ISizeGroupShortDomain sizeGroupShortDomain) =>
            new SizeGroupShortEntity(sizeGroupShortDomain);
    }
}