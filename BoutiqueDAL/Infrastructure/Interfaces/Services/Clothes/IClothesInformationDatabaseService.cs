using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис информации об одежде в базе данных
    /// </summary>
    public interface IClothesInformationDatabaseService : IDatabaseService<int, IClothesInformationDomain>
    { }
}