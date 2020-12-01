﻿using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers
{
    public class SizeGroupShortTransferConverter : TransferConverter<(ClothesSizeType, int), ISizeGroupShortDomain, SizeGroupShortTransfer>,
                                                   ISizeGroupShortTransferConverter
    {
        /// <summary>
        /// Преобразовать группу размеров одежды в трансферную модель
        /// </summary>
        public override SizeGroupShortTransfer ToTransfer(ISizeGroupShortDomain sizeGroupShortDomain) =>
            new SizeGroupShortTransfer(sizeGroupShortDomain);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IResultValue<ISizeGroupShortDomain> FromTransfer(SizeGroupShortTransfer sizeGroupShortTransfer) =>
            new SizeGroupShortDomain(sizeGroupShortTransfer).
            Map(sizeGroupShortDomain => new ResultValue<ISizeGroupShortDomain>(sizeGroupShortDomain));
    }
}