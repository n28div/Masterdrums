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
        /// Shows the main menu panel
        /// </summary>
        void MainMenu();

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
        #endregion

        #region Game panel
        /// <summary>
        /// Left note trigger
        /// </summary>
        void LeftNoteHit();

        /// <summary>
        /// Right note trigger
        /// </summary>
        void RightNoteHit();
        #endregion
    }
}
