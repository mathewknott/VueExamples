using System;
using System.Collections.Generic;
using System.Linq;
using ApiCore.Interfaces.NineLetter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NineLetter.Web.Models;
using NineLetter.Web.Models.NineLetter;
using System.Threading.Tasks;

namespace ApiCore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NineLetterController : Controller
    {
        private readonly IOptions<NineLetterConfig> _optionsAccessor;
        private readonly IFileProvider _fileProvider;
        private readonly IMemoryCache _memoryCache;
        private readonly INineLetterService _nineLetterService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="fileProvider"></param>
        /// <param name="memoryCache"></param>
        public NineLetterController(IOptions<NineLetterConfig> optionsAccessor, IFileProvider fileProvider, IMemoryCache memoryCache, INineLetterService nineLetterService)
        {
            _optionsAccessor = optionsAccessor;
            _fileProvider = fileProvider;
            _memoryCache = memoryCache;
            _nineLetterService = nineLetterService;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Result> PatternList()
        {
            var cacheKey = "Patterns";

            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Result> _))
            {
                return (IEnumerable<Result>)_memoryCache.Get(cacheKey);
            }

            var r = new List<Result>();

            var patternList = _nineLetterService.GetPatterns(_optionsAccessor.Value.PatternsToGenerate);

            foreach (var pattern in patternList)
            {
                var wordResults = _nineLetterService.ProcessTextFile(_fileProvider.GetFileInfo(_optionsAccessor.Value.FileLocation).PhysicalPath, pattern.Pattern, _optionsAccessor.Value.MinLettersLength, _optionsAccessor.Value.IgnoreProperNouns, pattern.Pattern[4]);

                var results = new Result
                {
                    Pattern = pattern.Pattern
                };

                var enumerable = wordResults as IList<string> ?? wordResults.ToList();

                if (wordResults.Any())
                {
                    results.Words = enumerable;
                    results.PossibleWords = enumerable.Count;
                    results.LongestWord = enumerable.Last();
                }
                r.Add(results);
            }

            var l = r.OrderByDescending(p => p.PossibleWords);

            // keep item in cache as long as it is requested at least
            // once every 5 minutes...
            // but in any case make sure to refresh it every hour

            // store in the cache
            _memoryCache.Set(cacheKey, l, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5))
              .SetAbsoluteExpiration(TimeSpan.FromHours(1)));

            return l;
        }

        /// <summary>
        /// Returns all available patterns
        /// </summary>
        [Route("Patterns")]
        [HttpGet]
        public async Task<IEnumerable<Result>> Patterns()
        {
            return await Task.FromResult(PatternList());
        }

        /// <summary>
        /// Validates a pattern.
        /// </summary>
        /// <param name="patternInput"></param>
        [HttpGet]
        [Route("validate/{patternInput}")]
        public async Task<Result> Validate(string patternInput)
        {
            if (patternInput == "" || patternInput.Length != 9)
            {
                return new Result(){};
            }

            var item = PatternList().SingleOrDefault(x => x.Pattern.Equals(patternInput, StringComparison.CurrentCultureIgnoreCase));
            return await Task.FromResult(item);
        }
    }
}