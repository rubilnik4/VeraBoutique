using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes
{
    /// <summary>
    /// Таблица базы данных категорий одежды
    /// </summary>
    public interface ICategoryTable : IDatabaseTable<string, CategoryEntity>
    { }
}