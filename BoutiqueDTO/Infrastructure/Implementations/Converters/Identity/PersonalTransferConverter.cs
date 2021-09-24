using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Identity
{
    /// <summary>
    /// Конвертер личных данных в трансферную модель
    /// </summary>
    public class PersonalTransferConverter: TransferConverter<string, IPersonalDomain, PersonalTransfer>, IPersonalTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override PersonalTransfer ToTransfer(IPersonalDomain personalDomain) =>
            new PersonalTransfer(personalDomain);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IPersonalDomain> FromTransfer(PersonalTransfer personalTransfer) =>
            new PersonalDomain(personalTransfer).
            Map(personal => new ResultValue<IPersonalDomain>(personal));
    }
}