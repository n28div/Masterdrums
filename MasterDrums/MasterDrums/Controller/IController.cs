using MasterDrums.Model;
using MasterDrums.View;

namespace MasterDrums.Controller
{
    public interface IController
    {
        /// <summary>
        /// Starts the game
        /// </summary>
        void StartGame();

        /// <summary>
        /// Stops the game
        /// </summary>
        void StopGame();

        /// <summary>
        /// The main view reference, used to show notes
        /// </summary>
        IMainView MainView
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the player who's playing.
        /// </summary>
        string PlayerName
        {
            set;
            get;
        }

        /// <summary>
        /// The initial bpm of the game.
        /// </summary>
        int InitialBpm
        {
            set;
            get;
        }

        /// <summary>
        /// The game mode is determined by the it instance.
        /// </summary>
        INoteGenerator GameMode
        {
            get;
            set;
        }

        /// <summary>
        /// The user has hit a right note
        /// </summary>
        void RightNoteHit();

        /// <summary>
        /// The user has hit a left note
        /// </summary>
        void LeftNoteHit();
    }
}
