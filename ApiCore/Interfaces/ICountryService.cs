using System.Collections.Generic;
using ApiCore.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApiCore.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> CountrySelectList(CountryValueType type);
    }
}