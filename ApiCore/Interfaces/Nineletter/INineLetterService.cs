using System.Collections.Generic;
using System.Threading.Tasks;
using NineLetter.Web.Models.NineLetter;

namespace ApiCore.Interfaces.NineLetter
{
    /// <summary>
    /// 
    /// </summary>
    public interface INineLetterService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Result>> GetPatternResult();
        
    }
}