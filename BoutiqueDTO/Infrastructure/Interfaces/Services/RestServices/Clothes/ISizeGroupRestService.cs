using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис загрузки группы размеров одежды в базу данных
    /// </summary>
    public interface ISizeGroupRestService : IRestServiceBase<int, ISizeGroupDomain>
    { }
}