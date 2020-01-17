using MasterDrums.Exception;
using MasterDrums.Utils;
using System;
using System.Windows.Forms;

namespace MasterDrums.Model
{
    /// <summary>
    /// The game class contains the game state informations.
    /// It is a singleton class.
    /// </summary>
    public class Game : IGame
    {
        private string _playerName = null;
        private int _bpm = -1;
        private int _score = 0;
        private int _wastedNotes = 0;

        /// <summary>
        /// Creates the game instance and sets the initial bpm
        /// </summary>
        /// <param name="initialBpm">The initial bpm</param>
        public Game(int initialBpm) : base()
        {
            this._bpm = initialBpm;
        }

        public string PlayerName {
            get => this._playerName;
            set => this._playerName = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public int Bpm => this._bpm;

        public int Score => this._score;

        /// <summary>
        /// A note is considered as wasted if its perfomed 200ms before or after it would naturally occur
        /// </summary>
        public int NoteWastedMs
        {
            get => 50;
        }

        public void Hit(INote note, double deltaT)
        {
            // every 2ms a 1 point penalty is added 
            int penalty = (int)Math.Round(deltaT / 2.0);
            this._score += (note.HitPoint - penalty);
        }

        public void Hit()
        {
            this._wastedNotes++;

            if (this._wastedNotes > 20)
                throw new GameEndedException();
        }

        public void SerializeScore()
        {
            throw new System.NotImplementedException();
        }

        
    }
}
