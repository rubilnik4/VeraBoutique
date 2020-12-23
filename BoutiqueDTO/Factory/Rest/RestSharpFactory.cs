using System;
using BoutiqueDTO.Models.Interfaces.Connection;
using RestSharp;

namespace BoutiqueDTO.Factory.Rest
{
    public static class RestSharpFactory
    {
        public static IRestClient GetRestClient(IHostConnection hostConnection) =>
             new RestClient(hostConnection.Host)
             {
                 Timeout = (int)TimeSpan.FromSeconds(hostConnection.TimeOut).TotalMilliseconds,
             };
    }
}