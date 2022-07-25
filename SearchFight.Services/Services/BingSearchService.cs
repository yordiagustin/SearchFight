using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearchFight.Services.Abstractions;
using SearchFight.Services.Configuration.Exceptions;
using SearchFight.Services.Configuration.Settings;
using SearchFight.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Services.Services
{
    /// <summary>
    /// Bing Search Service
    /// </summary>
    public class BingSearchService : IEngineSearchService
    {

        private readonly ILogger<BingSearchService> _logger;
        private readonly BingSearchSettings _bingSearchSettings;

        /// <summary>
        /// Bing Search Service
        /// </summary>
        /// <param name="bingSearchSettings">Bing Search Settings Injection</param>
        /// <param name="logger">Logger Dependency Injection</param>
        /// <exception cref="ArgumentNullException">Argument Null Exception</exception>
        public BingSearchService(BingSearchSettings bingSearchSettings, ILogger<BingSearchService> logger)
        {
            _bingSearchSettings = bingSearchSettings ?? throw new ArgumentNullException(nameof(bingSearchSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Total Results
        /// </summary>
        /// <param name="term">Term to search.</param>
        /// <returns>Search Result</returns>
        /// <exception cref="ArgumentNullException">Argument Null Exception</exception>
        public async Task<SearchTotalResult> GetTotalResults(string term)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    throw new ArgumentNullException(nameof(term));
                }

                HttpResponseMessage response;

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(_bingSearchSettings.BaseUrl);
                    httpClient.DefaultRequestHeaders
                    .Add(_bingSearchSettings.AuthorizationHeaderKey, _bingSearchSettings.ApiKey);

                    response = await httpClient
                    .GetAsync($"?q={term}")
                    .ConfigureAwait(false);
                }

                if (!response.IsSuccessStatusCode)
                {
                    var engineSearchServiceException = new EngineSearchServiceException(
                        _bingSearchSettings.EngineName,
                        $"Error retrieving results for {term}");

                    _logger.LogError($"Error with {engineSearchServiceException.EngineName} Service. {engineSearchServiceException.Message}");
                    throw engineSearchServiceException;
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var bingSearchResponse = JsonConvert.DeserializeObject<BingSearchResponse>(jsonResult);

                return new SearchTotalResult
                {
                    Engine = _bingSearchSettings.EngineName,
                    Term = term,
                    TotalResults = bingSearchResponse.WebPages.TotalEstimatedMatches
                };
            }
            catch (EngineSearchServiceException searchServiceException)
            {
                _logger.LogError($"Error with {searchServiceException.EngineName} Service. See details here: {searchServiceException.Message}");
                throw;
            }
        }
    }
}
