using System;
using System.Windows.Forms;
using System.Drawing;

namespace MasterDrums.View
{
    class PlayerNamePanel : TableLayoutPanel
    {
        IView _mainView;
        TextBox _txtUsername;

        /// <summary>
        /// Player name panel is a table with one column and 3 rows.
        /// The 3rd row is used as a spacing row.
        /// </summary>
        public PlayerNamePanel(IView mainView) : base()
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
            this.SuspendLayout();
            this.ColumnCount = 1;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.RowCount = 4;
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            this.ResumeLayout();
        }

        /// <summary>
        /// Sets up the buttons
        /// </summary>
        private void ButtonsSetup()
        {
            this.SuspendLayout();

            Label labelUsername = new Label();
            labelUsername.Text = "Nome del giocatore";
            labelUsername.Dock = DockStyle.Fill;
            labelUsername.Margin = new Padding(10);
            this.Controls.Add(labelUsername, 0, 0);

            this._txtUsername = new TextBox();
            this._txtUsername.Dock = DockStyle.Fill;
            this._txtUsername.Margin = new Padding(10);
            this.Controls.Add(this._txtUsername, 0, 1);

            Button buttonConfirm = new Button();
            buttonConfirm.Dock = DockStyle.Fill;
            buttonConfirm.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            buttonConfirm.Margin = new Padding(10);
            buttonConfirm.Text = "Conferma";
            buttonConfirm.UseVisualStyleBackColor = true;
            buttonConfirm.Click += new EventHandler((s, e) => this._mainView.SetPlayerName(this._txtUsername.Text));
            this.Controls.Add(buttonConfirm, 0, 3);

            this.ResumeLayout();
        }
    }
}
