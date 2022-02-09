using SearchFight.Services.Entities;
using System.Threading.Tasks;

namespace SearchFight.Services.Abstractions
{
    /// <summary>
    /// Engine Search service.
    /// </summary>
    public interface IEngineSearchService
    {
        /// <summary>
        /// Search the term provided.
        /// </summary>
        /// <param name="term">Searched Term.</param>
        /// <returns>Estimated Total Number of Results.</returns>
        public Task<SearchTotalResult> GetTotalResults(string term);
    }
}
