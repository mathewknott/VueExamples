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
    [Route("/acme/api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IRegistrationService _registrationService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityService"></param>
        /// <param name="registrationService"></param>
        public ActivityController(
            IActivityService activityService,
            IRegistrationService registrationService
            )
        {
            _activityService = activityService;
            _registrationService = registrationService;
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
        [HttpGet("/api/acme/Activity/GetActivities", Name = "Activity_List")]
        public async Task<ActionResult<JsonPagedResult<IEnumerable<Activity>>>> GetActivities(string search, string sort, string order, int limit = 200, int offset = 0)
        {
            var activities = await _activityService.GetActivitiesAsync(sort, out int total, order, limit, offset, search);

            return new JsonPagedResult<IEnumerable<Activity>>
            {
                Total = total,
                Rows = activities
            };
        }

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
        [HttpGet("/api/acme/Activity/GetRegistrations", Name = "Registrations_List")]
        public async Task<ActionResult<JsonPagedResult<IEnumerable<Registration>>>> GetRegistrations(string search, string sort, string order, int limit = 200, int offset = 0)
        {
            var registrations = await _registrationService.GetRegistrationsAsync(sort, out int total, order, limit, offset, search);

            return new JsonPagedResult<IEnumerable<Registration>>
            {
                Total = total,
                Rows = registrations
            };
        }

        #endregion
    }
}