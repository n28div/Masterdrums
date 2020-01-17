using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MasterDrums.Model;
using MasterDrums.Utils;
using MasterDrums.Controller;
using NAudio.Wave;

namespace MasterDrums.View
{
    /// <summary>
    /// 
    /// </summary>
    class PlayingPanel : Panel, IPlayingView, IPanel, IObserver
    {
        private const int STICK_DOWN_MS = 50;
        private const int REFRESH_RATE_MS = 20;

        private IMainView _mainView;
        private IController _controller;

        private INoteGenerator _noteGenerator = null;
        private Timer _gameLoopTimer;
        private bool _isRunning;

        private PictureBox _backgroundPictureBox;

        private bool _leftStickDown = false;
        private bool _rightStickDown = false;
        private WaveOutEvent _outputDevice;
        private WaveFileReader _audio;

        private System.Threading.Mutex _notesMutex;
        private LinkedList<Triplet<INote, Point, int>> _notes;
        
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="mainView">The main view instance that created the panel</param>
        /// <param name="controller">The controller instance used to communicate with the model</param>
        public PlayingPanel(IMainView mainView, IController controller)
        {
            this._mainView = mainView;
            this._controller = controller;

            // create the note generator and subscribe as listener
            this._isRunning = false;

            // the queue containg the notes viewed by the user
            this._notes = new LinkedList<Triplet<INote, Point, int>>();
            this._notesMutex = new System.Threading.Mutex();

            // the game loop timer takes care of drawing the required objects in the panel's main picture box
            this._gameLoopTimer = new Timer();
            _gameLoopTimer.Interval = REFRESH_RATE_MS;

            // the output sound configuration using NAudio lib
            this._outputDevice = new WaveOutEvent();
            this._audio = new WaveFileReader(Resource.snare_hit);
            this._outputDevice.Init(this._audio);
        }

        /// <summary>
        /// Called by the notegenerator when a new note has been generated
        /// Thread safe
        /// </summary>
        /// <param name="ng">The note generator instance</param>
        public void Update(ISubject ng)
        {
            try
            {
                this._notesMutex.WaitOne();

                INote note = this._noteGenerator.CurrentNote;

                if (!(note is PauseNote))
                {
                    int timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                    Point point;
                    if (note.Position == INote.notePosition.Left)
                        point = new Point(0, 0);
                    else
                        point = new Point(this.Size.Width, 0);

                    this._notes.AddLast(new Triplet<INote, Point, int>(note, point, timestamp));
                }
            }
            finally
            {
                this._notesMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// The mode played by the user
        /// </summary>
        public INoteGenerator GameMode
        {
            get => this._noteGenerator;
            set {
                if (this._noteGenerator != null)
                    this._noteGenerator.Detach(this);

                this._noteGenerator = value;
                this._noteGenerator.Attach(this);
            }
        }

        /// <summary>
        /// Draw the backgroung main picture box and setup the game loop to update it
        /// </summary>
        public void Draw()
        {
            this.SuspendLayout();
            this.Controls.Clear();

            this._backgroundPictureBox = new PictureBox();
            this._backgroundPictureBox.Width = this.Size.Width;
            this._backgroundPictureBox.Height = this.Size.Height;
            this._backgroundPictureBox.Location = new Point(0, 0);

            this._backgroundPictureBox.Paint += this.PaintObjects;
            this._gameLoopTimer.Tick += (s, e) => this._backgroundPictureBox.Invalidate();

            this.Controls.Add(this._backgroundPictureBox);
            this.ResumeLayout();
        }

        /// <summary>
        /// Draw the object composing the game scene
        /// </summary>
        private void PaintObjects(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            this.DrawSnare(e.Graphics);
            this.DrawNotes(e.Graphics);
            this.DrawScore(e.Graphics);

            if (this._leftStickDown)
                this.DrawLeftStickDown(e.Graphics);
            else
                this.DrawLeftStickUp(e.Graphics);

            if (this._rightStickDown)
                this.DrawRightStickDown(e.Graphics);
            else
                this.DrawRightStickUp(e.Graphics);
        }

        /// <summary>
        /// Draws the snare on the background picture box
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawSnare(Graphics g)
        {
            int snareSide = (int)Math.Round(this.Size.Width / 3.0);
            int snareX = (int)Math.Round(this.Size.Width / 3.0);
            int snareY = (int)Math.Round(this.Size.Height * 0.6);
            Image snare = Resource.snare;
            g.DrawImage(snare, snareX, snareY, snareSide, snareSide);

            // draw right and left hit spot
            int ellipseHeight = (int)Math.Round(this.Size.Height * 0.05);
            int ellipseWidth = (int)Math.Round(this.Size.Width * 0.05);

            int leftEllipseX = (int)Math.Round(this.LeftHitSpotX - (ellipseWidth / 2.0));
            int rightEllipseX = (int)Math.Round(this.RightHitSpotX - (ellipseWidth / 2.0));
            int ellipseY = (int)Math.Round(this.HitSpotY - (ellipseHeight / 2.0));

            g.DrawEllipse(new Pen(Color.LightGreen, 5F), leftEllipseX, ellipseY, ellipseWidth, ellipseHeight);
            g.DrawEllipse(new Pen(Color.LightGreen, 5F), rightEllipseX, ellipseY, ellipseWidth, ellipseHeight);
        }

        /// <summary>
        /// The Y of the perfect hit spot
        /// </summary>
        private int HitSpotY
        {
            get => (int)Math.Round(this.Size.Height * 0.75);
        }

        /// <summary>
        /// The X of the left perfect hit spot
        /// </summary>
        private int LeftHitSpotX
        {
            // size of the snare + 30% of the snare width from left
            get
            {
                int snareSize = (int)Math.Round(this.Size.Width / 3.0);
                return (int)Math.Round(snareSize + (snareSize * 0.3)); 
            }
        }

        /// <summary>
        /// The X of the right perfect hit spot
        /// </summary>
        private int RightHitSpotX
        {
            // size of the snare + 60% size of the snare
            get
            {
                int snareSize = (int)Math.Round(this.Size.Width / 3.0);
                return (int)Math.Round(snareSize + (snareSize * 0.6));
            }
        }

        /// <summary>
        /// Draws the left stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawLeftStickUp(Graphics g)
        {
            int w = (int)Math.Round(this.Size.Width * 0.25);
            int h = w;
            int y = (int)Math.Round(this.Size.Height * 0.55);
            int x = (int)Math.Round((this.Size.Width * 0.30) - (w / 2));
            
            Image leftStick = ImageUtils.RotateImage(Resource.left_stick, 30);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draws the left stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawLeftStickDown(Graphics g)
        {
            int w = (int)Math.Round(this.Size.Width * 0.25);
            int h = w;
            int y = (int)Math.Round(this.Size.Height * 0.50);
            int x = (int)Math.Round((this.Size.Width * 0.45) - w);

            Image leftStick = ImageUtils.RotateImage(Resource.left_stick, 100);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draws the right stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawRightStickUp(Graphics g)
        {
            int w = (int)Math.Round(this.Size.Width * 0.25);
            int h = w;
            int y = (int)Math.Round(this.Size.Height * 0.55);
            int x = (int)Math.Round((this.Size.Width * 0.60));

            Image leftStick = ImageUtils.RotateImage(Resource.right_stick, -30);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draws the right stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawRightStickDown(Graphics g)
        {
            int w = (int)Math.Round(this.Size.Width * 0.25);
            int h = w;
            int y = (int)Math.Round(this.Size.Height * 0.50);
            int x = (int)Math.Round((this.Size.Width * 0.55));

            Image leftStick = ImageUtils.RotateImage(Resource.right_stick, -100);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draw the notes on the screen
        /// Thread safe
        /// </summary>
        /// <param name="g">The graphic object where the drawing is performed</param>
        private void DrawNotes(Graphics g)
        {
            try
            {
                this._notesMutex.WaitOne();
                Triplet<INote, Point, int>[] notesCopy = new Triplet<INote, Point, int>[this._notes.Count];
                this._notes.CopyTo(notesCopy, 0);

                foreach (Triplet<INote, Point, int> x in notesCopy)
                {
                    INote note = x.Item1;
                    Point curPoint = x.Item2;

                    // draw note
                    int noteSide = (int)Math.Round(this.Width * 0.03);
                    if (note is SpecialNote)
                        noteSide *= 2;

                    g.DrawImage(note.Image,
                                (curPoint.X - (noteSide / 2)),
                                (curPoint.Y - (noteSide / 2)),
                                noteSide,
                                noteSide);

                    /*                  |\-----> angle
                     *                  | \
                     *  HitSpotY ->     |  \    <- diagonalSpace
                     *                  |___\
                     *                     ^-- HitSpotX  
                     */
                    double diagonalSpace = Math.Sqrt(Math.Pow(this.LeftHitSpotX, 2) + Math.Pow(this.HitSpotY, 2));
                    double angle = Math.Acos(this.HitSpotY / diagonalSpace);
                    double speed = diagonalSpace / this.NoteRideTime;

                    double sy = REFRESH_RATE_MS * speed * Math.Cos(angle);
                    double sx = REFRESH_RATE_MS * speed * Math.Sin(angle);

                    int newX;
                    int newY = (int)Math.Round(curPoint.Y + sy);
                    if (note.Position == INote.notePosition.Left)
                        newX = (int)Math.Round(curPoint.X + sx);
                    else
                        newX = (int)Math.Round(curPoint.X - sx);

                    Point newPoint = new Point(newX, newY);
                    x.Item2 = newPoint;
                    if (newPoint.Y > this.Size.Height)
                        this._notes.Remove(x);
                    else
                        x.Item2 = newPoint;
                }
            } finally
            {
                this._notesMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Draw the score on the screen
        /// </summary>
        /// <param name="g">The graphic object where the drawing is performed</param>
        private void DrawScore(Graphics g)
        {
            int scoreX = (int)Math.Round(this.Size.Width / 2.0);
            int scoreY = (int)Math.Round(this.Size.Height * 0.1);
            g.DrawString(this._controller.Score.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), new Point(scoreX, scoreY));
        }

        /// <summary>
        /// Time that a note takes to go from the top of the screen to the bottom in ms
        /// </summary>
        public float NoteRideTime
        {
            get => ((60000 / this._controller.Bpm) / 2);
        }

        /// <summary>
        /// Returns the distance from HitSpotY in which a note is considered as not hitted 
        /// (therefore the hit is wasted) 
        /// </summary>
        private int WastedDistance
        {
            get {
                double diagonalSpace = Math.Sqrt(Math.Pow(this.LeftHitSpotX, 2) + Math.Pow(this.HitSpotY, 2));
                double speed = diagonalSpace / this.NoteRideTime;
                return (int)Math.Round(speed * this._controller.HittedNoteInterval);
            }
        }

        /// <summary>
        /// Put down the left stick for 250ms and send the hit to the controller
        /// </summary>
        public void LeftNoteHit()
        {
            this._leftStickDown = true;
            this.PlayHitSound();
            this.Hit(INote.notePosition.Left);

            Timer t = new Timer();
            t.Interval = STICK_DOWN_MS;
            t.Tick += (s, e) =>
            {
                this._leftStickDown = false;
                t.Stop();
            };
            t.Start();
        }

        /// <summary>
        /// Put down the right stick for 250ms
        /// </summary>
        public void RightNoteHit()
        {
            this._rightStickDown = true;
            this.PlayHitSound();
            this.Hit(INote.notePosition.Right);

            Timer t = new Timer();
            t.Interval = STICK_DOWN_MS;
            t.Tick += (s, e) =>
            {
                this._rightStickDown = false;
                t.Stop();
            };
            t.Start();
        }

        /// <summary>
        /// Method called when an hit is performed
        /// Check wether the hit is conrrect or wasted
        /// </summary>
        /// <param name="position">The position if the hit</param>
        private void Hit(INote.notePosition position)
        {
            try
            {
                this._notesMutex.WaitOne();
                Triplet<INote, Point, int> bottomNote = this._notes.First.Value;

                if (bottomNote.Item1.Position == position && 
                    Math.Abs(bottomNote.Item2.Y - this.HitSpotY) < this.WastedDistance)
                {
                    int timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    int hitDelay = Math.Abs(timestamp - bottomNote.Item3);
                    this._controller.NoteHitted(bottomNote.Item1, hitDelay);
                    this._notes.RemoveFirst();
                }
                else
                    this._controller.EmptyHit();
            } catch
            {
                this._controller.EmptyHit();
            }
            finally
            {
                this._notesMutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Plays the sound of a snare hit
        /// </summary>
        private void PlayHitSound()
        {
            if (this.IsRunning)
            {
                this._outputDevice.Play();
                this._audio.Position = 0;
            }
        }

        /// <summary>
        /// Return wether the game is running
        /// </summary>
        public bool IsRunning
        {
            get => this._isRunning;
        }

        /// <summary>
        /// Puts the game in pause
        /// </summary>
        public void PauseGame()
        {
            this._isRunning = false;
            this._noteGenerator.Pause();
            this._gameLoopTimer.Stop();
        }

        /// <summary>
        /// Resume from pause
        /// </summary>
        public void ResumeGame()
        {
            this._isRunning = true;
            this._gameLoopTimer.Start();
            this._noteGenerator.Resume();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            this._isRunning = true;
            this._controller.StartGame();
            this._noteGenerator.Start();
            this._gameLoopTimer.Start();
        }

        /// <summary>
        /// Stops the game
        /// </summary>
        public void StopGame()
        {
            this._isRunning = false;
            this._noteGenerator.Stop();
            this._controller.StopGame();
        }
    }
}
