using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Controller
{
    public interface IController
    {
        /// <summary>
        /// Starts the application.
        /// </summary>
        void Run();
        /// <summary>
        /// Close the application.
        /// </summary>
        void Quit();

        /// <summary>
        /// Starts the game
        /// </summary>
        void StartGame();

        /// <summary>
        /// The name of the player who's playing.
        /// </summary>
        string PlayerName
        {
            set;
            get;
        }

    }
}
