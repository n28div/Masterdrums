using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Utils
{
    /// <summary>
    /// The abstract class that represents the Subject actor in the Observer pattern.
    /// </summary>
    public abstract class ISubject
    {
        /// <summary>
        /// List of actors that are currently observing.
        /// </summary>
        private List<IObserver> observers = new List<IObserver>();

        /// <summary>
        /// Method used by an actor to start observing the subject.
        /// </summary>
        /// <param name="obs">The actor who's going to observe</param>
        public void Attach(IObserver obs) {
            observers.Add(obs);
        }

        /// <summary>
        /// Method used by an actor to stop observing the subject.
        /// </summary>
        /// <param name="obs">The actor who's going to stop to observe</param>
        public void Detach(IObserver obs) {
            observers.Remove(obs);
        }

        /// <summary>
        /// Method used to notify the observers that the internl state has changed
        /// </summary>
        public void Notify()
        {
            foreach (IObserver obs in observers)
                obs.Update(this);
        }
    }
}
