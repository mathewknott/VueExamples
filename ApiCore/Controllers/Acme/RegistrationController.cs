using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCore.DTO.Acme;
using ApiCore.Interfaces.Acme;
using ApiCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers.Acme
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/acme/[controller]")]
    //[Authorize]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationService"></param>
        /// <param name="userService"></param>
        public RegistrationController(
            IRegistrationService registrationService,
            IUserService userService
            )
        {
            _registrationService = registrationService;
            _userService = userService;
        }
        
        #region JsonResults 

        /// <summary>
        /// Returns a list created.
        /// </summary>
        /// <param name="search">Text to search</param>
        /// <param name="sort">Parameter to sort </param>
        /// <param name="order">ASC or DESC</param>
        /// <param name="limit">Number of rows to return</param>
        /// <param name="offset">Starting position/offset in table</param>
        /// <returns></returns>
        /// 
        [HttpGet("/api/acme/Registration/GetRegistrations", Name = "Registration_List")]
        public async Task<IActionResult> GetRegistrations(string search, string sort, string order, int limit = 200, int offset = 0)
        {
            var registrations = await _registrationService.GetRegistrationsAsync(sort, out int total, order, limit, offset, search);

            var result = new JsonPagedResult<IEnumerable<Registration>>
            {
                Total = total,
                Rows = registrations
            };

            return Json(result);
        }

        /// <summary>
        /// Returns a list created.
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="search">Text to search</param>
        /// <param name="sort">Parameter to sort </param>
        /// <param name="order">ASC or DESC</param>
        /// <param name="limit">Number of rows to return</param>
        /// <param name="offset">Starting position/offset in table</param>
        /// <returns></returns>
        /// 
        [HttpGet("/api/acme/Registration/GetRegistrationsByActivityId", Name = "Registration_List_By_ActivityId")]
        public async Task<IActionResult> GetRegistrationsByActivityId(Guid activityId, string search, string sort, string order, int limit = 200, int offset = 0)
        {
            var registrations = await _registrationService.GetRegistrationsByActivityIdAsync(out var total, activityId, limit, offset);

            var result = new JsonPagedResult<IEnumerable<Registration>>
            {
                Total = total,
                Rows = registrations
            };

            return Json(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [HttpGet("/api/acme/Registration/GetUsers", Name = "Users_List")]
        public async Task<IActionResult> GetUsers(string search, string sort, string order, int limit = 200, int offset = 0)
        {
            var users = await _userService.GetUsersAsync(sort, out int total, order, limit, offset, search);

            var result = new JsonPagedResult<IEnumerable<User>>
            {
                Total = total,
                Rows = users
            };

            return Json(result);
        }
        
        #endregion
    }
}