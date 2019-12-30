using MasterDrums.Model;
using MasterDrums.View;
using MasterDrums.Utils;

namespace MasterDrums.Controller
{
    /// <summary>
    /// Entry point of the game, each controller, view and model is managed through this class.
    /// </summary>
    public class MainController : IController, IObserver
    {
        private int _initialBpm = -1;
        private int _currentBpm = -1;
        private string _playerName = null;
        private INoteGenerator _noteGenerator = null;
        private IMainView _mainView = null;
        
        /// <summary>
        /// Starts generating notes if the player name, the game mode and the initial bpms are set.
        /// </summary>
        public void StartGame()
        {
            if (this._initialBpm == -1 && this._playerName == null && this._noteGenerator == null)
                throw new GameOptionException("Game options not sets");

            // start generating the notes
            this._noteGenerator.Attach(this);
            this._noteGenerator.Bpm = this._initialBpm;
            this._noteGenerator.Start();

            // TODO: start increasing bpm
        }

        /// <summary>
        /// Called when the note generator generates a note.
        /// </summary>
        /// <param name="subj">The note generator instance</param>
        public void Update(ISubject subj)
        {
            INoteGenerator gen = (INoteGenerator)subj;
            INote generatedNote = gen.CurrentNote;

            if (!(generatedNote is PauseNote))
            {
                if (generatedNote.Position == INote.notePosition.Left)
                {
                    this._mainView.LaunchLeftNote(generatedNote);
                }
                else
                {
                    this._mainView.LaunchRightNote(generatedNote);
              }
            }
        }

        /// <summary>
        /// Stops generating notes and increasing bpms.
        /// Saves the player's score.
        /// </summary>
        public void StopGame() {
            // stop generating notes
            this._noteGenerator.Stop();
            // stop increasing bpms
            // TODO
        }

        public void RightNoteHit()
        {
            throw new System.NotImplementedException();
        }

        public void LeftNoteHit()
        {
            throw new System.NotImplementedException();
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
                this._currentBpm = this._initialBpm;
            }

            get => this._initialBpm;
        }

        /// <summary>
        /// If the initial bpm, the player's name and the game mode has been set the game can start
        /// </summary>
        public bool GameReady
        {
            get => (this._playerName != null) && (this._initialBpm != -1) && (this._noteGenerator == null);
        }

        /// <summary>
        /// Sets the game mode
        /// </summary>
        public INoteGenerator GameMode {
            get => this._noteGenerator;
            set => this._noteGenerator = value;
        }

        /// <summary>
        /// Main view instance
        /// </summary>
        public IMainView MainView
        {
            get => this._mainView;
            set => this._mainView = value;
        }
    }
}
