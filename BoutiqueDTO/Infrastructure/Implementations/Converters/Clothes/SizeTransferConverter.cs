using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер размеров одежды в трансферную модель
    /// </summary>
    public class SizeTransferConverter : TransferConverter<int, ISizeDomain, SizeTransfer>,
                                         ISizeTransferConverter
    {
        /// <summary>
        /// Преобразовать размеры одежды в трансферную модель
        /// </summary>
        public override SizeTransfer ToTransfer(ISizeDomain sizeDomain) =>
            new SizeTransfer(sizeDomain);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IResultValue<ISizeDomain> FromTransfer(SizeTransfer sizeTransfer) =>
            new SizeDomain(sizeTransfer).
            ToResultValue();
    }
}