using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table
{
    /// <summary>
    /// Таблица базы данных типа пола
    /// </summary>
    public interface IGenderTable : IDatabaseTable<GenderType, GenderEntity>
    { }
}