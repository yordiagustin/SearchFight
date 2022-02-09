using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Domain.Abstractions
{
    /// <summary>
    /// Mapper Abstraction
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="To"></typeparam>
    public interface IMapper<in T, out To>
    {
        /// <summary>
        /// Method To Map From 
        /// </summary>
        /// <param name="input">Object</param>
        /// <returns>Mapped Object</returns>
        To MapFrom(T input);
    }
}
