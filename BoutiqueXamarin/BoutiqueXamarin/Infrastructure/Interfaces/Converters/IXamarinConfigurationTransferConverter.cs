using System;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueXamarin.Models.Implementation.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;

namespace BoutiqueXamarin.Infrastructure.Interfaces.Converters
{
    /// <summary>
    /// Конвертер конфигурации Xamarin  в трансферную модель
    /// </summary>
    public interface IXamarinConfigurationTransferConverter : ITransferConverter<Guid, IXamarinConfigurationDomain, XamarinConfigurationTransfer>
    { }
}