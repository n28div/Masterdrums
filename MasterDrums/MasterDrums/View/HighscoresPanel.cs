using MasterDrums.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MasterDrums.View
{
    class HighscoresPanel : TableLayoutPanel
    {
        private IMainView _mainView;
        private ListView _records;
        private Button _btnBack;

        /// <summary>
        /// Highscores panel is a table with one column and 2 rows.
        /// The 1st row contains a listview with the game results,
        /// the 2nd contains a back button to return to the main menù
        /// </summary>
        public HighscoresPanel(IMainView mainView) : base()
        {
            this._mainView = mainView;
            this.TableSetup();
            this.ListViewSetup();
            this.ButtonSetup();
        }

        /// <summary>
        /// Sets up the table related properties
        /// </summary>
        private void TableSetup()
        {
            /// Table structure:
            /// Listview            80%
            ///                      5%
            /// btnBack             15%

            this.SuspendLayout();
            this.ColumnCount = 1;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.RowCount = 3;

            // listview
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            // blank space
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            // back button
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));

            this.ResumeLayout();
        }

        /// <summary>
        /// Sets up the listview containing the records
        /// </summary>
        private void ListViewSetup()
        {
            this.SuspendLayout();

            this._records = new ListView();
            this._records.View = System.Windows.Forms.View.Details;
            this._records.GridLines = true;

            // Add columns
            this._records.Columns.Add("Nome", 300);
            this._records.Columns.Add("Punteggio", 300);

            List<Tuple<int, String>> records = Game.LoadBestResults();
            String[] items = new string[2];

            // Add rows
            foreach (Tuple<int, String> t in records)
            {
                items[0] = t.Item2;
                items[1] = t.Item1.ToString();
                this._records.Items.Add(new ListViewItem(items));
            }

            this.ApplyStyle(this._records);
            this.Controls.Add(this._records, 0, 0);

            this.ResumeLayout();
        }


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
        /// Sets up the back button
        /// </summary>
        private void ButtonSetup()
        {
            this.SuspendLayout();
            this._btnBack = new Button();
            this._btnBack.Text = "Indietro";
            this._btnBack.Click += new EventHandler((s, e) => this._mainView.MainMenu());
            this.ApplyStyle(this._btnBack);
            this.Controls.Add(this._btnBack, 0, 2);
            this.ResumeLayout();
        }
    }
}
