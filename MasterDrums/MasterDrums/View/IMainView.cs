using MasterDrums.Model;
using System;

namespace MasterDrums.View
{
    /// <summary>
    /// Interface implemented by the main view
    /// </summary>
    public interface IMainView
    {
        #region Main menu panel
        /// <summary>
        /// Shows the new game panel
        /// </summary>
        void NewGame();

        /// <summary>
        /// Close the application
        /// </summary>
        void Quit();

        /// <summary>
        /// Shows the highscores panel
        /// </summary>
        void Highscores();
        #endregion

        #region New game panel
        /// <summary>
        /// Starts a new game
        /// </summary>
        void StartGame(string playerName, int initialBpm, INoteGenerator gameMode);

        /// <summary>
        /// Pause the game
        /// </summary>
        void PauseGame();

        /// <summary>
        /// Resume the game
        /// </summary>
        void ResumeGame();

        /// <summary>
        /// Stop the current game.
        /// </summary>
        void StopGame();

        /// <summary>
        /// Time required for a note to reach the hit point
        /// </summary>
        /// <returns>The timestamp</returns>
        int RideTime {
            get;
        }

        /// <summary>
        /// Check if the user is playing a game
        /// </summary>
        /// <returns>Boolean that inicates if the user is playng a game or not</returns>
        Boolean IsRunning {
            get;
        }

        /// <summary>
        /// The game score
        /// </summary>
        int GameScore
        {
            get;
        }


        #endregion

        #region Game panel
        /// <summary>
        /// Start moving a left note
        /// </summary>
        /// <param name="note">The note being launched</param>
        void LaunchLeftNote(INote note);

        /// <summary>
        /// Left note trigger
        /// </summary>
        void LeftNoteHit();

        /// <summary>
        /// Start moving a right note
        /// </summary>
        /// <param name="note">The note being launched</param>
        void LaunchRightNote(INote note);

        /// <summary>
        /// Right note trigger
        /// </summary>
        void RightNoteHit();
        #endregion
    }
}
