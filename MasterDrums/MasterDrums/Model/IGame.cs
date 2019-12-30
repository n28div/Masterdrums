using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDrums.Model
{
    public interface IGame
    {
        /// <summary>
        /// The player name
        /// </summary>
        string PlayerName { get; set; }

        /// <summary>
        /// The bpm at witch the user is currently playing
        /// </summary>
        int Bpm { get; }

        /// <summary>
        /// The score totalized by the user
        /// </summary>
        int Score { get; }

        /// <summary>
        /// The user has hit a note, the score is augmented based on bpm and note type.
        /// </summary>
        /// <param name="note">The note that has been hitted</param>
        /// <param name="deltaT">The delay in ms from the perfect hit spot</param>
        void Hit(INote note, double deltaT);

        /// <summary>
        /// Saves the score of the user in the .csv file
        /// </summary>
        void SerializeScore();
    }
}
