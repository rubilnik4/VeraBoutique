﻿using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers
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