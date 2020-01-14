using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using MasterDrums.Model;
using MasterDrums.Utils;
using NAudio.Wave;

namespace MasterDrums.View
{
    /// <summary>
    /// 
    /// </summary>
    class PlayingPanel : Panel, IPanel, IPlayingView
    {
        private const int STICK_DOWN_MS = 50;
        private const int REFRESH_RATE_MS = 1;

        private IMainView _mainView;
        private PictureBox _backgroundPictureBox;

        private bool _leftStickDown = false;
        private bool _rightStickDown = false;
        private WaveOutEvent _outputDevice;
        private WaveFileReader _audio;

        private List<Pair<INote, Point>> _screenNotes;
        private int _bpm;
        
        public PlayingPanel(IMainView mainView)
        {
            this._mainView = mainView;

            this._screenNotes = new List<Pair<INote, Point>>();
            
            this._outputDevice = new WaveOutEvent();
            this._audio = new WaveFileReader(Resource.snare_hit);
            this._outputDevice.Init(this._audio);
        }

        /// <summary>
        /// Draw the snare and the sticks
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
            // Timer to draw continously the view
            Timer drawTimer = new Timer();
            drawTimer.Interval = REFRESH_RATE_MS;
            drawTimer.Tick += (s, e) => this._backgroundPictureBox.Invalidate();
            drawTimer.Start();

            this.Controls.Add(this._backgroundPictureBox);
            this.ResumeLayout();
        }

        /// <summary>
        /// Draw the object composing the game scene
        /// </summary>
        private void PaintObjects(object sender, PaintEventArgs e)
        {       
            /*
            this._msSiceLast += REFRESH_RATE_MS;
            if (((this._bpm / 60) * 1000) < this._msSiceLast)
                this._msSiceLast = 0;
            */

            e.Graphics.Clear(Color.White);

            this.DrawSnare(e.Graphics);
            this.DrawNotes(e.Graphics);

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
            int snareWidth = (int)(this.Size.Width / 3);
            int snareHeight = snareWidth;
            int snareX = (int)(this.Size.Width / 3);
            int snareY = (int)(this.Size.Height * 0.6);
            Image snare = Resource.snare;
            g.DrawImage(snare, snareX, snareY, snareWidth, snareHeight);

            // draw right and left hit spot
            g.DrawEllipse(new Pen(Color.LightGreen, 5F), this.LeftHitSpotX, this.HitSpotY, 20, 15);
            g.DrawEllipse(new Pen(Color.LightGreen, 5F), this.RightHitSpotX, this.HitSpotY, 20, 15);
        }

        /// <summary>
        /// The Y of the perfect hit spot
        /// </summary>
        private int HitSpotY
        {
            get => (int)(this.Size.Height * 0.75);
        }

        /// <summary>
        /// The X of the left perfect hit spot
        /// </summary>
        private int LeftHitSpotX
        {
            get => (int)((this.Size.Width / 3) + ((this.Size.Width / 3) * 0.30));
        }

        /// <summary>
        /// The X of the right perfect hit spot
        /// </summary>
        private int RightHitSpotX
        {
            get => (int)((this.Size.Width / 3) + ((this.Size.Width / 3) * 0.65));
        }

        /// <summary>
        /// Draws the left stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawLeftStickUp(Graphics g)
        {
            int w = (int)(this.Size.Width * 0.25);
            int h = w;
            int y = (int)(this.Size.Height * 0.55);
            int x = (int)((this.Size.Width * 0.30) - (w / 2));
            
            Image leftStick = ImageUtils.RotateImage(Resource.left_stick, 30);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draws the left stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawLeftStickDown(Graphics g)
        {
            int w = (int)(this.Size.Width * 0.25);
            int h = w;
            int y = (int)(this.Size.Height * 0.50);
            int x = (int)((this.Size.Width * 0.45) - w);

            Image leftStick = ImageUtils.RotateImage(Resource.left_stick, 100);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draws the right stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawRightStickUp(Graphics g)
        {
            int w = (int)(this.Size.Width * 0.25);
            int h = w;
            int y = (int)(this.Size.Height * 0.55);
            int x = (int)((this.Size.Width * 0.60));

            Image leftStick = ImageUtils.RotateImage(Resource.right_stick, -30);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draws the right stick up on the screen
        /// </summary>
        /// <param name="g">The graphics object where the image is drawed</param>
        private void DrawRightStickDown(Graphics g)
        {
            int w = (int)(this.Size.Width * 0.25);
            int h = w;
            int y = (int)(this.Size.Height * 0.50);
            int x = (int)((this.Size.Width * 0.55));

            Image leftStick = ImageUtils.RotateImage(Resource.right_stick, -100);
            g.DrawImage(leftStick, x, y, w, h);
        }

        /// <summary>
        /// Draw the notes on the screen
        /// </summary>
        /// <param name="g">The graphic object where the drawing is performed</param>
        private void DrawNotes(Graphics g)
        {
            Pair<INote, Point>[] copy = this._screenNotes.ToArray();

            foreach (Pair<INote, Point> p in copy)
            {
                INote note = p.Item1;
                Point curPoint = p.Item2;

                // draw note
                int noteSide = (int)(this.Width * 0.05);
                if (note is SpecialNote)
                    noteSide *= 2;
                
                g.DrawImage(note.Image, curPoint.X, curPoint.Y, noteSide, noteSide);

                /*                  |\-----> angle
                 *                  | \
                 *  HitSpotY ->     |  \    <- diagonalSpace
                 *                  |___\
                 *                     ^-- HitSpotX  
                 */
                double diagonalSpace;
                if (note.Position == INote.notePosition.Left)
                    diagonalSpace = Math.Sqrt(Math.Pow(this.LeftHitSpotX, 2) + Math.Pow(this.HitSpotY, 2));
                else
                    diagonalSpace = Math.Sqrt(Math.Pow(this.RightHitSpotX, 2) + Math.Pow(this.HitSpotY, 2));

                double angle = Math.Acos(this.HitSpotY / diagonalSpace);
                
                double speed = this.HitSpotY / ((60000 / this._bpm) / 16);

                double sy = speed * Math.Cos(angle);
                double sx = speed * Math.Sin(angle);

                int newX;
                if (note.Position == INote.notePosition.Left)
                    newX = (int)Math.Ceiling(curPoint.X + sx);
                else
                    newX = (int)Math.Ceiling(curPoint.X - sx);

                int newY = (int)Math.Ceiling(curPoint.Y + sy);
                Point newPoint = new Point(newX, newY);
                p.Item2 = newPoint;

                if (newPoint.Y > this.HitSpotY)
                    this._screenNotes.Remove(p);
            }
        }

        /// <summary>
        /// Time that a note takes to go from the top of the screen to the bottom in ms
        /// </summary>
        public float NoteRideTime
        {
            get => 0;
        }

        public int Bpm
        {
            set => this._bpm = value;
        }

        /// <summary>
        /// Add the note to the stack of the notes that will be showed
        /// </summary>
        /// <param name="note">The note launched</param>
        public void LaunchLeftNote(INote note)
        {
            this._screenNotes.Add(new Pair<INote, Point>(note, new Point(0, 0)));
        }

        public void LaunchPauseNote()
        {
        }

        /// <summary>
        /// Add the note to the stack of the notes that will be showed
        /// </summary>
        /// <param name="note">The note launched</param>
        public void LaunchRightNote(INote note)
        {
            this._screenNotes.Add(new Pair<INote, Point>(note, new Point(this.Size.Width, 0)));
        }

        /// <summary>
        /// Put down the left stick for 250ms
        /// </summary>
        public void LeftNoteHit()
        {
            this._leftStickDown = true;
            this.PlayHitSound();

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
        /// Plays the sound of a snare hit
        /// </summary>
        private void PlayHitSound()
        {
            this._outputDevice.Play();
            this._audio.Position = 0;
        }
    }
}
