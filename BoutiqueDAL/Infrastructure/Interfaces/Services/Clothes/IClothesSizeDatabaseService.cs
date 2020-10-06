using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes
{
    /// <summary>
    /// Сервис размеров одежды в базе данных
    /// </summary>
    public interface IClothesSizeDatabaseService : IDatabaseService<string, IClothesSizeDomain>
    { }
}