using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiCore.Interfaces.JokeGenerator;
using Newtonsoft.Json;

namespace ApiCore.Services.JokeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class JokesFeed : IJokesFeed
    {
        private string Url { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        public JokesFeed(string endpoint)
        {
            Url = endpoint;
        }

        /// <inheritdoc />
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="category"></param>
        /// <returns>Joke string</returns>
        public async Task<string> GetRandomJoke(string firstName, string lastName, string category = null)
        {
           var client = new HttpClient { BaseAddress = new Uri(Url) };

            try
            {
                string joke;

                if (category == null)
                {
                    joke = await Task.FromResult(client.GetStringAsync("jokes/random").Result);
                }
                else
                {
                    joke = await Task.FromResult(client.GetStringAsync($"jokes/random?category={category}").Result);
                }

                if (joke != null && firstName != null && lastName != null)
                {
                    joke = joke.Replace("Chuck Norris", $"{firstName} {lastName}");
                }

                return JsonConvert.DeserializeObject<dynamic>(joke).value;
            }
            catch (Exception)
            {
                return "Joke Or Category Not Found.";
            }
        }
        
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetCategories()
        {
            var client = new HttpClient { BaseAddress = new Uri(Url) };

            var result = await Task.FromResult(client.GetStringAsync("jokes/categories").Result);
            
            return JsonConvert.DeserializeObject<List<string>>(result);
        }
    }}