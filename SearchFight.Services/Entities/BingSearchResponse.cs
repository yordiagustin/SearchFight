using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Services.Entities
{
    /// <summary>
    /// Bing Search Response
    /// </summary>
    public class BingSearchResponse
    {
        [JsonProperty("webPages")]
        public WebPages WebPages { get; set; }
    }

    /// <summary>
    /// Web Pages Object
    /// </summary>
    public class WebPages
    {
        [JsonProperty("totalEstimatedMatches")]
        public long TotalEstimatedMatches { get; set; }
    }
}
