using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Utils
{
    /// <summary>
    /// Interface for the Observable actor used for the Observable pattern.
    /// </summary>
    interface IObserver
    {
        /// <summary>
        /// The abstract method used to update the internal state based on the Observable object.
        /// </summary>
        /// <param name="subj">The observed object.</param>
        void Update(Subject subj);
    }
}
