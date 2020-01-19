using MasterDrums.Model;
using MasterDrums.View;
using MasterDrums.Utils;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using MasterDrums.Exception;

namespace MasterDrums.Controller
{
    /// <summary>
    /// Entry point of the game, each controller, view and model is managed through this class.
    /// </summary>
    public class MainController : IController
    {
        private Game _game;

        private int _initialBpm = -1;
        private string _playerName = null;
        private IMainView _mainView = null;
        
        /// <summary>
        /// Starts generating notes if the player name, the game mode and the initial bpms are set.
        /// </summary>
        public void StartGame()
        {
            if (this._initialBpm == -1 && this._playerName == null)
                throw new GameOptionException("Game options not set");

            this._game = new Game(this._initialBpm);
            this._game.PlayerName = this._playerName;
        }

        /// <summary>
        /// Stops generating notes and increasing bpms.
        /// Saves the player's score.
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
        public void NoteHitted(INote note, int delay)
        {
            this._game.Hit(note, delay);
        }

        /// <summary>
        /// Called when an empty hit is performed
        /// </summary>
        public void EmptyHit()
        {
            this._game.Hit();
        }

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
            set
            {
                this._initialBpm = value;
            }

            get => this._initialBpm;
        }

        /// <summary>
        /// Current bpm
        /// </summary>
        public int Bpm
        {
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
        /// The user score
        /// </summary>
        public int Score
        {
            get => this._game.Score;
        }

        /// <summary>
        /// Time after or before which an hit is considered as wasted
        /// </summary>
        public int HittedNoteInterval
        {
            get => this._game.NoteWastedMs;
        }

        /// <summary>
        /// The remaining wrong hits until the game end
        /// </summary>
        public int WrongHitsRemaining
        {
            get => this._game.WastedNotesRemaining;
        }
        
    }
}
