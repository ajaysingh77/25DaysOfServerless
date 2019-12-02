using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaysOfServerless.Day1.Services
{
    public class DreidelOptions
    {
        /// <summary>
        /// Return options defined in settings file
        /// </summary>
        public IList<string> Dreidels { get; set; }
    }
}
