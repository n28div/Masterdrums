using MasterDrums.Model;

namespace MasterDrums.View
{
    /// <summary>
    /// Interface implemented by the view that takes care of showing the game elements
    /// </summary>
    public interface IPlayingView
    {
        /// <summary>
        /// The exact time at which the hit is considered as perfect.
        /// The value is relative to the note launch.
        /// </summary>
        /// <returns>The time value</returns>
        double PerfectHitTime {
            get;
        }

        /// <summary>
        /// Launch a left note on the screen and takes care of signaling when the note has been hit
        /// and how much delay there was between the hit and the estimed perfect hit
        /// </summary>
        /// <param name="note">The note that will be launched</param>
        void LaunchLeftNote(INote note);

        /// <summary>
        /// Method called when a left note is hitted.
        /// </summary>
        /// <param name="delta">The distance from the perfect hit time in ms</param>
        void LeftNoteHitted(double delta);

        /// <summary>
        /// Launch a right note on the screen and takes care of signaling when the note has been hit
        /// and how much delay there was between the hit and the estimed perfect hit
        /// </summary>
        /// <param name="note">The note that will be launched</param>
        void LaunchRightNote(INote note);

        /// <summary>
        /// Method called when a right note is hitted.
        /// </summary>
        /// <param name="delta">The distance from the perfect hit time in ms</param>
        void RightNoteHitted(double delta);

        /// <summary>
        /// Launch a pause note on the screen and takes care of signaling when the note has been hit
        /// and how much delay there was between the hit and the estimed perfect hit
        /// </summary>
        void LaunchPauseNote();
    }
}
