using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Identity;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель
    /// </summary>
    public class SizeGroupTransferConverter : TransferConverter<int, ISizeGroupDomain, SizeGroupFullTransfer>,
                                              ISizeGroupTransferConverter
    {
        public SizeGroupTransferConverter(ISizeTransferConverter sizeTransferConverter)
        {
            _sizeTransferConverter = sizeTransferConverter;
        }

        /// <summary>
        /// Конвертер размеров одежды в трансферную модель
        /// </summary>
        private readonly ISizeTransferConverter _sizeTransferConverter;

        /// <summary>
        /// Преобразовать группу размеров одежды в трансферную модель
        /// </summary>
        public override SizeGroupFullTransfer ToTransfer(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupTransfer(sizeGroupDomain, _sizeTransferConverter.ToTransfers(sizeGroupDomain.Sizes));

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IResultValue<ISizeGroupDomain> FromTransfer(SizeGroupFullTransfer sizeGroupTransfer) =>
            GetSizeGroupFunc(sizeGroupTransfer).
            ResultValueCurryOk(_sizeTransferConverter.GetDomains(sizeGroupTransfer.Sizes)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения группы размеров
        /// </summary>
        private static IResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupDomain>> GetSizeGroupFunc(ISizeGroupBase sizeGroup) =>
            new ResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupDomain>>(
                sizes => new SizeGroupDomain(sizeGroup, sizes));
    }
}