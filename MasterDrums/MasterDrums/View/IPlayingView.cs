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
        /// Method called when a left note is hitted.
        /// </summary>
        void LeftNoteHit();

        /// <summary>
        /// Method called when a right note is hitted.
        /// </summary>
        void RightNoteHit();

        /// <summary>
        /// The mode played by the user
        /// </summary>
        INoteGenerator GameMode
        {
            get;
            set;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        void StartGame();

        /// <summary>
        /// Stops the game
        /// </summary>
        void StopGame();

        /// <summary>
        /// Puts the note generator in pause
        /// </summary>
        void PauseGame();

        /// <summary>
        /// Resumes the game from pause
        /// </summary>
        void ResumeGame();

        /// <summary>
        /// States if the game is running or not
        /// </summary>
        bool IsRunning
        {
            get;
        }
    }
}
