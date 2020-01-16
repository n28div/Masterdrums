using MasterDrums.Exception;
using MasterDrums.Utils;
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

        private int _hittedNotes = 0;
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

        public int NoteWastedMs
        {
            get => 500;
        }

        public void Hit(INote note, double deltaT)
        {
            // every 10ms a 1 point penalty is added 
            int penalty = (((int)deltaT) % 10) * -1;
            this._score += (note.HitPoint + penalty);

            if (deltaT < this.NoteWastedMs)
                this._hittedNotes++;
            else
                this._wastedNotes++;

            if (this._wastedNotes >= 20)
            {
                throw new GameEndedException();
            }
        }

        public void SerializeScore()
        {
            throw new System.NotImplementedException();
        }

        
    }
}
