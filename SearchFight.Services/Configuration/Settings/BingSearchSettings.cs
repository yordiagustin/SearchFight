using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Services.Configuration.Settings
{
    /// <summary>
    /// Bing Search Settings
    /// </summary>
    public class BingSearchSettings
    {
        /// <summary>
        /// Search Engine Name
        /// </summary>
        public string EngineName { get; set; }

        /// <summary>
        /// Base Url
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Authorization Header Key
        /// </summary>
        public string AuthorizationHeaderKey { get; set; }

        /// <summary>
        /// ApiKey
        /// </summary>
        public string ApiKey { get; set; }

    }
}
