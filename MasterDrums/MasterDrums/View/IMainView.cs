using MasterDrums.Model;
using System;
using System.Collections.Generic;

namespace MasterDrums.View
{
    /// <summary>
    /// Interface implemented by the main view
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        /// Shows the main menu view
        /// </summary>
        void MainMenu();

        /// <summary>
        /// Shows the new game view
        /// </summary>
        void NewGame();

        /// <summary>
        /// Close the application
        /// </summary>
        void Quit();

        /// <summary>
        /// Shows the highscores view
        /// </summary>
        void Highscores();
     
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
        /// Left note trigger
        /// </summary>
        void LeftNoteHit();

        /// <summary>
        /// Right note trigger
        /// </summary>
        void RightNoteHit();
        
        /// <summary>
        /// Refresh the highscores panel after new record has been added
        /// </summary>
        void RefreshHighscores();
    }
}
