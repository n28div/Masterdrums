namespace MasterDrums.Utils
{
    /// <summary>
    /// Interface for the Observable actor used for the Observable pattern.
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// The abstract method used to update the internal state based on the Observable object.
        /// </summary>
        /// <param name="subj">The observed object.</param>
        void Update(ISubject subj);
    }
}
