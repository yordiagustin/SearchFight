using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Services.Configuration.Settings
{
    /// <summary>
    /// Google Search Settings
    /// </summary>
    public class GoogleSearchSettings
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
        /// Api Key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Search Engine Id
        /// </summary>
        public string Cx { get; set; }
    }
}
