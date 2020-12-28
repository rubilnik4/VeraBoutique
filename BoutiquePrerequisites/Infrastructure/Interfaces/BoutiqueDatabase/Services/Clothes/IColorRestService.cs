using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Base;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Clothes
{

    /// <summary>
    /// Сервис загрузки цвета одежды в базу данных
    /// </summary>
    public interface IColorRestService : IRestServiceBase<string, IColorDomain>
    { }
}