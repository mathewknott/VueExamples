using Newtonsoft.Json;

namespace ApiCore.Models.JokeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public class NameResult
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("surname")]
        public string Surname { get; set; }
    }
}