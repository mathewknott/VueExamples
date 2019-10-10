using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCore.Interfaces.JokeGenerator;
using ApiCore.Models.JokeGenerator;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers.JokeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class JokeGeneratorController : ControllerBase
    {
        private readonly IJokesFeed _jokesService;
        private readonly INamesFeed _namesService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jokesService"></param>
        /// <param name="namesService"></param>
        public JokeGeneratorController(
            IJokesFeed jokesService, INamesFeed namesService)
        {
            _jokesService = jokesService;
            _namesService = namesService;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 3660, VaryByHeader = "User-Agent")]
        [Route("getcategories")]
        public async Task<ActionResult<List<string>>> GetCategories()
        {
            return await _jokesService.GetCategories();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 3660, VaryByHeader = "User-Agent")]
        [Route("getname")]
        public async Task<ActionResult<NameResult>> GetNames()
        {
            return await _namesService.GetName();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="randomName"></param>
        /// <param name="category"></param>
        /// <returns>If Random Name = false then you must input first and last name</returns>
        [HttpGet]
        [Route("getrandomjoke")]
        public async Task<ActionResult<string>> GetRandomJoke(bool randomName = false, string firstName = null, string lastName = null, string category = null)
        {
            if (randomName)
            {
                var name = _namesService.GetName().Result;

                return await _jokesService.GetRandomJoke(name.Name, name.Surname, category);
            }

            if(firstName != null && lastName != null)
            {
                return await _jokesService.GetRandomJoke(firstName, lastName, category);
            }

            return "Enter first and last name";

        }
    }
}