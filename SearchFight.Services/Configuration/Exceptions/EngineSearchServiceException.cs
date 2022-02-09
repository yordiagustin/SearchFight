using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Services.Configuration.Exceptions
{
    /// <summary>
    /// Exception From Engine Search Service.
    /// </summary>
    public class EngineSearchServiceException : Exception
    {
        /// <summary>
        /// Engine Name
        /// </summary>
        public string EngineName { get; private set; }

        /// <summary>
        /// Engine Search Service Exception.
        /// </summary>
        /// <param name="engineName">Engine Name</param>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="exception">Exception</param>
        public EngineSearchServiceException(
            string engineName,
            string errorMessage,
            Exception exception = null) : base(errorMessage, exception)
        {
            this.EngineName = engineName;
        }
    }
}
