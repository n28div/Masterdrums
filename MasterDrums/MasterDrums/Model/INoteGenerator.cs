using System.Threading;
using MasterDrums.Utils;

namespace MasterDrums.Model
{
    /// <summary>
    /// Abstract class for the note generator objects.
    /// The class implements the Observer generator (to communicate the generated note).
    /// </summary>
    abstract class INoteGenerator : ISubject
    {
        private int _bpm;
        private Thread _generatorThread;
        private bool _isRunning;
        private INote _noteGenerated;

        /// <summary>
        /// Constructor that sets the initial bpm value and create the internal thread used to infinetely generate notes.
        /// </summary>
        /// <param name="bpm">The bpm value</param>
        public INoteGenerator(int bpm)
        {
            this._bpm = bpm;
            this._generatorThread = new Thread(new ThreadStart(this.InternalThreadRoutine));
            this._isRunning = false;
        }

        /// <summary>
        /// Constructor that sets the initial bpm to 50 and create the internal thread used to infinetely generate notes.
        /// </summary>
        public INoteGenerator(): this(50) { }

        /// <summary>
        /// Bpm property.
        /// If the value is less than 30 or more than 300 the bpm are set to 50bpm.
        /// </summary>
        public int Bpm
        {
            get => this._bpm;
            set => this._bpm = (value < 30 || value > 300) ? 50 : value;
        }

        /// <summary>
        /// Method used to get the current generated note.
        /// </summary>
        /// <returns>The generated note</returns>
        public INote CurrentNote
        {
            get => this._noteGenerated;
        }

        /// <summary>
        /// The private internal routine used to generate notes based on bpm.
        /// </summary>
        private void InternalThreadRoutine()
        {
            while (this._isRunning)
            {
                this._noteGenerated = this.NextNote();
                this.Notify();

                // to calculate the time needed to sleep we simply need to divide 60 (the seconds in 1 minute) by the bpm
                // the time calculated is in seconds, so whe convert it into milliseconds
                int timeToSleep = (60 / this._bpm) * 1000;
                Thread.Sleep(timeToSleep);
            }
        }

        /// <summary>
        /// Method used to get the next note.
        /// </summary>
        public abstract INote NextNote();

        /// <summary>
        /// Starts the internal thread
        /// </summary>
        public void Start()
        {
            this._generatorThread.Start();
            this._isRunning = true;
        }

        /// <summary>
        /// Stops the internal thread
        /// </summary>
        public void Stop()
        {
            this._generatorThread.Start();
            this._isRunning = false;
        }
    }
}
