using System;
using System.Windows.Forms;
using System.Drawing;

namespace MasterDrums.View
{
    class MainMenuPanel: TableLayoutPanel
    {
        private IMainView _mainView;

        /// <summary>
        /// Main menu panel is a table with one column and 4 rows.
        /// The 3rd row is used as a spacing row.
        /// </summary>
        public MainMenuPanel(IMainView mainView) : base()
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
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            this.ResumeLayout();
        }

        /// <summary>
        /// Sets up the buttons
        /// </summary>
        private void ButtonsSetup()
        {
            this.SuspendLayout();

            Button buttonNewGame = new Button();
            buttonNewGame.Dock = DockStyle.Fill;
            buttonNewGame.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            buttonNewGame.Margin = new Padding(10);
            buttonNewGame.Text = "Nuova partita";
            buttonNewGame.UseVisualStyleBackColor = true;
            buttonNewGame.Click += new EventHandler((s, e) => this._mainView.NewGame());
            this.Controls.Add(buttonNewGame, 0, 0);

            Button buttonHighscores = new Button();
            buttonHighscores.Dock = DockStyle.Fill;
            buttonHighscores.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            buttonHighscores.Margin = new Padding(10);
            buttonHighscores.Text = "Record";
            buttonHighscores.UseVisualStyleBackColor = true;
            buttonHighscores.Click += new EventHandler((s, e) => this._mainView.Highscores());
            this.Controls.Add(buttonHighscores, 0, 1);

            Button buttonQuit = new Button();
            buttonQuit.Dock = DockStyle.Fill;
            buttonQuit.Font = new Font("Microsoft Sans Serif", 20F, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            buttonQuit.Margin = new Padding(10);
            buttonQuit.Text = "Esci";
            buttonQuit.UseVisualStyleBackColor = true;
            buttonQuit.Click += new EventHandler((s, e) => this._mainView.Quit());
            this.Controls.Add(buttonQuit, 0, 3);

            this.ResumeLayout();
        }

    }
}
