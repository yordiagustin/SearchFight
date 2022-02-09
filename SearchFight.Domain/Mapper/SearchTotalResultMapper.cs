using SearchFight.Domain.Abstractions;
using SearchFight.Services.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Domain.Mapper
{
    /// <summary>
    /// Search Total Result Mapper
    /// </summary>
    public class SearchTotalResultMapper : IMapper<List<Task<SearchTotalResult>>, List<SearchTotalResult>>
    {
        /// <summary>
        /// Mapper From Search Results Tasks To List Of Search Results
        /// </summary>
        /// <param name="searchTasks"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public List<SearchTotalResult> MapFrom(List<Task<SearchTotalResult>> searchTasks)
        {
            if (searchTasks == null)
            {
                throw new ArgumentNullException(nameof(searchTasks));
            }

            var searchTotalResults = new List<SearchTotalResult>();
            foreach (var task in searchTasks)
            {
                searchTotalResults.Add(task.Result);
            }
            return searchTotalResults;
        }
    }
}
