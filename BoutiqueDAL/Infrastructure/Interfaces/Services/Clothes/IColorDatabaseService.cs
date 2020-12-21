using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис цвета одежды в базе данных
    /// </summary>
    public interface IColorDatabaseService : IDatabaseService<string, IColorDomain>
    { }
}