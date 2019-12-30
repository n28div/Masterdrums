using System;
using System.Windows.Forms;
using MasterDrums.Model;

namespace MasterDrums.View
{
    /// <summary>
    /// 
    /// </summary>
    class PlayingPanel : Panel, IPlayingView
    {
        private IMainView _mainView;
        private TextBox txt;

        public PlayingPanel(IMainView mainView)
        {
            this._mainView = mainView;

            this.SuspendLayout();
            this.txt = new TextBox();
            this.txt.Location = new System.Drawing.Point(10, 10);
            this.txt.Size = new System.Drawing.Size(100, 50);
            this.Controls.Add(this.txt);
            this.ResumeLayout();
        }

        public double PerfectHitTime => throw new System.NotImplementedException();

        public void LaunchLeftNote(INote note)
        {
            this.txt.Text = "LEFT";
        }

        public void LaunchPauseNote()
        {
            this.txt.Text = "PAUSE";
        }

        public void LaunchRightNote(INote note)
        {
            this.txt.Text = "RIGHT";
        }

        public void LeftNoteHitted(double delta)
        {
            throw new System.NotImplementedException();
        }

        public void RightNoteHitted(double delta)
        {
            throw new System.NotImplementedException();
        }
    }
}
