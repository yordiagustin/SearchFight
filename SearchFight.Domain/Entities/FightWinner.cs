using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Entities
{
    /// <summary>
    /// Fight Winner
    /// </summary>
    public class FightWinner
    {
        /// <summary>
        /// Sarch Engine
        /// </summary>
        public string SearchEngine { get; set; }

        /// <summary>
        /// Searched Term.
        /// </summary>
        public string Term { get; set; }


        /// <summary>
        /// Method To Format String Of Entity
        /// </summary>
        /// <returns>Formatted String Of Entity Values</returns>
        public override string ToString()
        {
            return $"{SearchEngine} winner: {Term}";
        }
    }
}
