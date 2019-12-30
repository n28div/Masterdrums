using MasterDrums.Utils;

namespace MasterDrums.Model
{
    /// <summary>
    /// The game class contains the game state informations.
    /// It is a singleton class.
    /// </summary>
    public class Game : ISingleton, IGame
    {
        private string _playerName = null;
        private int _bpm = -1;
        private int _score = 0;

        public string PlayerName {
            get => this._playerName;
            set => this._playerName = string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public int Bpm => this._bpm;

        public int Score => this._score;

        public void Hit(INote note, double deltaT)
        {
            // every 10ms a 1 point penalty is added 
            int penalty = (((int)deltaT) % 10) * -1;
            this._score += (note.HitPoint + penalty);
        }

        public void SerializeScore()
        {
            throw new System.NotImplementedException();
        }
    }
}
