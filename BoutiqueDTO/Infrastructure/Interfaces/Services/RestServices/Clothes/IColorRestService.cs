using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Clothes
{
    /// <summary>
    /// Сервис загрузки цвета одежды в базу данных
    /// </summary>
    public interface IColorRestService : IRestServiceBase<string, IColorDomain>
    { }
}