using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearchFight.Services.Abstractions;
using SearchFight.Services.Configuration.Exceptions;
using SearchFight.Services.Configuration.Settings;
using SearchFight.Services.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.Services.Services
{
    /// <summary>
    /// Google Search Service
    /// </summary>
    public class GoogleSearchService : IGoogleSearchService
    {

        private readonly ILogger<GoogleSearchService> _logger;
        private readonly GoogleSearchSettings _googleSearchSettings;

        /// <summary>
        /// Google Search Service
        /// </summary>
        /// <param name="googleSearchSettings">Google Search Settings Injection</param>
        /// <param name="logger">Logger Dependency Injection</param>
        /// <exception cref="ArgumentNullException">Argument Null Exception</exception>
        public GoogleSearchService(GoogleSearchSettings googleSearchSettings, ILogger<GoogleSearchService> logger)
        {
            _googleSearchSettings = googleSearchSettings ?? throw new ArgumentNullException(nameof(googleSearchSettings));
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
                    httpClient.BaseAddress = new Uri(_googleSearchSettings.BaseUrl);

                    response = await httpClient
                    .GetAsync($"?key={_googleSearchSettings.ApiKey}&cx={_googleSearchSettings.Cx}&q={term}")
                    .ConfigureAwait(false);
                }

                if (!response.IsSuccessStatusCode)
                {
                    var engineSearchServiceException = new EngineSearchServiceException(
                        _googleSearchSettings.EngineName,
                        $"Error getting results for '{term}'");

                    _logger.LogError($"Error with {engineSearchServiceException.EngineName} Service. {engineSearchServiceException.Message}");
                    throw engineSearchServiceException;
                }

                var jsonResult = await response.Content.ReadAsStringAsync();
                var googleSearchResponse = JsonConvert.DeserializeObject<GoogleSearchResponse>(jsonResult);
                return new SearchTotalResult
                {
                    Engine = _googleSearchSettings.EngineName,
                    Term = term,
                    TotalResults = googleSearchResponse.SearchInformation.TotalResults
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
