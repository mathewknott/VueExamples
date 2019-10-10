using System.Collections.Generic;

namespace NineLetter.Web.Models.NineLetter
{
    public class Result
    {
        public Result()
        {
            Words = new List<string>();
        }

        public string Pattern { get; set; }

        public IEnumerable<Result> Patterns { get; set; }

        public IEnumerable<string> Words { get; set; }

        public int PossibleWords { get; set; }

        public string LongestWord { get; set; }
    }
}