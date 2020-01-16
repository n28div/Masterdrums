using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterDrums.View
{
    class GamePausePanel : TableLayoutPanel
    {
        private IMainView _mainView;
        private Button _btnResume;
        private Button _btnQuit;

        /// <summary>
        /// Player name panel is a table with one column and 3 rows.
        /// The 3rd row is used as a spacing row.
        /// </summary>
        public GamePausePanel(IMainView mainView) : base()
        {
            this._mainView = mainView;

            this.TableSetup();
            this.ButtonsSetup();

            this.BackColor = Color.DarkGray;
        }

        /// <summary>
        /// Sets up the table related properties
        /// </summary>
        private void TableSetup()
        {
            /// Table structure:
            /// 
            /// 2   btnResume       20% 
            /// 
            /// 4   btnQuit         20%

            this.SuspendLayout();
            this.ColumnCount = 1;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.RowCount = 5;

            this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            // resueme button
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            // quit button
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            this.ResumeLayout();
        }

        /// <summary>
        /// Apply padding font and docking properties to the control
        /// </summary>
        /// <param name="c">The input control where the styles are applied</param>
        /// <returns>The modified control</returns>
        private Control ApplyStyle(Control c)
        {
            c.Dock = DockStyle.Fill;
            c.Margin = new Padding(10);
            c.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            return c;
        }

        /// <summary>
        /// Sets up the buttons
        /// </summary>
        private void ButtonsSetup()
        {
            this.SuspendLayout();

            #region Game mode controls
            // TODO
            #endregion

            #region Resume button
            this._btnResume = new Button();
            this._btnResume.Text = "Riprendi";
            this._btnResume.UseVisualStyleBackColor = true;
            this._btnResume.Click += new EventHandler(this.ResumeOrQuit);
            this.ApplyStyle(this._btnResume);

            this.Controls.Add(this._btnResume, 0, 1);
            #endregion

            #region Quit button
            this._btnQuit = new Button();
            this._btnQuit.Text = "Abbandona";
            this._btnQuit.UseVisualStyleBackColor = true;
            this._btnQuit.Click += new EventHandler(this.ResumeOrQuit);
            this.ApplyStyle(this._btnQuit);

            this.Controls.Add(this._btnQuit, 0, 3);
            #endregion


            this.ResumeLayout();
        }

        private void ResumeOrQuit(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton.Text == "Riprendi")
            {
                this._mainView.ResumeGame();
            }
            if (clickedButton.Text == "Abbandona")
            {
                DialogResult dialogResult = MessageBox.Show("Vuoi davvero abbandonare la partita?", "Attenzione", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    this._mainView.StopGame();
                }
                else if (dialogResult == DialogResult.No)
                {
                    // nothing to do
                }
            }

        }
    }

}