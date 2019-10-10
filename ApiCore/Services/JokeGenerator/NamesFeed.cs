using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiCore.Interfaces.JokeGenerator;
using ApiCore.Models.JokeGenerator;
using Newtonsoft.Json;

namespace ApiCore.Services.JokeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class NamesFeed : INamesFeed
    {
        private string Url { get; }

        /// <summary>
        /// 
        /// </summary>
        public NamesFeed(string endpoint)
        {
            Url = endpoint;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<NameResult> GetName()
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };

            var response = await client.GetStringAsync("");
            
            return JsonConvert.DeserializeObject<NameResult>(response);
        }
    }
}