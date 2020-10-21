using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис цвета одежды в базе данных
    /// </summary>
    public class ColorClothesDatabaseService : DatabaseService<string, IColorClothesDomain, ColorClothesEntity>,
                                               IColorClothesDatabaseService
    {
        public ColorClothesDatabaseService(IDatabase database, IColorClothesTable colorClothesTable,
                                           IColorClothesEntityConverter colorClothesEntityConverter)
            : base(database, colorClothesTable, colorClothesEntityConverter)
        { }
    }
}