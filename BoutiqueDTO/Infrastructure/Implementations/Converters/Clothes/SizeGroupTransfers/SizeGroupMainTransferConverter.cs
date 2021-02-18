using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
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
    public class SizeGroupMainTransferConverter : TransferConverter<int, ISizeGroupMainDomain, SizeGroupMainTransfer>,
                                                  ISizeGroupMainTransferConverter
    {
        public SizeGroupMainTransferConverter(ISizeTransferConverter sizeTransferConverter)
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
        public override SizeGroupMainTransfer ToTransfer(ISizeGroupMainDomain sizeGroupMainDomain) =>
            new SizeGroupMainTransfer(sizeGroupMainDomain, _sizeTransferConverter.ToTransfers(sizeGroupMainDomain.Sizes));

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IResultValue<ISizeGroupMainDomain> FromTransfer(SizeGroupMainTransfer sizeGroupMainTransfer) =>
            GetSizeGroupFunc(sizeGroupMainTransfer).
            ResultValueCurryOk(_sizeTransferConverter.GetDomains(sizeGroupMainTransfer.Sizes)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения группы размеров
        /// </summary>
        private static IResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupMainDomain>> GetSizeGroupFunc(ISizeGroupBase sizeGroup) =>
            new ResultValue<Func<IEnumerable<ISizeDomain>, ISizeGroupMainDomain>>(
                sizes => new SizeGroupMainDomain(sizeGroup, sizes));
    }
}