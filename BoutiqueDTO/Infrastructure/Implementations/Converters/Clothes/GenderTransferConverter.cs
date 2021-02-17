using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Interfaces.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public class GenderTransferConverter: TransferConverter<GenderType, IGenderDomain, GenderTransfer>,
                                          IGenderTransferConverter
    {
        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public override GenderTransfer ToTransfer(IGenderDomain genderDomain) =>
            new GenderTransfer(genderDomain.GenderType, genderDomain.Name);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IResultValue<IGenderDomain> FromTransfer(GenderTransfer genderTransfer) =>
            new GenderDomain(genderTransfer.GenderType, genderTransfer.Name).
            Map(genderDomain => new ResultValue<IGenderDomain>(genderDomain));
    }
}