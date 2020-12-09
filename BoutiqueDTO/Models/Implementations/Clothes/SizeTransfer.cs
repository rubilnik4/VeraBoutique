using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BoutiqueCommon.Infrastructure.Implementation;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Clothes;

namespace BoutiqueDTO.Models.Implementations.Clothes
{
    /// <summary>
    /// Размер одежды. Трансферная модель
    /// </summary>
    public class SizeTransfer : SizeBase, ISizeTransfer
    {
        public SizeTransfer(ISizeBase size)
            : this(size.SizeType, size.Name)
        { }

        [JsonConstructor]
        public SizeTransfer(SizeType sizeType, string name)
            : base(sizeType, name)
        { }
    }
}