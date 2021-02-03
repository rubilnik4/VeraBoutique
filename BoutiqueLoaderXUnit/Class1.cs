using System.Linq;
using System.Text.Json;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using BoutiqueLoader.Factories.DatabaseInitialize.Boutique;
using Xunit;

namespace BoutiqueLoaderXUnit
{
    public class Class1
    {
        /// <summary>
        /// Преобразовать в Json
        /// </summary>
        [Fact]
        public void ToJson_Ok()
        {
            var clothes = ClothesInitialize.Clothes.First();
            var clothesTransferConverter = new ClothesTransferConverter(new GenderTransferConverter(),
                                                                        new ClothesTypeShortTransferConverter(),
                                                                        new ColorTransferConverter(),
                                                                        new SizeGroupTransferConverter(new SizeTransferConverter()));
            var clothesTransfer = clothesTransferConverter.ToTransfer(clothes);
            string json = JsonSerializer.Serialize(clothesTransfer);
            var clothesAfterJson = JsonSerializer.Deserialize<ClothesTransfer>(json);

            Assert.True(clothesAfterJson?.Equals(clothesTransfer));
        }
    }
}