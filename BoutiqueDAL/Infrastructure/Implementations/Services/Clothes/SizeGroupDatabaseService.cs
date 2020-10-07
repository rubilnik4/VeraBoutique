using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Services.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Services.Clothes
{
    /// <summary>
    /// Сервис группы размеров одежды в базе данных
    /// </summary>
    public class SizeGroupDatabaseService : DatabaseService<(ClothesSizeType, int), ISizeGroupDomain, SizeGroupEntity>,
                                            ISizeGroupDatabaseService
    {
        public SizeGroupDatabaseService(IDatabase database, ISizeGroupTable sizeTable,
                                        ISizeGroupEntityConverter sizeEntityConverter)
            : base(database, sizeTable, sizeEntityConverter)
        { }
    }
}