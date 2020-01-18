using MasterDrums.Model;
using MasterDrums.View;
using System;
using System.Collections.Generic;

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
        /// The current bpm
        /// </summary>
        int Bpm
        {
            get;
        }

        /// <summary>
        /// The players score
        /// </summary>
        int Score
        {
            get;
        }

        /// <summary>
        /// A note has been hitted by the user
        /// </summary>
        /// <param name="note">The note hitted by the user</param>
        /// <param name="delay">The in ms from the perfect hit time</param>
        void NoteHitted(INote note, int delay);

        /// <summary>
        /// Called when an empty hit is performed
        /// </summary>
        void EmptyHit();

        /// <summary>
        /// Distance in ms from perfect note hit time in order to consider a note as hittable
        /// </summary>
        int HittedNoteInterval
        {
            get;
        }

        /// <summary>
        /// Remaining notes that can be missed until the game end
        /// </summary>
        int WastedNotesRemaining();

        /// <summary>
        /// Get the game records
        /// </summary>
        List<Tuple<int, String>> GetGameRecords();
    }
}
