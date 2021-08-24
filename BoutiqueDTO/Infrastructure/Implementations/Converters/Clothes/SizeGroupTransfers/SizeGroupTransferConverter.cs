using System;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers
{
    public class SizeGroupTransferConverter : TransferConverter<int, ISizeGroupDomain, SizeGroupTransfer>,
                                              ISizeGroupTransferConverter
    {
        /// <summary>
        /// Преобразовать группу размеров одежды в трансферную модель
        /// </summary>
        public override SizeGroupTransfer ToTransfer(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupTransfer(sizeGroupDomain);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IResultValue<ISizeGroupDomain> FromTransfer(SizeGroupTransfer sizeGroupTransfer) =>
            new SizeGroupDomain(sizeGroupTransfer).
            ToResultValue();
    }
}