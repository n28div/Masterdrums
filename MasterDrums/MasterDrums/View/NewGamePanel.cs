using MasterDrums.Model;
using System;
using System.Windows.Forms;
using System.Drawing;

namespace MasterDrums.View
{
    class NewGamePanel : TableLayoutPanel
    {
        private IMainView _mainView;
        private TextBox _txtUsername;
        private NumericUpDown _txtInitialBpm;
        private ComboBox _gameModeSelection;

        /// <summary>
        /// Player name panel is a table with one column and 3 rows.
        /// The 3rd row is used as a spacing row.
        /// </summary>
        public NewGamePanel(IMainView mainView) : base()
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
            /// 0   labelName       14% 
            /// 1   txtName         14%
            /// 
            /// 2   labelMode       14%
            /// 3   selectMode      14%
            /// 
            /// 4   labelInitialBpm 14%
            /// 5   txtInitialBpm   14%
            /// 
            /// 6   startButton     16%

            this.SuspendLayout();
            this.ColumnCount = 1;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.RowCount = 4;

            // player name
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));

            // game mode
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));

            // initial bpm
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 14F));

            // start button
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));

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

            #region Username controls
            Label labelUsername = new Label();
            labelUsername.Text = "Nome del giocatore";
            this.ApplyStyle(labelUsername);

            this._txtUsername = new TextBox();
            this.ApplyStyle(this._txtUsername);

            this.Controls.Add(labelUsername, 0, 0);
            this.Controls.Add(this._txtUsername, 0, 1);
            #endregion

            #region Game mode controls
            Label labelGameMode = new Label();
            labelGameMode.Text = "Modalità di gioco";
            this.ApplyStyle(labelGameMode);

            this._gameModeSelection = new ComboBox();
            this.ApplyStyle(this._gameModeSelection);
            this._gameModeSelection.Items.Add(new RandomNoteGenerator());
            this._gameModeSelection.Items.Add(new AlternatedHandNoteGenerator());
            this._gameModeSelection.SelectedIndex = 0;

            this.Controls.Add(labelGameMode, 0, 2);
            this.Controls.Add(this._gameModeSelection, 0, 3);
            #endregion

            #region Initial bpm controls
            Label labelInitialBpm = new Label();
            labelInitialBpm.Text = "BPM Iniziale";
            this.ApplyStyle(labelInitialBpm);

            this._txtInitialBpm = new NumericUpDown();
            this._txtInitialBpm.Minimum = INoteGenerator.MIN_BPM;
            this._txtInitialBpm.Maximum = INoteGenerator.MAX_BPM;
            this.ApplyStyle(this._txtInitialBpm);

            this.Controls.Add(labelInitialBpm, 0, 4);
            this.Controls.Add(this._txtInitialBpm, 0, 5);
            #endregion

            #region Confirm controls
            Button buttonConfirm = new Button();
            buttonConfirm.Text = "Inizia";
            buttonConfirm.UseVisualStyleBackColor = true;
            buttonConfirm.Click += new EventHandler(this.Confirm);
            this.ApplyStyle(buttonConfirm);

            this.Controls.Add(buttonConfirm, 0, 6);
            #endregion

            this.ResumeLayout();
        }

        private void Confirm(object sender, EventArgs e)
        {
            if (this._txtUsername.Text != "")
            {
                string name = this._txtUsername.Text;
                int initialBpm = (int)this._txtInitialBpm.Value;

                if (this._gameModeSelection.SelectedItem != null)
                {
                    INoteGenerator gameMode = (INoteGenerator)this._gameModeSelection.SelectedItem;
                    this._mainView.StartGame(name, initialBpm, gameMode);
                }
                else
                    MessageBox.Show("E' necessario inserire la modalità per iniziare una partita!");
            }
            else
            {
                MessageBox.Show("E' necessario inserire il nome del giocatore per iniziare una partita!");
            }
        }
    }
}
