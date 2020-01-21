using MasterDrums.Model;
using MasterDrums.View;

namespace MasterDrums.Controller
{
    /// <summary>
    /// Main controller, used to reflect the view events in the model.
    /// </summary>
    public class MainController : IController
    {
        private Game _game;

        private int _initialBpm = -1;
        private string _playerName = null;
        private IMainView _mainView = null;
        
        /// <summary>
        /// If the initial BPM and the player name are set the game is started
        /// otherwise an exception of type <c>GameOptionException</c> it raised
        /// </summary>
        public void StartGame()
        {
            if (this._initialBpm == -1 && this._playerName == null)
                throw new GameOptionException("Game options not set");

            this._game = new Game(this._initialBpm);
            this._game.PlayerName = this._playerName;
        }

        /// <summary>
        /// Stops the game and serializes the player's score.
        /// </summary>
        public void StopGame() {
            this._mainView.StopGame();
            this._game.SerializeScore();
            this._mainView.RefreshHighscores();
        }

        /// <summary>
        /// Communicate to the game model that a note has been hitted
        /// </summary>
        /// <param name="note">The note hitted</param>
        /// <param name="delay">Delay from the perfect time</param>
        public void NoteHitted(INote note, int delay) => this._game.Hit(note, delay);

        /// <summary>
        /// Communicate to the model that an empty hit has been performed
        /// </summary>
        public void EmptyHit() => this._game.Hit();

        /// <summary>
        /// Sets and gets the player's name
        /// </summary>
        public string PlayerName
        {
            set => this._playerName = string.IsNullOrWhiteSpace(value) ? null : value;
            get => this._playerName;
        }

        /// <summary>
        /// Sets and gets the initial bpm.
        /// </summary>
        public int InitialBpm
        {
            set => this._initialBpm = value;
            get => this._initialBpm;
        }

        /// <summary>
        /// Gets the bpm at which the user is playing
        /// </summary>
        public int Bpm {
            get => this._game.Bpm;
        }

        /// <summary>
        /// Main view instance
        /// </summary>
        public IMainView MainView
        {
            get => this._mainView;
            set => this._mainView = value;
        }

        /// <summary>
        /// The score totalized by the user
        /// </summary>
        public int Score
        {
            get => this._game.Score;
        }

        /// <summary>
        /// Time after or before an hit is considered as wasted
        /// </summary>
        public int HittedNoteInterval
        {
            get => this._game.NoteWastedMs;
        }

        /// <summary>
        /// The remaining wrong hits until the game ends
        /// </summary>
        public int WrongHitsRemaining
        {
            get => this._game.WrongHitsRemaining;
        }
        
    }
}
