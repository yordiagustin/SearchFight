using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Services.Entities
{
    /// <summary>
    /// Google Search Response
    /// </summary>
    public class GoogleSearchResponse
    {
        [JsonProperty("searchInformation")]
        public SearchInformation SearchInformation { get; set; }
    }

    /// <summary>
    /// Search Information Object
    /// </summary>
    public class SearchInformation
    {
        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }
    }
}
