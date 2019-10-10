using System.Threading.Tasks;
using ApiCore.Models.JokeGenerator;

namespace ApiCore.Interfaces.JokeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public interface INamesFeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<NameResult> GetName();
    }
}