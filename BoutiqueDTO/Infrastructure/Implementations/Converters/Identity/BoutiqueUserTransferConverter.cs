using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Identity
{
    /// <summary>
    /// Конвертер пользователей в трансферную модель
    /// </summary>
    public class BoutiqueUserTransferConverter : TransferConverter<string, IBoutiqueUserDomain, BoutiqueUserTransfer>, 
                                                 IBoutiqueUserTransferConverter
    {
        /// <summary>
        /// Преобразовать пользователя в трансферную модель
        /// </summary>
        public override BoutiqueUserTransfer ToTransfer(IBoutiqueUserDomain boutiqueUserDomain) =>
            new BoutiqueUserTransfer(boutiqueUserDomain);

        /// <summary>
        /// Преобразовать пользователя из трансферной модели
        /// </summary>
        public override IResultValue<IBoutiqueUserDomain> FromTransfer(BoutiqueUserTransfer boutiqueUserTransfer) =>
            new BoutiqueUserDomain(boutiqueUserTransfer).
            Map(boutiqueUser => new ResultValue<IBoutiqueUserDomain>(boutiqueUser));
    }
}