using MasterDrums.Model;

namespace MasterDrums.View
{
    /// <summary>
    /// Interface implemented by the view that takes care of showing the game elements
    /// </summary>
    public interface IPlayingView
    {
        /// <summary>
        /// The time that a note takes in order to go from the top of the screen to the perfect hit spot.
        /// </summary>
        float NoteRideTime
        {
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
        void LeftNoteHit();

        /// <summary>
        /// Launch a right note on the screen and takes care of signaling when the note has been hit
        /// and how much delay there was between the hit and the estimed perfect hit
        /// </summary>
        /// <param name="note">The note that will be launched</param>
        void LaunchRightNote(INote note);

        /// <summary>
        /// Method called when a right note is hitted.
        /// </summary>
        void RightNoteHit();
    }
}
