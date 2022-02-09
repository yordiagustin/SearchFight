using System;

namespace SearchFight.Services.Entities
{
    /// <summary>
    /// Searched Result
    /// </summary>
    public class SearchTotalResult
    {
        /// <summary>
        /// Searched Term
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Engine Name
        /// </summary>
        public string Engine { get; set; }

        /// <summary>
        /// Estimated Total Results
        /// </summary>
        public long TotalResults { get; set; }
    }
}
