using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер типа пола в трансферную модель
    /// </summary>
    public class GenderTransferConverter: TransferConverter<GenderType, IGenderDomain, IGenderTransfer>,
                                          IGenderTransferConverter
    {
        /// <summary>
        /// Преобразовать пол в трансферную модель
        /// </summary>
        public override IGenderTransfer ToTransfer(IGenderDomain gender) =>
            new GenderTransfer(gender.GenderType, gender.Name);

        /// <summary>
        /// Преобразовать пол из трансферной модели
        /// </summary>
        public override IGenderDomain FromTransfer(IGenderTransfer genderTransfer) =>
            new GenderDomain(genderTransfer.GenderType, genderTransfer.Name);
    }
}