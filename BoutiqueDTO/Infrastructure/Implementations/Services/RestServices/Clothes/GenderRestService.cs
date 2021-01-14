﻿using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис загрузки и получения типа пола в базу данных
    /// </summary>
    public class GenderRestService : RestServiceBase<GenderType, IGenderDomain, GenderTransfer>, IGenderRestService
    {
        public GenderRestService(IGenderApiService genderApiService, 
                                 IGenderTransferConverter genderTransferConverter, 
                                 IBoutiqueLogger boutiqueLogger)
            :base(genderApiService, genderTransferConverter,  boutiqueLogger)
        { }
    }
}