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
    public class MainController : IController, IObserver
    {
        private Game _game;

        private int _initialBpm = -1;
        private string _playerName = null;
        private INoteGenerator _noteGenerator = null;
        private Queue<Pair<INote, int>> _generatedNotes;
        private System.Threading.Mutex _generatedNotesMutex;
        private IMainView _mainView = null;
        
        /// <summary>
        /// Starts generating notes if the player name, the game mode and the initial bpms are set.
        /// </summary>
        public void StartGame()
        {
            this._generatedNotesMutex = new System.Threading.Mutex();

            if (this._initialBpm == -1 && this._playerName == null && this._noteGenerator == null)
                throw new GameOptionException("Game options not set");

            // start generating the notes
            this._generatedNotes = new Queue<Pair<INote, int>>();
            this._noteGenerator.Attach(this);
            this._noteGenerator.Bpm = this._initialBpm;
            this._noteGenerator.Start();

            this._game = new Game(this._initialBpm);
            this._game.PlayerName = this._playerName;

            // a timer is created to remove non hitted notes
            Timer nonHittedTimer = new Timer();
            nonHittedTimer.Interval = 50;
            nonHittedTimer.Tick += NonHittedTimer_Tick;
            nonHittedTimer.Start();
        }

        private void NonHittedTimer_Tick(object sender, EventArgs e)
        {
            this._generatedNotesMutex.WaitOne();
            if (this._generatedNotes.Count > 0)
            {
                Pair<INote, int> topPair = this._generatedNotes.Peek();
                int timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                if (timestamp < (topPair.Item2 + this._mainView.RideTime + this._game.NoteWastedMs - 300000))
                {
                    this._generatedNotes.Dequeue();
                }
            }
            this._generatedNotesMutex.ReleaseMutex();
        }
        

        /// <summary>
        /// Called when the note generator generates a note.
        /// </summary>
        /// <param name="subj">The note generator instance</param>
        public void Update(ISubject subj)
        {
            INoteGenerator gen = (INoteGenerator)subj;
            INote generatedNote = gen.CurrentNote;

            int timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            if (!(generatedNote is PauseNote))
            {
                this._generatedNotesMutex.WaitOne();
                this._generatedNotes.Enqueue(new Pair<INote, int>(generatedNote, timestamp));
                this._generatedNotesMutex.ReleaseMutex();

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

        /// <summary>
        /// Pause generating notes and increasing bpms.
        /// </summary>
        public void PauseGame()
        {
            // pause generating notes
            this._noteGenerator.Pause();
            // TODO
        }

        /// <summary>
        /// Resume generating notes and increasing bpms.
        /// </summary>
        public void ResumeGame()
        {
            // resume generating notes
            this._noteGenerator.Resume();
            // TODO
        }

        /// <summary>
        /// Method called when the right note was hitted
        /// </summary>
        /// <param name="ts">Timestamp when the note was hitted</param>
        public void RightNoteHit(int ts)
        {
            this._generatedNotesMutex.WaitOne();
            try
            {
                Pair<INote, int> pair = this._generatedNotes.Peek();

                if (pair.Item1.Position == INote.notePosition.Right)
                {
                    this._generatedNotes.Dequeue();
                    int delay = Math.Abs(ts - pair.Item2);
                    this._game.Hit(pair.Item1, delay);
                }
            }
            catch (GameEndedException e)
            {
                MessageBox.Show("O FRA sono dsA");
            }
            catch
            {

            }
            finally
            {
                this._generatedNotesMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Method called when the left note was hitted
        /// </summary>
        /// <param name="ts">Timestamp when the note was hitted</param>
        public void LeftNoteHit(int ts)
        {
            this._generatedNotesMutex.WaitOne();
            try
            {
                Pair<INote, int> pair = this._generatedNotes.Peek();

                if (pair.Item1.Position == INote.notePosition.Left)
                {
                    this._generatedNotes.Dequeue();
                    int delay = Math.Abs(ts - pair.Item2);
                    this._game.Hit(pair.Item1, delay);
                }
            }
            catch (GameEndedException e)
            {
                MessageBox.Show("O FRA sono dsA");
            }
            catch
            {

            }
            finally
            {
                this._generatedNotesMutex.ReleaseMutex();
            }
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

        /// <summary>
        /// The user score
        /// </summary>
        public int Score
        {
            get => this._game.Score;
        }
    }
}
