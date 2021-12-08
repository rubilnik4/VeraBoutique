using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Размер одежды. Сущность базы данных
    /// </summary>
    public class SizeEntity : SizeBase, ISizeEntity
    {
        public SizeEntity(ISizeBase size)
          : this(size.Id, size.SizeType, size.Name)
        { }
        
        public SizeEntity(int id, SizeType sizeType,  string name)
            : this(id, sizeType, name, null)
        { }

        public SizeEntity(int id, SizeType sizeType, string name, IEnumerable<SizeGroupCompositeEntity>? sizeGroupComposites)
            : base(sizeType, name)
        {
            Id = id;
            SizeGroupComposites = sizeGroupComposites?.ToList();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public override int Id { get; }

        /// <summary>
        /// Связующая сущность размера одежды
        /// </summary>
        public IReadOnlyCollection<SizeGroupCompositeEntity>? SizeGroupComposites { get; }
    }
}