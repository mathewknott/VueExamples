using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore.Interfaces.JokeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJokesFeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<string> GetRandomJoke(string firstName, string lastName, string category = null);
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetCategories();

    }
}