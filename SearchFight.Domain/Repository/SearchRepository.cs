using Microsoft.Extensions.Logging;
using SearchFight.Domain.Abstractions;
using SearchFight.Services.Abstractions;
using SearchFight.Services.Entities;
using SearchFight.Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Domain.Repository
{
    /// <summary>
    /// Search Repository
    /// </summary>
    public class SearchRepository : ISearchRepository
    {
        private readonly ILogger<SearchRepository> _logger;
        private readonly IMapper<List<Task<SearchTotalResult>>, List<SearchTotalResult>> _mapper;
        private readonly List<IEngineSearchService> _searchServices;

        /// <summary>
        /// Search Repository Constructor
        /// </summary>
        /// <param name="logger">Logger Dependency Injection</param>
        /// <param name="mapper">Mapper Dependency Injection</param>
        /// <param name="bingSearchService">Bing Search Service Dependency Injection</param>
        /// <param name="googleSearchService">Google Search Service Dependency Injection</param>
        /// <exception cref="ArgumentNullException">Argument Null Exception</exception>
        public SearchRepository(ILogger<SearchRepository> logger, IMapper<List<Task<SearchTotalResult>>, List<SearchTotalResult>> mapper,
            IBingSearchService bingSearchService, IGoogleSearchService googleSearchService)
        {
            _searchServices = new List<IEngineSearchService>
            {
                googleSearchService,
                bingSearchService
            };

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Search Results with Engine Search Services.
        /// </summary>
        /// <param name="terms">Searched Terms</param>
        /// <returns>List of search total result.</returns>
        public async Task<IList<SearchTotalResult>> GetSearchResults(IList<string> terms)
        {
            if (terms == null || terms.Count < 2)
            {
                var exception = new ArgumentException("You must enter at least 2 terms to search", nameof(terms));
                _logger.LogError(exception.Message);
                throw exception;
            }

            try
            {
                var searchTotalResultsTasks = new List<Task<SearchTotalResult>>();
                foreach (var searchService in _searchServices)
                {
                    foreach (var term in terms)
                    {
                        searchTotalResultsTasks.Add(searchService.GetTotalResults(term));
                    }
                }
                await Task.WhenAll(searchTotalResultsTasks);
                return _mapper.MapFrom(searchTotalResultsTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError("Excepción:" + ex.Message);
                throw;
            }
        }
    }
}
