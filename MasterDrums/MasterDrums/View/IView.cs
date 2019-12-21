using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.View
{
    /// <summary>
    /// Interface used to interact with the view
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Method used to remove all the views from the main panel
        /// </summary>
        void ClearView();

        #region Main menu
        /// <summary>
        /// Shows the menu where the user can leave the game mode and change the commands
        /// </summary>
        void ShowMenuView();

        /// <summary>
        /// Called when the user wants to start a new game
        /// </summary>
        void StartNewGame();

        /// <summary>
        /// Called when the user wants to close the game (can be called only from the main menu).
        /// </summary>
        void Quit();

        /// <summary>
        /// Shows the view containing the records of the game.
        /// </summary>
        void ShowHighscoreView();
        #endregion

        /// <summary>
        /// Shows the view where the user can change the commands used to play.
        /// </summary>
        void ShowCommandsView();

        /// <summary>
        /// Shows instruction view where the game mechanics are explained.
        /// </summary>
        void ShowInstructionView();

        
    }
}
