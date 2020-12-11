using System;
using System.Net.Http;
using System.Threading.Tasks;
using BoutiquePrerequisites.Boutique;
using BoutiquePrerequisites.Factories.Client;
using BoutiquePrerequisites.Factories.Connection;

namespace BoutiquePrerequisites
{
    public class Program
    {
        public static async Task Main() =>
            BoutiqueClientFactory.BoutiqueClient;
        //{
        //    var boutiqueClient = ;


        //    var genders = await boutiqueClient.ApiGenderGetAsync();
        //}
    }
}
